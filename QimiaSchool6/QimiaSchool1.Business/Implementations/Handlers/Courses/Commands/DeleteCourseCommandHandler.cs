using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Courses;

namespace QimiaSchool1.Business.Implementations.Handlers.Courses.Commands;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly ICourseManager _courseManager;

    public DeleteCourseCommandHandler(ICourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _courseManager.DeleteCourseByIdAsync(request.CourseId, cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {

        }
        return Unit.Value;
    }
}
