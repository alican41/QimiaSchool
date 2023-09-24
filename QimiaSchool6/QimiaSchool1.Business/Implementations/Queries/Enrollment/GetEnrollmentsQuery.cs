using MediatR;
using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;

namespace QimiaSchool1.Business.Implementations.Queries.Enrollment;

public class GetEnrollmentsQuery : IRequest<List<EnrollmentDto>>
{
}
