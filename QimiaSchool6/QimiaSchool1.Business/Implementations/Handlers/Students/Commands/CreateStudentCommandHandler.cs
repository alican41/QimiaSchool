using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Students;
using QimiaSchool1.Business.Implementations.Events.Students;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool1.Business.Implementations.Handlers.Students.Commands;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IStudentManager _studentManager;
    private readonly IEventBus _eventBus;

    public CreateStudentCommandHandler(IStudentManager studentManager, IEventBus eventBus)
    {
        _studentManager = studentManager;
        _eventBus = eventBus;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstMidName = request.Student.FirstMidName,
            LastName = request.Student.LastName,
            EnrollmentDate = DateTime.Now,
        };

        await _studentManager.CreateStudentAsync(student, cancellationToken);

        await _eventBus.PublishAsync(new StudentCreatedEvent
        {
            StudentId = student.StudentId,
            FirstMidName = student.FirstMidName,
            LastName = student.LastName,
        });

        return student.StudentId;
    }
}
