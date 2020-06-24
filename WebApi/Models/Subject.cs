using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        public string Title { get; set; }
        public ClassRange Range { get; set; }
        public bool SchoolSpecific { get; set; }

        public virtual ICollection<ClassSubject> ClassSubject { get; set; }
    }
}