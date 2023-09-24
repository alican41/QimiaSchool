using MediatR;

namespace QimiaSchool1.Business.Implementations.Commands.Courses;

public class DeleteCourseCommand : IRequest<Unit>
{
    public int CourseId { get; }

    public DeleteCourseCommand(int courseId)
    {
        CourseId = courseId;
    }
}
