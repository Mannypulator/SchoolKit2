using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Proprietor: ApplicationUser
    {
        public ICollection<School> Schools { get; set; } = new HashSet<School>();
       
    }
}