using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class School
    {
        public School()
        {
            ResultRecords = new HashSet<ResultRecord>();
            Students = new HashSet<Student>();
            SSCompulsories = new HashSet<SSCompulsory>();
            Teachers = new HashSet<Teacher>();
            Principals = new HashSet<Principal>();
            Tests = new HashSet<Test>();
            SSDrops = new HashSet<SSDrop>();
        }
        [Key]
        public int SchoolID { get; set; }
        public string Name { get; set; }
        
        public string Address { get; set; }
        public int LgaID { get; set; }
        public bool ShowPositon { get; set; }
        public string Append { get; set; }
        public int StudentCount { get; set; }
        public int TeachersCount { get; set; }
        public int RegNoCount { get; set; }

        [ForeignKey("LgaID")]
        public LGA LGA { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Principal> Principals { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<SSCompulsory> SSCompulsories { get; set; }
        public ICollection<SSDrop> SSDrops { get; set; }
        public ICollection<ResultRecord> ResultRecords { get; set; }
    }
}