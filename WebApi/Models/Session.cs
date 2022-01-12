using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Session
    {
        public Session (){
            Terms = new HashSet<Term>();
        }
        [Key]
        public int SessionID { get; set; }
        public string SessionName { get; set; }
        public DateTime SessionStart { get; set; }
        public int SchoolID { get; set; }
        public bool Current { get; set; }
        public bool Completed { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        public ICollection<Term> Terms { get; set; }
    }

    [NotMapped]
    public class SessionModel
    {
        public string SessionName { get; set; }
        public int SchoolID { get; set; }
       
    }
}