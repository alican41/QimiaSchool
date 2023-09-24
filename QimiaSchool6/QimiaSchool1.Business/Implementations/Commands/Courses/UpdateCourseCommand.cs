using MediatR;
using QimiaSchool1.Business.Implementations.Commands.Courses.Dtos;

namespace QimiaSchool1.Business.Implementations.Commands.Courses;

public class UpdateCourseCommand : IRequest<Unit>
{
    public UpdateCourseDto Course { get; set; }

    public int CourseId { get;}

    public UpdateCourseCommand(UpdateCourseDto course, int courseId)
    {
        Course = course;
        CourseId = courseId;
    }
}
