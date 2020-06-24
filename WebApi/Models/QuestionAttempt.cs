using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class QuestionAttempt
    {
        [Key]
        public int QuestionAttemptID { get; set; }
        [Required]
        public int OptionID { get; set; }
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public string StudentID { get; set; }
        [ForeignKey("OptionID")]
        public virtual Option Option { get; set; }
        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
    }
}