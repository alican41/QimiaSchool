using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;

namespace QimiaSchool1.Business.Implementations;

public class CourseManager : ICourseManager
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICacheService _cacheService;

    public CourseManager(ICourseRepository courseRepository, ICacheService cacheService)
    {
        _courseRepository = courseRepository;
        _cacheService = cacheService;
    }

    public async Task CreateCourseAsync(
        Course course,
        CancellationToken cancellationToken)
    {
        course.CourseId = default;

        await _courseRepository.CreateAsync(course, cancellationToken);
    }

    public async Task UpdateCourseAsync(
        Course course,
        CancellationToken cancellationToken)
    {


        var cacheKey = $"course-{course.CourseId}";

        var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);

        if (cachedCourse != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _courseRepository.UpdateAsync(course, cancellationToken);

    }

    public async Task DeleteCourseByIdAsync(
        int courseId,
        CancellationToken cancellationToken)
    {


        var cacheKey = $"course-{courseId}";

        var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);

        if (cachedCourse != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _courseRepository.DeleteByIdAsync(courseId, cancellationToken);

    }

    public async Task<List<Course>> GetAllCoursesAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        return courses;
    }

    public async Task<Course> GetCourseByIdAsync(
        int courseId,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"course-{courseId}";

        var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);

        if (cachedCourse != null)
        {
            return cachedCourse;
        }

        var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);

        await _cacheService.SetAsync(cacheKey, course, TimeSpan.FromMinutes(5), cancellationToken);

        return course;

    }
}
