using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;
using System.Linq.Expressions;

namespace QimiaSchool1.DataAccess.Repositories.Implementations;

public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(QimiaSchoolDbContext dbContext) : base(dbContext)
    {
    }

    
}
