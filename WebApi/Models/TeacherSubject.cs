using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class TeacherSubject
    {
        [Key]
        public int TeacherSubjectID { get; set; }
        public int ClassSubjectID { get; set; }

        public string TeacherID { get; set; }
       
        [ForeignKey("ClassSubjectID")]
        public ClassSubject ClassSubject  { get; set; }

        [ForeignKey("TeacherID")]
        public Teacher Teacher { get; set; } = null!;
    }
}