using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class EnrollmentModel
    {
        public string SubjectName { get; set; }
        public int CA { get; set; }
        public int Exam { get; set; }

        public int Total { get; set; }
        public Grade Grade { get; set; }
        
       
     }
}