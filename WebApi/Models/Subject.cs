using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Subject
    {
        public Subject()
        {
            ClassSubject = new HashSet<ClassSubject>();
        }
        [Key]
        public int SubjectID { get; set; }
        public string Title { get; set; }
        public ClassRange Range { get; set; }
        public bool SchoolSpecific { get; set; }

        public  ICollection<ClassSubject> ClassSubject { get; set; }
        public ICollection<SSCompulsory> SSCompulsories { get; set; }
        public ICollection<SSDrop> SSDrops { get; set; }
    }
}