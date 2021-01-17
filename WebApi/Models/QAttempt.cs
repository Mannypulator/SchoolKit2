using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    
    public class QAttempt
    {
        public QAttempt()
        {
            QAttemptsOptions = new HashSet<QAttemptsOption>();
        }
        [Key]
        public int QuestionAttemptID { get; set; }
        public int TestAttemptID { get; set; }
        public int QuestionID { get; set; }
        public int OptSeed { get; set; }
       

        [ForeignKey("TestAttemptID")]
        public TestAttempt TestAttempt { get; set; }

        [ForeignKey("QuestionID")]
        public Question Question { get; set; }
        
      public ICollection<QAttemptsOption> QAttemptsOptions { get; set; }

      
        
    
    }
}