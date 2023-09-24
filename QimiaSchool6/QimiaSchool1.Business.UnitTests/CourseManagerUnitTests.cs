using Moq;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;

namespace QimiaSchool1.Business.UnitTests;

[TestFixture]
internal class CourseManagerUnitTests
{
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly Mock<ICacheService> _cacheService;
    private readonly CourseManager _courseManager;
    
    public CourseManagerUnitTests()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _cacheService = new Mock<ICacheService>();
        _courseManager = new CourseManager( _mockCourseRepository.Object , _cacheService.Object);
    }

    [Test]
    public async Task CreateCourseAsync_WhenCalled_CallsRepository()
    {
        var testCourse = new Course
        {
            CourseTitle = "Test",
            CourseCredits = 5,
        };

        await _courseManager.CreateCourseAsync(testCourse, default);

        _mockCourseRepository
            .Verify(
            sr => sr.CreateAsync(
                It.Is<Course>(s => s == testCourse),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateCourseAsync_WhenCourseIdHasValue_RemovesAndCallsRepository()
    {
        var testCourse = new Course
        {
            CourseId = 1,
            CourseTitle = "Test",
            CourseCredits = 5,
        };

        await _courseManager.CreateCourseAsync(testCourse, default);

        _mockCourseRepository
            .Verify(
            sr => sr.CreateAsync(
                    It.Is <Course>(s => s == testCourse),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}
