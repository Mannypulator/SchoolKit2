using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class AnnualEnrollment
    {
        [Key]
        public int AnnualEnrollmentID { get; set; }
        
        public string StudentID { get; set; }
        public int ClassSubjectID { get; set; }
        public int FirstTerm { get; set; }
        public int SecondTerm { get; set; }
        public int ThirdTerm { get; set; }

        public int Total { get; set; }
        public Grade Grade { get; set; }
        
        public int TermID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("ClassSubjectID")]
        public ClassSubject ClassSubject { get; set; }

        [ForeignKey("TermID")]
        public Term Term { get; set; }
        
    }
}