using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Test
    {
        public Test()
        {
            Questions = new HashSet<Question>();
            Attempts = new HashSet<TestAttempt>();
        }
        [Key]
        public int TestID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool Closed { get; set; }
        [Required]
        public int Minutes { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int SchoolID { get; set; }
        [Required]
        public int ClassSubjectID { get; set; }
        public int QNPS { get; set; }
        [Required]
        public int TermID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        [ForeignKey("ClassSubjectID")]
        public ClassSubject ClassSubject { get; set; }
        [ForeignKey("TermID")]
        public Term Term { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<TestAttempt> Attempts { get; set; }


    }
}