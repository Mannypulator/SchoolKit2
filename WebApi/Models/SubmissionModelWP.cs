using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SubmissionModelWP
    {
        public SubmissionModel[] model { get; set; }
        public int PageNo { get; set; }
        public int TestID { get; set; }
    }
}