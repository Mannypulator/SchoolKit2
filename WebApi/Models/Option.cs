using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
   
    public class Option
    {  public Option()
        {
            QAttemptsOptions = new HashSet<QAttemptsOption>();
        }

        [Key]
        public int OptionID { get; set; }
        public string Opt { get; set; }
        [Required]
        public int QuestionID { get; set; }

        public bool Correct { get; set; }

        [ForeignKey("QuestionID")]
        public Question Question { get; set; }

       public ICollection<QAttemptsOption> QAttemptsOptions { get; set; }
    }
}