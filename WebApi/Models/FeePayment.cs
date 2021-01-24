using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class FeePayment
    {
        [Key]
        public long FeePaymentID { get; set; }
        public long FeeID { get; set; }
        public decimal AmountPaid { get; set; }
        public string StudentID { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("FeeID")]
        public Fee Fee { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
    }
}