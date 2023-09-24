using Moq;
using NUnit.Framework.Internal;
using QimiaSchool1.Business.Implementations;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;

namespace QimiaSchool1.Business.UnitTests;

internal class EnrollmentManagerUnitTests
{
    private readonly Mock<IEnrollmentRepository> _enrollmentRepositoryMock;
    private readonly EnrollmentManager _enrollmentManager;

    public EnrollmentManagerUnitTests()
    {
        _enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
        _enrollmentManager = new EnrollmentManager( _enrollmentRepositoryMock.Object );
    }

    [Test]
    public async Task CreateEnrollmentAsync_WhenCalled_CallsRepository()
    {
        var testEnrollment = new Enrollment
        {
            StudentId = 1,
            CourseId = 2,
        };

        await _enrollmentManager.CreateEnrollmentAsync(testEnrollment, default);

        _enrollmentRepositoryMock
            .Verify(
        sr => sr.CreateAsync(
                It.Is <Enrollment>(s => s == testEnrollment),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateEnrollmentAsync_WhenEnrollmentIdHasValue_RemovesAndCallsRepository()
    {
        var testEnrollment = new Enrollment
        {
            EnrollmentId = 1,
            StudentId = 2,
            CourseId = 3,
        };

        await _enrollmentManager.CreateEnrollmentAsync(testEnrollment, default);

        _enrollmentRepositoryMock 
            .Verify(
            sr => sr.CreateAsync(
                It.Is <Enrollment>(s => s == testEnrollment),
                It.IsAny<CancellationToken>()), Times.Once);
    }
}
