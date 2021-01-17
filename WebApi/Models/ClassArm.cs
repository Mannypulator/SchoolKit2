using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ClassArm
    {
        public ClassArm()
        {
           Students = new HashSet<Student>();
           ClassSubjects = new HashSet<ClassSubject>();
           ResultRecords = new HashSet<ResultRecord>();
        }
        
        [Key]
        public int ClassArmID { get; set; }
        [Required]
        public int ClassID { get; set; }
        [Required]
        public Arms Arm { get; set; }

        [ForeignKey("ClassID")]
        public Class Class { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<ClassSubject> ClassSubjects { get; set; }
        public ICollection<ResultRecord> ResultRecords { get; set; }
    }
}
