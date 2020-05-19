using Reliance.Web.Tests.Infrastructure;
using Shouldly;
using SnowStorm.Infrastructure;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace Reliance.Web.Tests.DbMigrations
{
    public class DbMigrationTests
    {
        private const string SchemaScriptsPath = @"..\..\..\..\Reliance.DbMigrations\Scripts";

        [Fact]
        public void VerifyAllScriptsEmbedded()
        {
            //var generalScriptsPath = Assembly.GetExecutingAssembly().RelativePath(SchemaScriptsPath);
            var scriptsOnDisk = Directory.GetFiles(SchemaScriptsPath, "*.sql", SearchOption.AllDirectories).Select(Path.GetFileName);
            var scriptsEmbedded = Assembly.GetAssembly(typeof(Reliance.DbMigrations.DbMigration)).GetManifestResourceNames();

            foreach (var f in scriptsOnDisk)
            {
                scriptsEmbedded.ShouldContain(e => e.EndsWith(f), "Script file: " + f);
            }

        }
    }
}
