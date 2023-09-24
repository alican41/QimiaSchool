using Moq;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;


namespace QimiaSchool1.Business.UnitTests;

[TestFixture]
internal class StudentManagerUnitTests
{
    private readonly Mock<IStudentRepository> _mockStudentRepository;
    private readonly Mock<ICacheService> _cacheService;
    private readonly StudentManager _studentManager;

    public StudentManagerUnitTests()
    {
        _mockStudentRepository =new Mock<IStudentRepository>();
        _cacheService = new Mock<ICacheService>();
        _studentManager = new StudentManager(_mockStudentRepository.Object, _cacheService.Object);

    }

    [Test]
    public async Task CreateStudentAsync_WhenCalled_CallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateStudentAsync_WhenStudentIdHasValue_RemovesAndCallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            StudentId = 1,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

}
