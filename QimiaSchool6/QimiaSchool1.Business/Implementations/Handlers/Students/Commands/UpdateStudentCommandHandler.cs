using MediatR;
using QimiaSchool1.Business.Implementations.Commands.Students;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.DataAccess.Exceptions;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;
using QimiaSchool1.Business.Implementations.Events.Students;

namespace QimiaSchool1.Business.Implementations.Handlers.Students.Commands;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
{
    private readonly IStudentManager _studentManager;
    private readonly IEventBus _eventBus;

    public UpdateStudentCommandHandler(IStudentManager studentManager, IEventBus eventBus)
    {
        _studentManager = studentManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {


        var existingStudent = await _studentManager.GetStudentByIdAsync(request.StudentId, cancellationToken);

        if (existingStudent == null)
        {
            throw new EntityNotFoundException<Student>(request.StudentId);
        }

        existingStudent.FirstMidName = request.Student.FirstMidName ?? string.Empty;
        existingStudent.LastName = request.Student.LastName ?? string.Empty;

        await _studentManager.UpdateStudentAsync(existingStudent, cancellationToken);

        await _eventBus.PublishAsync(new StudentUpdatedEvent
        {
            StudentId = existingStudent.StudentId,
            FirstMidName = existingStudent.FirstMidName,
            LastName = existingStudent.LastName,
        });

        return Unit.Value;
        
    }
}
