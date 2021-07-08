using System.Collections.Generic;

namespace WebApi.Models
{
    public class Parent: ApplicationUser
    {
        public ICollection<Student> Ward { get; set; } = new HashSet<Student>();
    }
}