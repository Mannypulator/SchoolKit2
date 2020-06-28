using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SubjectModel
    {
          public string Title { get; set; }
        public string Range { get; set; }
        public bool SchoolSpecific { get; set; }
    }
}