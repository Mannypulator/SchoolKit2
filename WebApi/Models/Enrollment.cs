using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public string StudentID { get; set; }
        public int ClassSubjectID { get; set; }
        public bool CompletionState { get; set; }
        public int CA { get; set; }
        public int Exam { get; set; }

        public int Total { get; set; }
        public Grade grade { get; set; }
        public int TermID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("ClassSubjectID")]
        public virtual ClassSubject ClassSubject { get; set; }

        [ForeignKey("TermID")]
        public virtual Term Term { get; set; }
        
    }
}