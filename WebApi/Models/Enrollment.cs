using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        [Required]
        public string StudentID { get; set; }
        [Required]
        public int ClassSubjectID { get; set; }
        public bool CompletionState { get; set; }
        public int CA { get; set; }
        public int Exam { get; set; }

        public int Total { get; set; }
        public Grade Grade { get; set; }
        [Required]
        public int TermID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [ForeignKey("ClassSubjectID")]
        public ClassSubject ClassSubject { get; set; }

        [ForeignKey("TermID")]
        public Term Term { get; set; }
        
    }
}