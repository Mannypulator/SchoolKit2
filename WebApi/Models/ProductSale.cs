using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ProductSale
    {
        [Key]
        public int ProductSalesID { get; set; }
        public int ProductID { get; set; }
        public string? StudentID { get; set; }
        public decimal? Discount { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        
    }
}