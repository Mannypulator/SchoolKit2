using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Methods
{
    public class PrincipalMethods
    {
        public async Task<IdentityResult> Principal(Principal model, SchoolKitContext _context, UserManager<Principal> _userManager, string role)
        {
            var principal = new Principal{
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Email = model.Email,
                SchoolID = model.SchoolID,
                //LgaID = model.LgaID,
                Gender = (UserGender)model.Gender,
                UserName = model.Email,

            };

            try{
                 var result = await _userManager.CreateAsync(principal, model.PasswordHash);
                 if(result.Succeeded)
                 {
                     await _userManager.AddToRoleAsync(principal,role);

                     if(model.PrincipalQualifications != null)
                     {
                         foreach(var sub in model.PrincipalQualifications)
                       {
                         var principalQualification = new PrincipalQualification{
                             Qlf = sub.Qlf,
                             PrincipalID = principal.Id
                         };
                         await _context.PrincipalQualifications.AddAsync(principalQualification);
                       }
                     await _context.SaveChangesAsync();
                     }
                     return result;
                 }
                 else{
                     return result;
                 }
            }
            catch(Exception ex){
                throw ex;
            }
           
        }

    }
}