using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class SchoolKitContext : IdentityDbContext
    {
        
       SchoolKitContext(DbContextOptions options):base(options)
      {
          
      }

      public DbSet<ApplicationUser> ApplicationUsers { get; set; }
      public DbSet<Class> Classes { get; set; }
      public DbSet<ClassSubject> ClassSubjects { get; set; }
      public DbSet<Enrollment> Enrollments { get; set; }
      public DbSet<LGA> LGAs { get; set; }
      public DbSet<Principal> Principals { get; set; }
      public DbSet<PrincipalQualification> PrincipalQualifications { get; set; }
      public DbSet<Result> Results { get; set; }
      public DbSet<ResultRecord> ResultRecords { get; set; }
      public DbSet<School> Schools { get; set; }
      public DbSet<State> States { get; set; }
      public DbSet<Student> Students { get; set; }
      public DbSet<Teacher> Teachers { get; set; }
      public DbSet<TeacherQualification> TeacherQualifications { get; set; }
      public DbSet<TeacherSubject> TeacherSubjects { get; set; }
      public DbSet<Term> Terms { get; set; }
    }
}