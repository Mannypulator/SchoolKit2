using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        [Required]
        public string Qn { get; set; }
        [Required]
        public int Anwser { get; set; }
        [Required]
        public int TestID { get; set; }
        [ForeignKey("TestID")]
        public Test Test { get; set; }
        
        public virtual ICollection<QuestionAttempt> QuestionAttempts { get; set; }
        public virtual ICollection<Option> Options { get; set; }
    }
}