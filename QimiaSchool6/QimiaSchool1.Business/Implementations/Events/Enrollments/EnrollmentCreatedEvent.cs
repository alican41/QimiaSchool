
namespace QimiaSchool1.Business.Implementations.Events.Enrollments;

public record class EnrollmentCreatedEvent
{
    public int EnrollmentId { get; init; }
    public int StudentId { get; init; }
    public int CourseId { get; init; }
}
