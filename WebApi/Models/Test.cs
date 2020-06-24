using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Test
    {
        [Key]
        public int TestID { get; set; }
        [Required]
        public int Minutes { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int SchoolID { get; set; }
        [Required]
        public int ClassSubjectID { get; set; }
        [Required]
        public int TermID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        [ForeignKey("ClassSubjectID")]
        public ClassSubject ClassSubject { get; set; }
        [ForeignKey("TermID")]
        public Term Term { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestAttempt> Attempts { get; set; }


    }
}