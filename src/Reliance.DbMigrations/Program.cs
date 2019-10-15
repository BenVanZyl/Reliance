using System;
using System.IO;
using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace Reliance.DbMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Implement Configuration.GetConnectionString("DefaultConnection")
            // var connectionString = "Server=(localdb)\\mssqllocaldb;Database=Reliance;Trusted_Connection=True;MultipleActiveResultSets=true"; // "Server=(local)\\SqlExpress; Database=MyApp; Trusted_connection=true";
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                //return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
           // return 0;
        }

        private static IConfigurationRoot _configuration = null;
        private static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    _configuration = builder.Build();
                }
                return _configuration;
            }
            set { _configuration = value; }
        }
    }
}
