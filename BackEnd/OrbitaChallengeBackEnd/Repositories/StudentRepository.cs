using Microsoft.EntityFrameworkCore;
using OrbitaChallengeBackEnd.Data;
using OrbitaChallengeBackEnd.Interfaces;
using OrbitaChallengeBackEnd.Models;
using OrbitaChallengeBackEnd.ViewModels;

namespace OrbitaChallengeBackEnd.Repositories;

  public class StudentRepository : IStudentRepository
  {
    private readonly ILogger<StudentRepository> _logger;
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResultViewModel<IEnumerable<Student>>> GetAllAsync()
    {
        try
        {
            var students = await _context.Students.ToListAsync();
            return new ResultViewModel<IEnumerable<Student>>(true, "Students retrieved successfully", students);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving students.");
            return new ResultViewModel<IEnumerable<Student>>(false, "Error retrieving students");
        }
    }

    public async Task<ResultViewModel<bool>> AddAsync(Student student)
    {
        try
        {
            if (await _context.Students.AnyAsync(s => s.RA == student.RA))
                return new ResultViewModel<bool>(false, "A student with this RA already exists.");
            

            if (!IsValidCPF(student.CPF))
                return new ResultViewModel<bool>(false, "Invalid CPF");
            

            if (await _context.Students.AnyAsync(s => s.CPF == student.CPF))
                return new ResultViewModel<bool>(false, "A student with this CPF already exists.");
            

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return new ResultViewModel<bool>(true, "Student created successfully", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding student.");
            return new ResultViewModel<bool>(false, "Error adding student");
        }
    }

    private bool IsValidCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        // Calcula o primeiro dígito verificador
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        // Calcula o segundo dígito verificador
        tempCpf += digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        // Compara os dígitos calculados com os dígitos do CPF informado
        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }

    public async Task<ResultViewModel<bool>> EditAsync(Student updatedStudent)
    {
        try
        {

            if (!IsValidCPF(updatedStudent.CPF))
            {
                return new ResultViewModel<bool>(false, "Invalid CPF");
            }

            if (await _context.Students.AnyAsync(s => s.CPF == updatedStudent.CPF && s.RA != updatedStudent.RA))
                return new ResultViewModel<bool>(false, "A student with this CPF and another RA already exists.");


            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.RA == updatedStudent.RA);


            if (existingStudent == null)
                return new ResultViewModel<bool>(false, "Student not found");


            existingStudent.Name = updatedStudent.Name;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.CPF = updatedStudent.CPF;
            existingStudent.UpdatedAt = DateTime.Now; // Atualiza a data de atualização

            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();

            return new ResultViewModel<bool>(true, "Student updated successfully", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating student.");
            return new ResultViewModel<bool>(false, "Error updating student");
        }
    }
    public async Task<ResultViewModel<IEnumerable<Student>>> GetByAsync(string searchTerm)
    {
        try
        {
            // Busca os estudantes pelo Nome, RA ou CPF, utilizando Contains para buscas parciais
            var students = await _context.Students
                .Where(s =>
                    s.Name.Contains(searchTerm) ||
                    s.RA.Contains(searchTerm) ||
                    s.CPF.Contains(searchTerm))
                .ToListAsync();

            // Retorna o resultado com a informação de sucesso
            return new ResultViewModel<IEnumerable<Student>>(students.Any(),
                students.Any() ? "Students found." : "No students found.",
                students.AsEnumerable());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching students.");
            return new ResultViewModel<IEnumerable<Student>>(false, "Error fetching students.");
        }
    }


    public async Task<ResultViewModel<bool>> DeleteAsync(string id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return new ResultViewModel<bool>(false, "Student not found");
        }

        try
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return new ResultViewModel<bool>(true, "Student deleted successfully", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting student.");
            return new ResultViewModel<bool>(false, "Error deleting student");
        }
    }

    public async Task<ResultViewModel<Student>> GetByRAAsync(string RA)
    {
        try
        {
            var student = await _context.Students
                .Where(s => s.RA == RA).FirstOrDefaultAsync();

            return new ResultViewModel<Student>(true,
                student is not null ? "Student found." : "No student found.",
                student);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching student.");
            return new ResultViewModel<Student>(false, "Error fetching student.");
        }
    }

}

