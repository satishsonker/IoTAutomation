﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace IoT.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
           
          return  WebHost.CreateDefaultBuilder(args)
    .ConfigureLogging(logBuilder =>
    {
        logBuilder.ClearProviders(); // removes all providers from LoggerFactory
        logBuilder.AddConsole();
        logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
    })
                .UseStartup<Startup>();
        }

    }
}
