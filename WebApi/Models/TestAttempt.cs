using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class TestAttempt
    {
        [Key]
        public int AttemptID { get; set; }
        public int TimeSpent { get; set; }
        public int Score { get; set; }
        [Required]
        public string StudentID { get; set; }
        [Required]
        public int TestID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        [ForeignKey("TestID")]
        public Test Test { get; set; }
    }
}