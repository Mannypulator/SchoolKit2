using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    
    public class QAttemptsOption
    {
         [Key]
        public int QAttemptsOptionID { get; set; }
        public int QuestionAttemptID { get; set; }
        public int OptionID { get; set; }

        [ForeignKey("QuestionAttemptID")]
        public QAttempt QuestionAttempt { get; set; }

        [ForeignKey("OptionID")]
        public Option Option { get; set; }
        
       
    }
}