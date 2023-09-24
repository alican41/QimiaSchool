using QimiaSchool1.DataAccess.Entities;

namespace QimiaSchool1.Business.Abstracts;

public interface IStudentManager
{
    public Task CreateStudentAsync(
        Student student,
        CancellationToken cancellationToken);

    public Task UpdateStudentAsync(
        Student student,
        CancellationToken cancellationToken);
 
    public Task DeleteStudentByIdAsync(
        int studentId,
        CancellationToken cancellationToken);

    public Task<List<Student>> GetAllStudentsAsync(
        CancellationToken cancellationToken = default);

    public Task<Student> GetStudentByIdAsync(
        int studentId,
        CancellationToken cancellationToken);

}
