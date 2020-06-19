using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ResultRecord
    {
        public int ResultRecordID { get; set; }
        public int SchoolID { get; set; }
        public int ClassID { get; set; }
        public int TermID { get; set; }

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }

        [ForeignKey("TermID")]
        public virtual Term Term { get; set; }
    }
}