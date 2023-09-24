using AutoMapper;
using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Queries.Enrollment;
using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;

namespace QimiaSchool1.Business.Implementations.Handlers.Enrollments.Queries;

public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDto>
{
    private readonly IMapper _mapper;
    private readonly IEnrollmentManager _enrollmentManager;

    public GetEnrollmentQueryHandler(IMapper mapper, IEnrollmentManager enrollmentManager)
    {
        _mapper = mapper;
        _enrollmentManager = enrollmentManager;
    }

    public async Task<EnrollmentDto> Handle(
        GetEnrollmentQuery request,
        CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.EnrollmentId, cancellationToken);

        return _mapper.Map<EnrollmentDto>(enrollment);
    }
}
