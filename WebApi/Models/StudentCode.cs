using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class StudentCode
    {
       [Key]
       public int CodeID { get; set; }
       public string Code { get; set; }
       public int SchoolID { get; set; }
       public int ClassArmID { get; set; }
       public DateTime Date { get; set; }
    }
}