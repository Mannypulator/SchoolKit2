namespace WebApi.Models
{
    public class AnnualEnrollmentModel
    {
        public string SubjectName { get; set; }
        public int FirstTerm { get; set; }
        public int SecondTerm { get; set; }
        public int ThirdTerm { get; set; }

        public int Total { get; set; }
        public Grade Grade { get; set; }
        
       
     }
}