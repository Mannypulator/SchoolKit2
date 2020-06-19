using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ClassSubject
    {
        [Key]
        public int ClassSubjectID { get; set; }

        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public string Description { get; set; }
        public bool isCompulsory { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }
         [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}