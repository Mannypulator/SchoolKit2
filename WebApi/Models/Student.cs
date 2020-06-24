using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Student: ApplicationUser
    {
        public string RegNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassArmID { get; set; }
        public int SchoolID { get; set; }

        public bool HasGraduated { get; set; }

        [ForeignKey("ClassArmID")]
        public virtual ClassArm ClassArm { get; set; }

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }

       public virtual ICollection<Enrollment> Enrollments{ get; set; }
       public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<QuestionAttempt> QuestionAttempts { get; set; }
         public virtual ICollection<TestAttempt> TestAttempts { get; set; }
    }
}