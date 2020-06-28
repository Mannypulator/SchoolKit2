using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class SSDrop
    {
        [Key]
        public int SSDropID { get; set; }
        public int SchoolID { get; set; }
        public int SubjectID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }
        [ForeignKey("SubjectID")]
        public Subject Subject { get; set; }
    }
}