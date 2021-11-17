using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SubjectAssign
    {
        public string TeacherID { get; set; }
        public IList<int> ClassSubjectIDs { get; set; }
       
    }
}