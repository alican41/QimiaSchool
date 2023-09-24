using Serilog;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;


namespace QimiaSchool1.Business.Implementations;

public class StudentManager : IStudentManager
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICacheService _cacheService;
    public StudentManager(IStudentRepository studentRepository, ICacheService cacheService)
    {
        _studentRepository = studentRepository;
        _cacheService = cacheService;
    }

    public async Task CreateStudentAsync(
        Student student,
        CancellationToken cancellationToken)
    {
        // No id should be provided while insert.
        student.StudentId = default;

        // Serilog
        Log.Information("Create request accepted by controller.");


        await _studentRepository.CreateAsync(student, cancellationToken);
    }

    public async Task UpdateStudentAsync(
        Student student,
        CancellationToken cancellationToken)
    {


        var cacheKey = $"student-{student.StudentId}";

        var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);

        if (cachedStudent != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _studentRepository.UpdateAsync(student, cancellationToken);

    }

    public async Task DeleteStudentByIdAsync(
        int studentId,
        CancellationToken cancellationToken)
    {


        var cacheKey = $"student-{studentId}";

        var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);

        if (cachedStudent != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _studentRepository.DeleteByIdAsync(studentId, cancellationToken);

    }

    public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAllAsync(cancellationToken);
      
        return students;
    }

    public async Task<Student> GetStudentByIdAsync(
        int studentId,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"student-{studentId}";

        var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);

        if (cachedStudent != null)
        {
            return cachedStudent;
        }

        var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);

        await _cacheService.SetAsync(cacheKey, student, TimeSpan.FromMinutes(5), cancellationToken);

        return student;

    }


}

