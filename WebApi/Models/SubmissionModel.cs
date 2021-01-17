using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SubmissionModel
    {
        public int QAttemptID { get; set; }
        public int[] QAOptions { get; set; }
    }
}