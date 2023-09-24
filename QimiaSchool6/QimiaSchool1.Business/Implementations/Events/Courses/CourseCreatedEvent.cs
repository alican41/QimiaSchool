

namespace QimiaSchool1.Business.Implementations.Events.Courses;

public record class CourseCreatedEvent
{
    public int CourseId { get; init; }
    public string CourseTitle{ get; init;} = string.Empty;

    public int CourseCredits { get; init; }
}
