using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class School
    {
        [Key]
        public int SchoolID { get; set; }
        public string Name { get; set; }
        
        public string Address { get; set; }
        public int LgaID { get; set; }

        [ForeignKey("LgaID")]
        public virtual LGA LGA { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Principal> Principals { get; set; }
        public virtual ICollection<ResultRecord> ResultRecords { get; set; }
    }
}