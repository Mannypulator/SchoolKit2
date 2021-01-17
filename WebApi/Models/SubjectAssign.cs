using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SubjectAssign
    {
        public string TeacherID { get; set; } = null!;
        public int ClassSubjectID { get; set; }
        public int SchoolID { get; set; }
    }
}