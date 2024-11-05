using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Para o ILogger
using Moq; // Para usar o Moq
using OrbitaChallengeBackEnd.Controllers;
using OrbitaChallengeBackEnd.Data;
using OrbitaChallengeBackEnd.Interfaces;
using OrbitaChallengeBackEnd.Models;
using OrbitaChallengeBackEnd.Repositories;
using OrbitaChallengeBackEnd.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OrbitaChallengeTest
{
    public class StudentsTest
    {
        private readonly AppDbContext _context;
        private readonly IStudentRepository _studentRepository; 
        private readonly StudentController _studentController;
        private readonly Mock<ILogger<StudentRepository>> _loggerMock; 

        public StudentsTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _loggerMock = new Mock<ILogger<StudentRepository>>();

            _studentRepository = new StudentRepository(_context, _loggerMock.Object); 

            _studentController = new StudentController(_studentRepository);
        }

        [Fact]
        public async Task InsertStudent_ShouldAddStudentToDatabase()
        {
            // Arrange: Configura os dados de teste
            var student = new Student
            {
                Name = "Estudante Teste",
                Email = "estudante@email.com",
                RA = "12345",
                CPF = "101.492.950-49",
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                IsActive = true
            };

            // Act: Chama o método de inserção no controlador
            await _studentController.Add(student);

            // Assert: Verifica se o estudante foi adicionado ao banco de dados
            var result = await _context.Students.FirstOrDefaultAsync(s => s.Name == "Estudante Teste"); // Corrigido o nome
            Assert.NotNull(result);
            Assert.Equal("Estudante Teste", result.Name);
            Assert.Equal("estudante@email.com", result.Email);
            Assert.Equal("12345", result.RA);
            Assert.Equal("101.492.950-49", result.CPF);
            // Comparar datas sem milissegundos
            Assert.True((result.CreatedAt - student.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public async Task UpdateStudent_ShouldNotAllowInvalidCPF()
        {
            var student = new Student
            {
                Name = "Estudante Teste",
                Email = "estudante@email.com",
                RA = Guid.NewGuid().ToString(),
                CPF = "101.492.950-48",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            student.CPF = "111.111.111-11"; // CPF inválido
            var result = await _studentController.Edit(student);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultViewModel = Assert.IsType<ResultViewModel<bool>>(okResult.Value);

            Assert.False(resultViewModel.Success);
            Assert.Equal("Invalid CPF", resultViewModel.Message);
        }

        [Fact]
        public async Task DeleteStudent_ShouldRemoveStudentFromDatabase()
        {
            // Arrange: Adiciona um estudante ao banco de dados para depois excluí-lo
            var student = new Student
            {
                Name = "Estudante a Ser Excluído",
                Email = "exclusao@email.com",
                RA = "54321",
                CPF = "222.222.222-22",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Act: Chama o método de exclusão no controlador
            var result = await _studentController.Delete(student.Id);

            // Assert: Verifica se o estudante foi removido do banco de dados
            Assert.IsType<OkResult>(result); // Confirma se o retorno foi um OkResult
            var deletedStudent = await _context.Students.FindAsync(student.Id);
            Assert.Null(deletedStudent); // Confirma que o estudante não está mais no banco
        }

        [Fact]
        public async Task DeleteStudent_ShouldReturnNotFoundForNonexistentStudent()
        {
            // Act: Tenta excluir um estudante com um ID que não existe
            var result = await _studentController.Delete(999); // ID fictício que não existe

            // Assert: Verifica se o resultado é NotFound
            Assert.IsType<NotFoundResult>(result); // Confirma que o retorno foi NotFound
        }


    }
}
