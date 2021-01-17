using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Admin: ApplicationUser
    {
        public Admin() {
            Schools = new HashSet<School>();
        } 
        public ICollection<School> Schools { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}