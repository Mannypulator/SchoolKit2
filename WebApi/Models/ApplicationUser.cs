using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? MiddleName { get; set; }
        public string? Address { get; set; }
        public UserGender Gender { get; set; }
        
       
    }
}