using Reliance.Db.Scripts.MsSql;
using Shouldly;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Reliance.Web.Test.DbScripts
{
    public class ScriptExecutorTests
    {
        /// <summary>
        /// Error in this test means there is a script that has not been marked as embedded resource.  Checck the error message and scripts.
        /// </summary>
        [Fact]
        public void VerifyAllScriptsEmbedded()
        {
            var scriptsOnDisk = Directory.GetFiles(PathToScripts(), "*.sql", SearchOption.AllDirectories).Select(Path.GetFileName);
            var scriptsEmbedded = Assembly.GetAssembly(typeof(ScriptExecutor)).GetManifestResourceNames();

            foreach (var f in scriptsOnDisk)
            {
                scriptsEmbedded.ShouldContain(e => e.EndsWith(f), "Script file: " + f);
            }
        }


        [Fact]
        public void VerifyIsRunningAsTest()
        {
            // code is running in unit test mode
            RunningAs.UnitTest.ShouldBeTrue();
        }

        private string PathToScripts()
        {
            var scriptPath = @"SnowBird.DbMigrations\Scripts";

            Console.WriteLine(" ## Getting assemblyLocation ...");
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            Console.WriteLine($" ##   assemblyLocation = '{assemblyLocation}' ");

            Console.WriteLine(" ## Getting srcPosition ...");
            var srcPosition = assemblyLocation.IndexOf(@"src") + 4; // location = start position of search string + 4 position onward to include slash sign
            Console.WriteLine($" ##   srcPosition = '{srcPosition}' ");

            Console.WriteLine(" ## Getting rootPath ...");
            var rootPath = assemblyLocation.Remove(srcPosition, assemblyLocation.Count() - srcPosition);
            Console.WriteLine($" ##   rootPath = '{rootPath}' ");
            Directory.Exists(rootPath).ShouldBeTrue();

            Console.WriteLine(" ## Compiling Full Path ...");
            var path = $"{rootPath}{scriptPath}";
            Console.WriteLine($" ##   Full Path = '{path}' ");
            if (!Directory.Exists(path))
            {
                Console.WriteLine($" ##   ** Full Path NOT FOUND! Switching context ... ");
                scriptPath = scriptPath.Replace(@"\", @"/");
                path = $"{rootPath}{scriptPath}";
                Console.WriteLine($" ##   ** Full Path = '{path}' ");
            }

            //final confirmation
            Directory.Exists(path).ShouldBeTrue();

            return path;
        }
    }
}
