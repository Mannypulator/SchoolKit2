using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class AttemptsModel
    {
         
        public int AttemptID { get; set; }
        public int TimeSpent { get; set; }
        public int Score { get; set; }
        
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        
    }
}