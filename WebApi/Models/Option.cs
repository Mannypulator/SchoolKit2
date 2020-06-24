using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Option
    {
        [Key]
        public int OptionID { get; set; }
        public string Opt { get; set; }
        [Required]
        public int QuestionID { get; set; }
        [ForeignKey("QuestionID")]
        public Question Question { get; set; }

        public virtual ICollection<QuestionAttempt> QuestionAttempts { get; set; }
    }
}