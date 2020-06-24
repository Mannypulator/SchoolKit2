using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class TeacherQualification
    {
        [Key]
        public int ID { get; set; }
        public string Qlf { get; set; }
        public string TeacherID { get; set; }
        [ForeignKey("TeacherID")]
        public Teacher Teacher { get; set; }
    }
}