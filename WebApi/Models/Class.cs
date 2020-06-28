using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        [Required]

        public string ClassName { get; set; }
        

        public ICollection<ClassArm> ClassArms { get; set; }
    }
}