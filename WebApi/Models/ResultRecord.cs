using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ResultRecord
    {
        public ResultRecord(){
            Results = new HashSet<Result>();
        }
        [Key]
        public int ResultRecordID { get; set; }
        public ResultType Type { get; set; }
        [Required]
        public int ClassArmID { get; set; }
        public double ClassAverage { get; set; }
        [Required]
        public int TermID { get; set; }

        [ForeignKey("ClassArmID")]
        public ClassArm ClassArm { get; set; }

        [ForeignKey("TermID")]
        public Term Term { get; set; }
        public int ClassCount { get; set; }

        public ICollection<Result> Results { get; set; }
    }

    public class ResultList
    {
        public ResultType Type { get; set; }
        public TermLabel Label { get; set; }

        public Arms Arm { get; set; }
        public Class Class{ get; set; }
        public int Result { get; set; }
       

        

        
    }
}