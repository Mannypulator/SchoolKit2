using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ClassName
    {
        
        public int ClassArmID { get; set; }
        public string Name { get; set; }

        public string Arm { get; set; }
    }
}
