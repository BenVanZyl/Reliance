using Shouldly;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Reliance.Db.Scripts.MsSql.Test
{
    public class ScriptExecutorTests
    {

        private string _pathToScripts = @"..\..\..\..\Reliance.Db.Scripts.MsSql\Scripts";

        /// <summary>
        /// Error in this test means there is a script that has not been marked as embedded resource.  Checck the error message and scripts.
        /// </summary>
        [Fact]
        public void VerifyAllScriptsEmbedded()
        {
            //var generalScriptsPath = Assembly.GetExecutingAssembly().RelativePath(SchemaScriptsPath);
            var scriptsOnDisk = Directory.GetFiles(_pathToScripts, "*.sql", SearchOption.AllDirectories).Select(Path.GetFileName);
            var scriptsEmbedded = Assembly.GetAssembly(typeof(Scripts.MsSql.ScriptExecutor)).GetManifestResourceNames();

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
    }
}
