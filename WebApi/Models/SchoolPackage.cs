using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class SchoolPackage
    {
        [Key]
        public int SchoolPackageID { get; set; }
        public Package Package { get; set; }
        public int SchoolID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }
    }
}