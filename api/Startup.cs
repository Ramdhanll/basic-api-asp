using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Context;
using api.Repository.Data;
// using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

namespace basic_api
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
         services.AddControllers();
         services.AddScoped<EmployeeRepository>();
         services.AddScoped<AccountRepository>();
         services.AddScoped<UniversityRepository>();
         services.AddScoped<ProfilingRepository>();
         services.AddScoped<EducationRepository>();
         services.AddScoped<RoleRepository>();
         services.AddScoped<AccountRoleRepository>();



         // services.AddDbContext<MyContext>(options =>
         // options.UseSqlServer(Configuration.GetConnectionString("API")));

         // kode dibawah untuk menggunakan lazyLoading
         services.AddDbContext<MyContext>(options => options.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("API")));
         services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
         );

         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = Configuration["Jwt:Issuer"],
                  ValidAudience = Configuration["Jwt:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
               };
            });

         services.AddCors(c =>
         {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());


         app.UseAuthentication();

         app.UseAuthorization();



         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
