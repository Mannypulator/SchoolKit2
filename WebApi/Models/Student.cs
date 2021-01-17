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
        } 
        public string RegNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassArmID { get; set; }
        public int SchoolID { get; set; }

        public bool HasGraduated { get; set; }
        public  bool IsActivated { get; set; }
        [NotMapped]
        public StudentCode Code { get; set; }

        [ForeignKey("ClassArmID")]
        public virtual ClassArm ClassArm { get; set; }

        [ForeignKey("SchoolID")]
        public School School { get; set; }

       public ICollection<Enrollment> Enrollments{ get; set; }
       public ICollection<Result> Results { get; set; }
       public ICollection<TestAttempt> TestAttempts { get; set; }
    }
}