using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Product
    {
        public Product(){
            ProductExpenses = new HashSet<ProductExpense>();
            ProductSales = new HashSet<ProductSale>();
        }
        [Key]
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public int SchoolID { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        public ICollection<ProductExpense> ProductExpenses { get; set; }
        public ICollection<ProductSale> ProductSales { get; set; }
    }
}