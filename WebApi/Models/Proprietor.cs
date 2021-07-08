using System.Collections.Generic;

namespace WebApi.Models
{
    public class Proprietor: ApplicationUser
    {
        public ICollection<School> Schools { get; set; } = new HashSet<School>();
    }
}