﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Reliance.DbScripts.MsSql;
using System;
using System.Net;
using System.Net.Http;

namespace Reliance.Web.Test.Infrastructure
{
    public class TestServerFixture : IDisposable
    {
        private static readonly object _lock = new object();

        public TestServer TestServer { get; }
        public HttpClient Client { get; }
        public bool UpgradePerformed { get; set; } = false;
        public string ConnectionString { get; set; }

        public TestServerFixture()
        {
            CreateTestDbWithDbUp();

            var webBuilder = new WebHostBuilder()
                //.UseStartup<Startup>()  //startup in test project.
                .UseStartup<Reliance.Web.Startup>()
                .UseSetting("ConnectionStrings:AuthConnection", ConnectionString)
                .UseSetting("ConnectionStrings:DataConnection", ConnectionString);

            TestServer = new TestServer(webBuilder);

            Client = TestServer.CreateClient();
            Client.BaseAddress = new Uri(Client.BaseAddress.ToString().Replace("http:", "https:"));

            ////Client.cre
            //var hndl = TestServer.CreateHandler()
            //    {

            //    };

            //var uri = new Uri(Client.BaseAddress.ToString());
            //var credentialsCache = new CredentialCache { { uri, "NTLM", CredentialCache.DefaultNetworkCredentials } };
            //var handler = new HttpClientHandler { Credentials = credentialsCache,  };
            //var _apiClient = new HttpClient(handler);


        }


        private void CreateTestDbWithDbUp()
        {
            if (UpgradePerformed)
                return;

            lock (_lock)
            {
                if (!string.IsNullOrEmpty(ConnectionString))
                    return; //has a connection string

                //create cnn string
                var cnnstring = $"Server=(localdb)\\mssqllocaldb;Database={DbName};Trusted_Connection=True;MultipleActiveResultSets=true";
                ConnectionString = cnnstring;

                try
                {
                    ScriptExecutor.PerformUpgrade(ScriptTokens.DeploymentEnvironments.Testing, cnnstring, true);

                    UpgradePerformed = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private string DbName
        {
            get
            {
                if (string.IsNullOrEmpty(_dbName))
                {
                    _dbName = $"TstReliance-{DateTime.Today:yyyyMMdd}-{Guid.NewGuid()}";
                }

                return _dbName;
            }
        }
        private string _dbName = "";

        public void Dispose()
        {
            //throw new NotImplementedException();
            //TODO: Cleanup of test databases.
        }

    }
}
