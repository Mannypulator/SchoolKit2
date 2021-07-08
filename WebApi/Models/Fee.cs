using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Fee
    {
        public Fee(){
            StudentFees = new HashSet<StudentFee>();
        }
        [Key]
        public int FeeID { get; set; }
        public string FeeName { get; set; }
        public FeeType FeeType { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public decimal TotalAmountOwed { get; set; }
        public decimal TotalAmountExpeced { get; set; }
        public int TermID { get; set; }
        
        [ForeignKey("TermID")]
        public Term Term { get; set; }
        public ICollection<StudentFee> StudentFees { get; set; }
        public ICollection<FeePayment> FeePayments { get; set; }

        [NotMapped]
        public int ClassArmID { get; set; }
        [NotMapped]
        public Class Class { get; set; }
        [NotMapped]
        public List<Student> Students { get; set; }
    }
}
