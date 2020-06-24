using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class SchoolSubject
    {
        [Key]
        public int SchoolSubjectID { get; set; }
        [Required]
        public int SchoolID { get; set; }
        [Required]
        public int SubjectID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
        
    }
}