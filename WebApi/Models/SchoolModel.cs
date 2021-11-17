using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [NotMapped]
    public class SchoolModel
    {
       public ReceivedSchoolModel school { get; set; }
       public ReceivedPrincipalModel principal { get; set; }
    }

    [NotMapped]
    public class ReceivedSchoolModel{
        public int SchoolID { get; set; }
        public string Name { get; set; }
        
        public string? Address { get; set; }
        public int LgaID { get; set; }
        public bool ShowPositon { get; set; }
        public string Append { get; set; }
        
        public string? ProprietorID { get; set; }
        public SchoolType Type { get; set; }

        public List<int> SsCompulsories { get; set; }
        public List<int> SsDrops { get; set; }
    }

    [NotMapped]
    public class ReceivedPrincipalModel: ApplicationUser{
        public int SchoolID { get; set; }
    }
}