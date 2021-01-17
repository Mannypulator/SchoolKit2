using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SchoolModel
    {
       public School school { get; set; }
       public Principal principal { get; set; }
    }
}