using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class TeacherSubjectModel
    {
       
        public int ClassSubjectID { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public Arms ClassArm { get; set; }
        
    }
}