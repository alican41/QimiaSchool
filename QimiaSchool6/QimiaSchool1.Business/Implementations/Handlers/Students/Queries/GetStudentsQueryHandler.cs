using AutoMapper;
using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Queries.Student;
using QimiaSchool1.Business.Implementations.Queries.Student.Dtos;

namespace QimiaSchool1.Business.Implementations.Handlers.Students.Queries;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentDto>>
{
    private readonly IMapper _mapper;
    private readonly IStudentManager _studentManager;

    public GetStudentsQueryHandler(IMapper mapper, IStudentManager studentManager)
    {
        _mapper = mapper;
        _studentManager = studentManager;
    }

    public async Task<List<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentManager.GetAllStudentsAsync(cancellationToken);

        var studentDtos = _mapper.Map<List<StudentDto>>(students);

        return studentDtos;
    }
}
