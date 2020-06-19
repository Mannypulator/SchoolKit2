using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Principal: ApplicationUser
    {
        public int SchoolID { get; set; }
        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
        public virtual ICollection<PrincipalQualification> PrincipalQualifications { get; set; }
    }
}