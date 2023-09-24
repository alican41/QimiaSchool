using System.Linq.Expressions;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;

namespace QimiaSchool1.DataAccess.Repositories.Implementations;

public class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    public CourseRepository(QimiaSchoolDbContext dbContext) : base(dbContext)
    {
    }

   
}
