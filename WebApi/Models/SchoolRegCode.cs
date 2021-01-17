using System.ComponentModel.DataAnnotations;
namespace WebApi.Models
{
    public class SchoolRegCode
    {
         [Key]
        public int CodeId { get; set; }

        public string Code { get; set; }
    }
}