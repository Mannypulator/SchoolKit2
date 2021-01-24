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
            Fees = new HashSet<Fee>();
            Products = new HashSet<Product>();
            GeneralExpenses = new HashSet<GeneralExpense>();
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
        public bool SessionStart { get; set; }
        
        public string? AdminID { get; set; }
        [NotMapped]
        public string Code { get; set; }

        [ForeignKey("AdminID")]
        public Admin Admin { get; set; }

        [ForeignKey("LgaID")]
        public LGA LGA { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Principal> Principals { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<SSCompulsory> SSCompulsories { get; set; }
        public ICollection<SSDrop> SSDrops { get; set; }
        public ICollection<ResultRecord> ResultRecords { get; set; }
        public ICollection<Fee> Fees { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<GeneralExpense> GeneralExpenses { get; set; }
        
    }
}