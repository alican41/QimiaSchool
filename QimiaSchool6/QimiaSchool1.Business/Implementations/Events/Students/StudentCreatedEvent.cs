
namespace QimiaSchool1.Business.Implementations.Events.Students;

public record class StudentCreatedEvent
{
    public int StudentId { get; init; }

    public string FirstMidName { get; init;} = string.Empty;

    public string LastName { get; init; } = string.Empty;
}
