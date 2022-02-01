using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        [Required]
        public string StudentID { get; set; }
        public int ClassPosition { get; set; }
        public int ResultRecordID { get; set; }
        public int Total { get; set; }
        public string TeacherComment { get; set; }
        public string PrincipalComment { get; set; }
        
        public double Average { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        
        [ForeignKey("ResultRecordID")]
        public ResultRecord ResultRecord { get; set; }
        
    }
}