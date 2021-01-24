using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        [Required]
        public string StudentID { get; set; }
        public ResultType Type { get; set; }
        public double ClassAverage { get; set; }
        public int ClassPosition { get; set; }
        [Required]
        public int TermID { get; set; }
        
        public int Total { get; set; }
        public double Average { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        [ForeignKey("TermID")]
        public virtual Term Term { get; set; }
    }
}