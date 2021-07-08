using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ProductExpense
    {
        [Key]
        public int ProductExpenseID { get; set; }
        public int ProductID { get; set; }
        public decimal Expense { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}