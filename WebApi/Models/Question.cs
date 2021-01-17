using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{

    public class Question
    {
        public Question()
        {
            Options = new HashSet<Option>();
        }
        [Key]
        public int QuestionID { get; set; }
        [Required]
        public string Qn { get; set; }

        public QuestionType QType{ get; set; }
        public int CorrectOptions { get; set; }

        public  double Mark { get; set; }
        public bool AllOptionsCorrect { get; set; } //to decide whether or not all options should be correct
                                                    //before the  question's mark is awarded to the question
       
        [Required]
        public int TestID { get; set; }
        [ForeignKey("TestID")]
        public Test Test { get; set; }
        public ICollection<Option> Options { get; set; }
        public virtual ICollection<QAttempt> QuestionAttempts { get; set; }
    }
}