using System.Collections.Generic;

namespace WebApi.Models
{
    public class Term
    {
        public int TermID { get; set; }
        public TermLabel Label { get; set; }
        public string Session { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<ResultRecord> ResultRecords { get; set; }
    }
}