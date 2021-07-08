using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class SchoolKitContext : IdentityDbContext<ApplicationUser>
    {
        
     
      protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=SchoolKit.db");

      public DbSet<ApplicationUser> ApplicationUsers { get; set; }

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
      public DbSet <Admin> Admins { get; set; }
      public DbSet <ClassArm> ClassArms { get; set; }
      public DbSet<Test> Tests { get; set; }
      public DbSet<TestAttempt> TestAttempts { get; set; }
      public DbSet<Question> Questions { get; set; }
      public DbSet<QAttempt> QuestionAttempts { get; set; }
      public DbSet<QAttemptsOption> QAttemptsOptions { get; set; }
      
      public DbSet<Option> Options { get; set; }
      public DbSet<SSDrop> SSDrops { get; set; }
      public DbSet<Subject> Subjects { get; set; }
      public DbSet<SSCompulsory> SSCompulsories { get; set; }
      public DbSet<SchoolRegCode> SchoolRegCodes { get; set; }
      public DbSet<StudentCode> StudentCodes { get; set; }
      public DbSet<TeacherCode> TeacherCodes { get; set; }
      public DbSet<AnnualEnrollment> AnnualEnrollments { get; set; }
      public DbSet<Fee> Fees { get; set; }
      public DbSet<FeePayment> FeePayments { get; set; }
      public DbSet<ProductSale> ProductSales { get; set; }
      public DbSet<ProductExpense> ProductExpenses { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<GeneralExpense> GeneralExpenses { get; set; }
      public DbSet<StudentFee> StudentFees { get; set; }
      public DbSet<Session> Sessions { get; set; }
      public DbSet<SchoolPackage> SchoolPackages { get; set; }
      public DbSet<Parent> Parents { get; set; }
      public DbSet<Proprietor> Proprietors { get; set; }
      
    }
}