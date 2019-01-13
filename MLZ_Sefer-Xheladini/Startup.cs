using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MLZ_Sefer_Xheladini.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace MLZ_Sefer_Xheladini
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

  services
    .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
    .AddBasicAuthentication(
      options =>
      {
        options.Realm = "My Application";
        options.Events = new BasicAuthenticationEvents
        {
          OnValidatePrincipal = context =>
          {
            if ((context.UserName.ToLower() == "name") 
                    && (context.Password == "password"))
            {
              var claims = new List<Claim>
              {
                new Claim(ClaimTypes.Name, 
                          context.UserName, 
                          context.Options.ClaimsIssuer)
              };
 
              var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(new ClaimsIdentity(
                  claims, 
                  BasicAuthenticationDefaults.AuthenticationScheme)),
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties(),
                BasicAuthenticationDefaults.AuthenticationScheme);
 
              return Task.FromResult(AuthenticateResult.Success(ticket));
            }
 
            return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
          }
        };
      });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<MLZ_Sefer_XheladiniContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MLZ_Sefer_XheladiniContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
  app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
