using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class RQAttemptModel
    {
        public int QAttemptID { get; set; }
        public Question Question { get; set; }
    }
}