using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class LGA
    {
        [Key]
        public int LgaID { get; set; }

        public string Name { get; set; }
        public int StateID { get; set; }
        [ForeignKey("StateID")]
        public virtual State State { get; set; }
        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}