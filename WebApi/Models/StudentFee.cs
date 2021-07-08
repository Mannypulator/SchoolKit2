using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class StudentFee
    {
        public int StudentFeeID { get; set; }
        public string StudentID { get; set; }
        public int FeeID { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        [ForeignKey("FeeID")]
        public Fee Fee { get; set; }
        [NotMapped]
        public decimal Amount{ get; set; }/// for front end use
    }
}