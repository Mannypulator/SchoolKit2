using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ClassArm
    {
        [Key]
        public int ClassArmID { get; set; }
        [Required]
        public int ClassID { get; set; }
        [Required]
        public Arms Arm { get; set; }

        [ForeignKey("ClassID")]
        public Class Class { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<ClassSubject> ClassSubject { get; set; }
        public virtual ICollection<ResultRecord> ResultRecords { get; set; }
    }
}
