using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Term
    {
        public Term(){
            Results = new HashSet<Result>();
            Fees = new HashSet<Fee>();
            Enrollments = new HashSet<Enrollment>();
            AnnualEnrollments = new HashSet<AnnualEnrollment>();

        }
        [Key]
        public int TermID { get; set; }
        public TermLabel Label { get; set; }
        public string Session { get; set; }
        public bool Current { get; set; }
        public int SchoolID { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Result> Results { get; set; }
        public ICollection<ResultRecord> ResultRecords { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<Fee> Fees { get; set; }
        public ICollection<AnnualEnrollment> AnnualEnrollments { get; set; }
        
    }
}