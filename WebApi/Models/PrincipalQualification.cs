using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class PrincipalQualification
    {
        [Key]
        public int ID { get; set; }
        public string Qlf { get; set; }
        public string PrincipalID { get; set; }
        [ForeignKey("PrincipalID")]
        public Principal Principal { get; set; }
    }
}