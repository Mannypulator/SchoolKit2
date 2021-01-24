using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class GeneralExpense
    {
        [Key]
        public long GeneralExpensesID { get; set; }
        public decimal Expense { get; set; }
        public string Description { get; set; }
        public int SchoolID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }

    }
}