using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ResultRecord
    {
        [Key]
        public int ResultRecordID { get; set; }
        [Required]
        public int SchoolID { get; set; }
        [Required]
        public int ClassArmID { get; set; }
        [Required]
        public int TermID { get; set; }

        [ForeignKey("SchoolID")]
        public School School { get; set; }

        [ForeignKey("ClassArmID")]
        public ClassArm ClassArm { get; set; }

        [ForeignKey("TermID")]
        public Term Term { get; set; }
    }
}