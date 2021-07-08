using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Student: ApplicationUser
    {
        public Student(){
            Enrollments = new HashSet<Enrollment>();
            Results = new HashSet<Result>();
            TestAttempts = new HashSet<TestAttempt>();
            StudentFees = new HashSet<StudentFee>();
            AnnualEnrollments = new HashSet<AnnualEnrollment>();
            Purchases = new HashSet<ProductSale>();
        } 
        public string RegNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassArmID { get; set; }
        public int SchoolID { get; set; }

        public bool HasGraduated { get; set; }
        public  bool IsActivated { get; set; }
        public decimal Balance { get; set; }
        public String ParentID { get; set; }
        [NotMapped]
        public StudentCode Code { get; set; }

        [ForeignKey("ClassArmID")]
        public ClassArm ClassArm { get; set; }

        [ForeignKey("SchoolID")]
        public School School { get; set; }
        [ForeignKey("ParentID")]
        public Parent Parent { get; set; }

       public ICollection<Enrollment> Enrollments{ get; set; }
       public ICollection<Result> Results { get; set; }
       public ICollection<TestAttempt> TestAttempts { get; set; }
       public ICollection<StudentFee> StudentFees { get; set; }
       public ICollection<AnnualEnrollment> AnnualEnrollments { get; set; }
       public ICollection<FeePayment> Payments { get; set; }
       public ICollection<ProductSale> Purchases { get; set; }
    }
}