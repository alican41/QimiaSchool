using AutoMapper;
using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Queries.Enrollment;
using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;

namespace QimiaSchool1.Business.Implementations.Handlers.Enrollments.Queries;

public class GetEnrollmentsQueryHandler : IRequestHandler<GetEnrollmentsQuery, List<EnrollmentDto>>
{
    private readonly IMapper _mapper;
    private readonly IEnrollmentManager _enrollmentManager;

    public GetEnrollmentsQueryHandler(IMapper mapper, IEnrollmentManager enrollmentManager)
    {
        _mapper = mapper;
        _enrollmentManager = enrollmentManager;
    }

    public async Task<List<EnrollmentDto>> Handle(GetEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentManager.GetAllEnrollmentsAsync(cancellationToken);

        var enrollmentDtos = _mapper.Map<List<EnrollmentDto>>(enrollments);

        return enrollmentDtos;
    }
}
