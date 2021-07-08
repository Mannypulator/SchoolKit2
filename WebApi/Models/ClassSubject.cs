using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ClassSubject
    {
         
        public ClassSubject()
        {
            Enrollments = new HashSet<Enrollment>();
            TeacherSubjects = new HashSet<TeacherSubject>();
            Tests = new HashSet<Test>();
            AnnualEnrollments = new HashSet<AnnualEnrollment>();
        }
        [Key]
        public int ClassSubjectID { get; set; }
        [Required]
        public int ClassArmID { get; set; }
        [Required]
        public int SubjectID { get; set; }
        public bool isCompulsory { get; set; }

        [ForeignKey("ClassArmID")]
        public virtual ClassArm ClassArm { get; set; }
         [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }

        public  ICollection<Enrollment> Enrollments { get; set; }
        public  ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<AnnualEnrollment> AnnualEnrollments { get; set; }
    }
}