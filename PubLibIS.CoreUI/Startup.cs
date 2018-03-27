using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PubLibIS.CoreUI.ServiceExtensions;

namespace PubLibIS.CoreUI
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(jwtBearerOptions =>
       {
         jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
         {
           ValidateActor = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = Configuration["TokenAuthentication:Issuer"],
           ValidAudience = Configuration["TokenAuthentication:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenAuthentication:SecretKey"]))
         };
       });

      string connectionString = Configuration.GetConnectionString("LibConnection");
      services.AddMvc().AddJsonOptions(opt =>
      {
        opt.SerializerSettings.DateFormatString = ViewModels.Util.CultureFormatsModule.GetCustomDateFormat();
        opt.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTime;
      });
      services.AddDALDI(connectionString);
      services.AddBLLDI();


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseAuthentication();
      app.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 404 &&
           !Path.HasExtension(context.Request.Path.Value) &&
           !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      app.UseMvcWithDefaultRoute();
      app.UseDefaultFiles();
      app.UseStaticFiles();
    }
  }
}
