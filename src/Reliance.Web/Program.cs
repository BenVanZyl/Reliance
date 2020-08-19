using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Reliance.Web.ThisApp.Infrastructure;
using Serilog;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;

namespace Reliance.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configFileName = "appsettings.json";

#if DEBUG
            configFileName = "appsettings.Development.json";
#endif
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile(configFileName)
                .Build();

            var cnnString = PrivateSettings.ConnectionString;
#if DEBUG
            cnnString = config.GetSection("ConnectionStrings:DefaultConnection").Value;
#endif

            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo
                .MSSqlServer(
                    connectionString: cnnString,
                    sinkOptions: new SinkOptions { TableName = "LogEvents" })
                .CreateLogger();

            try
            {
                Log.Information("Application Starting.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
