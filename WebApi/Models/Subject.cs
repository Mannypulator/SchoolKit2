using System.Collections.Generic;

namespace WebApi.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ClassSubject> ClassSubject { get; set; }
    }
}