using OrbitaChallengeBackEnd.Models;
using OrbitaChallengeBackEnd.ViewModels;
namespace OrbitaChallengeBackEnd.Interfaces;

  public interface IStudentRepository
  {
    Task<ResultViewModel<bool>> AddAsync(Student student);
    Task<ResultViewModel<IEnumerable<Student>>> GetAllAsync();
    Task<ResultViewModel<bool>> DeleteAsync(string ra);
    Task<ResultViewModel<bool>> EditAsync(Student updatedStudent);
    Task<ResultViewModel<IEnumerable<Student>>> GetByAsync(string searchTerm);
}

