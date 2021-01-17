using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using WebApi.Middlewares;
using WebApi.Models;


namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
              services.AddControllers()
             .AddNewtonsoftJson(options =>{
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                       
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
             });
            
            services.AddEntityFrameworkSqlite().AddDbContext<SchoolKitContext>();
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>{
                  options.Password.RequireDigit = false;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequireUppercase = false;
                  options.Password.RequiredLength = 6;
                  options.User.RequireUniqueEmail = false;
                  
                  
                  
            })
            .AddEntityFrameworkStores<SchoolKitContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityCore<Student>(options =>{
                options.User.RequireUniqueEmail = false;
            })
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Student, IdentityRole>>()
            .AddEntityFrameworkStores<SchoolKitContext>()
            .AddDefaultTokenProviders();
            

             services.AddIdentityCore<Teacher>(options =>{
               // options.User.RequireUniqueEmail = true;
            
            })
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Teacher, IdentityRole>>()
            .AddEntityFrameworkStores<SchoolKitContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityCore<Principal>(options =>{
               // options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Principal, IdentityRole>>()
            .AddEntityFrameworkStores<SchoolKitContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityCore<Admin>(options =>{
                //options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Admin, IdentityRole>>()
            .AddEntityFrameworkStores<SchoolKitContext>()
            .AddDefaultTokenProviders();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            
            services.AddAuthentication(x =>{
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseStudentStatus();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
