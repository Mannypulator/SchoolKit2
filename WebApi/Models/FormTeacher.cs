using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class TeacherClass
    {
        [Key]
        public int TeacherClassID { get; set; }
        public int ClassArmID { get; set; }
        public string TeacherID { get; set; }

        [ForeignKey("ClassArmID")]
        public ClassArm ClassArm { get; set; }
        [ForeignKey("TeacherID")]
        public Teacher Teacher { get; set; }

        
       
     }
}