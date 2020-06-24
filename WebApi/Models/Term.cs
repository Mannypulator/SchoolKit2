using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Term
    {
        [Key]
        public int TermID { get; set; }
        public TermLabel Label { get; set; }
        public string Session { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<ResultRecord> ResultRecords { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}