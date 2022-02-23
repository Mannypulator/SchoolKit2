using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class AffectiveDomain
    {
        [Key]
        public int AffectiveDomainID { get; set; }
        public Grade Punctuality { get; set; }
        public Grade Attendance { get; set; }
        public Grade Neatness { get; set; }
        public Grade Honesty { get; set; }
        public Grade SelfControl { get; set; }
        public Grade Obedience { get; set; }
        public Grade Activeness { get; set; }
        public string StudentID { get; set; }
        public int TermID { get; set; }
        public ResultType Type { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("TermID")]
        public Term Term { get; set; }
        
    }

    [NotMapped]
    public class ReturnAD
    {
        
        public int AffectiveDomainID { get; set; }
        public Grade Punctuality { get; set; }
        public Grade Attendance { get; set; }
        public Grade Neatness { get; set; }
        public Grade Honesty { get; set; }
        public Grade SelfControl { get; set; }
        public Grade Obedience { get; set; }
        public Grade Activeness { get; set; }
        
        public ResultType Type { get; set; }

        
        
    }
}
 