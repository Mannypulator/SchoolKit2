using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class StudentFee
    {
        public long StudentFeeID { get; set; }
        public string StudentID { get; set; }
        public long FeeID { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        [ForeignKey("FeeID")]
        public Fee Fee { get; set; }

    }
}