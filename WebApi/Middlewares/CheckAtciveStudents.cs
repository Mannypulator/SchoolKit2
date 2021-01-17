using System;
using System.Diagnostics;  
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;

namespace WebApi.Middlewares
{
    public class StudentStatus
{
    private readonly RequestDelegate _next;
    private HttpContext _context;
   
    public StudentStatus(RequestDelegate next, IHttpContextAccessor contextAccessor)
    {
        _next = next;
       
        _context = contextAccessor.HttpContext;
        
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try{
            string userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            var _user = httpContext.RequestServices.GetService<UserManager<Student>>();
            var student = await _user.FindByIdAsync(userId);
            if(student != null)
            {
                if(!student.IsActivated)
                {
                    httpContext.Response.StatusCode = 401;  
                    httpContext.Response.ContentType = "application/json";
                    string jsonString = JsonConvert.SerializeObject(new {message = "Your account is not activated"});
                    await httpContext.Response.WriteAsync(jsonString);
                    return;
                }
            }  
        }
        catch(NullReferenceException){
        }   
        await _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class StudentStatusExtensions
{
    public static IApplicationBuilder UseStudentStatus(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StudentStatus>();
    }
} 
}
