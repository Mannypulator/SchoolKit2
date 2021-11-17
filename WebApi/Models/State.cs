using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LGA> LGAs { get; set; }
        
    }

    [NotMapped]
     public class StateModel
    {
        public int StateID { get; set; }
    }
}