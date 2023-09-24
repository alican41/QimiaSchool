using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;

namespace QimiaSchool1.Business.Implementations;

public class EnrollmentManager : IEnrollmentManager
{

    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentManager(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }


    public async Task CreateEnrollmentAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken)
    {
        enrollment.EnrollmentId = default;

        await _enrollmentRepository.CreateAsync(enrollment, cancellationToken);
    }

    public async Task UpdateEnrollmentAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken)
    {
       

        await _enrollmentRepository.UpdateAsync( enrollment, cancellationToken);
    }

    public async Task DeleteEnrollmentByIdAsync(
        int enrollmentId,
        CancellationToken cancellationToken)
    {
        

        await _enrollmentRepository.DeleteByIdAsync(enrollmentId, cancellationToken);
    }

    public async Task<List<Enrollment>> GetAllEnrollmentsAsync(CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
      
        return enrollments;
    }
    public async Task<Enrollment> GetEnrollmentByIdAsync(
        int enrollmentId, 
        CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(
            enrollmentId,
            cancellationToken);

        return enrollment;
    }
}
