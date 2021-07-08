using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class ResultModel
    {
        public ResultModel()
        {
         Enrollments  = new List<EnrollmentModel>();
         AnnualEnrollments = new List<AnnualEnrollmentModel>();
        }
        public string SessionName { get; set; }
        public string TermName { get; set; }
        public string ClassName { get; set; }
        public string ResultType { get; set; }
        public double ClassAverage { get; set; }
        public int ClassPosition { get; set; }
        public int Total { get; set; }
        public double Average { get; set; }        
        public List<EnrollmentModel> Enrollments { get; set; }
        public List<AnnualEnrollmentModel> AnnualEnrollments { get; set; }

        public string Message { get; set; }
    }
}