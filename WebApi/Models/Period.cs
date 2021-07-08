using System;

namespace WebApi.Models
{
    public class Period
    {
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public int SchoolID { get; set; }
        public Order Order { get; set; }
    }
}