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
        public int ResultID { get; set; }
        public TermLabel TermName { get; set; }
        public string ClassName { get; set; }
        public int ClassCount { get; set; }
        public string ResultType { get; set; }
        public string ClassAverage { get; set; }
        public int ClassPosition { get; set; }
        public int Total { get; set; }
        public string Average { get; set; }   
        public string StudentName { get; set; }  
        public string PrincipalComment { get; set; }  
        public string TeacherComment { get; set; } 
        public string RegNo { get; set; }
        public List<AffectiveDomain> AD { get; set; }
        public List<PsychomotorDomain> PD { get; set; }
        public UserGender Gender { get; set; }
        public List<EnrollmentModel> Enrollments { get; set; }
        public List<AnnualEnrollmentModel> AnnualEnrollments { get; set; }
        

        public string Message { get; set; }
    }
}