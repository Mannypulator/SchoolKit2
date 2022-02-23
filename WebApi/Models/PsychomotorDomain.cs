using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class PsychomotorDomain
    {
        [Key]
        public int PsychomotorDomainID { get; set; }
        public Grade Handwriting { get; set; }
        public Grade Fluency { get; set; }
        public Grade Sports { get; set; }
        public Grade DrawingPainting { get; set; }
        public Grade HandlingTools { get; set; }
        public Grade Creativity { get; set; }
        public string StudentID { get; set; }
        public int TermID { get; set; }
        public ResultType Type { get; set; }
    

        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("TermID")]
        public Term Term { get; set; }
        
    }

    [NotMapped]

     public class ReturnPD
    {
     
        public int PsychomotorDomainID { get; set; }
        public Grade Handwriting { get; set; }
        public Grade Fluency { get; set; }
        public Grade Sports { get; set; }
        public Grade DrawingPainting { get; set; }
        public Grade HandlingTools { get; set; }
        public Grade Creativity { get; set; }
        
        public ResultType Type { get; set; }
    

      
        
    }
}
 