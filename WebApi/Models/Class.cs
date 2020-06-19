using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        public string ClassName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<ClassSubject> ClassSubject { get; set; }
        public virtual ICollection<ResultRecord> ResultRecords { get; set; }
    }
}