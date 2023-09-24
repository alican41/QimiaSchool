using Microsoft.EntityFrameworkCore;
using QimiaSchool1.DataAccess.Entities;

namespace QimiaSchool1.DataAccess;

public class QimiaSchoolDbContext : DbContext
{
    public QimiaSchoolDbContext(
        DbContextOptions<QimiaSchoolDbContext> contextOptions) : base(contextOptions)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Course> Courses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .Navigation(x => x.Enrollments)
            .AutoInclude();

    }
}

