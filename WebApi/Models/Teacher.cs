using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Teacher: ApplicationUser
    {
        public int SchoolID { get; set; }
        

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public virtual ICollection<TeacherQualification> TeacherQualifications { get; set; }
    }
}