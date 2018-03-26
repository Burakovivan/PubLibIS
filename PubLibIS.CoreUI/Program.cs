using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PubLibIS.CoreUI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
              var env = hostingContext.HostingEnvironment;

              config.AddJsonFile("appsettings.json", optional: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

              config.AddEnvironmentVariables();
            })
            .UseStartup<Startup>()
            .Build();
  }
}
