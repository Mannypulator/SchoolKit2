using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class StudentQualification
    {
        [Key]
        public int ID { get; set; }
        public string Qlf { get; set; }
        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
    }
}