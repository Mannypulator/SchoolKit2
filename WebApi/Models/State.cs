using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LGA> LGAs { get; set; }
        
    }
}