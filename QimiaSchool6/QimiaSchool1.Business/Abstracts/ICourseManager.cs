using QimiaSchool1.DataAccess.Entities;

namespace QimiaSchool1.Business.Abstracts;

public interface ICourseManager
{
    public Task CreateCourseAsync(
        Course course,
        CancellationToken cancellationToken);

    public Task UpdateCourseAsync(
        Course course,
        CancellationToken cancellationToken);

    public Task DeleteCourseByIdAsync(
        int courseId,
        CancellationToken cancellationToken);

    public Task<List<Course>> GetAllCoursesAsync(
        CancellationToken cancellationToken);

    public Task<Course> GetCourseByIdAsync(
        int courseId,
        CancellationToken cancellationToken);
}
