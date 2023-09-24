

namespace QimiaSchool1.Business.Implementations.Events.Courses;

public record class CourseUpdatedEvent
{
    public int CourseId { get; init; }
    public string CourseTitle { get; init; } = string.Empty;

    public int CourseCredits { get; init; }
}
