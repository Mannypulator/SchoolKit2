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
        public State State { get; set; }
        public ICollection<School> Schools { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}