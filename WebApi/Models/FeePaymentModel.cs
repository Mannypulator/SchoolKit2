using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class FeePaymentModel
    {
        
        public FeePayment FeePayment { get; set; }
        public StudentFee StudentFee { get; set; }
    }
    
}