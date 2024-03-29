using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Teacher: ApplicationUser
    {
        public Teacher(){
            TeacherSubjects = new HashSet<TeacherSubject>();
            TeacherQualifications = new HashSet<TeacherQualification>();
            TeacherClasses = new HashSet<TeacherClass>();
            
        }
        public int SchoolID { get; set; }
        [NotMapped]
        public TeacherCode Code { get; set; }
        

        [ForeignKey("SchoolID")]
        public School School { get; set; }
        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<TeacherQualification> TeacherQualifications { get; set; }
        public ICollection<TeacherClass> TeacherClasses { get; set; }
    }

    public class ReceivedTeacher:ApplicationUser{
        public int SchoolID { get; set; }
        public ICollection<int> TeacherSubjects { get; set; }
        
    }
}