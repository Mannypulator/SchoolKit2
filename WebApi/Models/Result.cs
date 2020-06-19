using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Result
    {
        public int ResultID { get; set; }
        public int StudentID { get; set; }
        public int ClassPosition { get; set; }
        public int TermID { get; set; }
        public double Average { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        [ForeignKey("TermID")]
        public virtual Term Term { get; set; }
    }
}