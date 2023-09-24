using QimiaSchool1.DataAccess.Entities;

namespace QimiaSchool1.Business.Abstracts;

public interface IEnrollmentManager
{
    public Task CreateEnrollmentAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken);

    public Task UpdateEnrollmentAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken);

    public Task DeleteEnrollmentByIdAsync(
        int enrollmentId,
        CancellationToken cancellationToken);

    public Task<List<Enrollment>> GetAllEnrollmentsAsync(
        CancellationToken cancellationToken);

    public Task<Enrollment> GetEnrollmentByIdAsync(
        int enrollmentId,
        CancellationToken cancellationToken);
}
