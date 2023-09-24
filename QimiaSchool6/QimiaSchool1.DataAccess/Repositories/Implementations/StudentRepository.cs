using System.Linq.Expressions;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Repositories.Abstractions;


namespace QimiaSchool1.DataAccess.Repositories.Implementations;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    public StudentRepository(QimiaSchoolDbContext dbContext) : base(dbContext)
    {
    }

    

    

   
}
