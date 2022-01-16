using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    
    public class ScoreScheme
    {
        [Key]
        public int ScoreShemeID { get; set; }
        public int SchoolID { get; set; }
        public int Test1 { get; set; }
        public int Test2 { get; set; }
        public int Test3 { get; set; }
        public int Exam { get; set; }
        public int MinA { get; set; }
        public int MaxA { get; set; }
        public int MinB { get; set; }
        public int MaxB { get; set; }
        public int MinC { get; set; }
        public int MaxC { get; set; }
        public int MinD { get; set; }
        public int MaxD { get; set; }
        public int MinE { get; set; }
        public int MaxE { get; set; }
        public int MinP { get; set; }
        public int MaxP { get; set; }
        public int MinF { get; set; }
        public int MaxF { get; set; }

        [ForeignKey("SchoolID")]
        public School School { get; set; }
        
    }

    public class TestScheme
    {
        public int SchoolID { get; set; }
        public int Test1 { get; set; }
        public int Test2 { get; set; }
        public int Test3 { get; set; }
        public int Exam { get; set; }
    }

    public class GradeScheme
    {
        public int SchoolID { get; set; }
        public int MinA { get; set; }
        public int MaxA { get; set; }
        public int MinB { get; set; }
        public int MaxB { get; set; }
        public int MinC { get; set; }
        public int MaxC { get; set; }
        public int MinD { get; set; }
        public int MaxD { get; set; }
        public int MinE { get; set; }
        public int MaxE { get; set; }
        public int MinP { get; set; }
        public int MaxP { get; set; }
        public int MinF { get; set; }
        public int MaxF { get; set; }
    }

     public class TestGradeScheme
    {
       public TestScheme Tests { get; set; }
       public GradeScheme Grades { get; set; }
    }


}