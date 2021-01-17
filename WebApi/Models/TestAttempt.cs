using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class TestAttempt
    {
         public TestAttempt()
        {
            QuestionAttempts = new HashSet<QAttempt>();
            
        }
        [Key]
        public int AttemptID { get; set; }
        public int TimeSpent { get; set; }
        public DateTime StartTime { get; set; }
        public bool Finished { get; set; }
        public int Score { get; set; }
        [Required]
        public string StudentID { get; set; }
        [Required]
        public int TestID { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        [ForeignKey("TestID")]
        public Test Test { get; set; }
        public ICollection<QAttempt> QuestionAttempts { get; set; }
    }
}