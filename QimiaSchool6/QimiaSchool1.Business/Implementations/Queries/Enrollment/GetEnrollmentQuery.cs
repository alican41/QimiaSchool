using MediatR;
using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool1.Business.Implementations.Queries.Enrollment;

public class GetEnrollmentQuery : IRequest<EnrollmentDto>
{
    public int EnrollmentId { get;}

    public GetEnrollmentQuery(int id)
    {
        EnrollmentId = id;
    }
}
