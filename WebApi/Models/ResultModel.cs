using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class ResultModel
    {
        public ResultModel()
        {
         Result = new Result();
         Enrollments  = new List<Enrollment>();
        }
        public Result Result { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}