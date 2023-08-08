using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace configuring_practice
{
    class Program
    {
        public static void Main(string[] args)
        {
            //ConfigurationBuilder -> using this class, you can store configuration values in a JSON file, and also retrieve them at run time.
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build()) // read the configuration of logger from appsettings.json file
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application is starting");
            //WebHostBuilder class is used to configure various aspects of the web host and the application. This includes setting up the application's services, middleware pipeline, logging, and more.
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //adding all the dependencies and services here
                    services.AddSingleton<IGreetingService, GreetingService>();

                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
            svc.RunMethod();
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            //adding key value pair stored in appsettings.json file
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true) //this adds appsettings.development.json file similar one also
                .AddEnvironmentVariables(); //if you have environment variables that will override the appsettings.json
        }
    }

}