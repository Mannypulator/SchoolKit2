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
         Enrollments  = new List<EnrollmentModel>();
        }
        public Result Result { get; set; }
        public List<EnrollmentModel> Enrollments { get; set; }
    }
}