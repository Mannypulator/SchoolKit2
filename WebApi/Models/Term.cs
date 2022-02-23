using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Term
    {
        public Term(){
            Fees = new HashSet<Fee>();
            Enrollments = new HashSet<Enrollment>();
            AnnualEnrollments = new HashSet<AnnualEnrollment>();
             AffectiveDomains = new HashSet<AffectiveDomain>();
            PsychomotorDomains = new HashSet<PsychomotorDomain>();
        }
        [Key]
        public int TermID { get; set; }
        public TermLabel Label { get; set; }
        
        public bool Current { get; set; }
        public bool Completed  { get; set; }
        public int SessionID { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }
        
        [ForeignKey("SessionID")]
        public Session Session { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ResultRecord> ResultRecords { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<Fee> Fees { get; set; }
        public ICollection<AnnualEnrollment> AnnualEnrollments { get; set; }
        public ICollection<AffectiveDomain> AffectiveDomains { get; set; }
        public ICollection<PsychomotorDomain> PsychomotorDomains { get; set; }
        
    }

    [NotMapped]
    public class ReturnTerm
    {
        public int TermID { get; set; }
        public int SchoolID { get; set; }
        
       
        
        
    }
}