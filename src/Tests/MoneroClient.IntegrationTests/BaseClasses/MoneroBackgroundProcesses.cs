using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MoneroClient.IntegrationTests.Utilities;
using System;
using System.Collections.Generic;

namespace MoneroClient.IntegrationTests.BaseClasses
{
    public class MoneroBackgroundProcesses : IDisposable
    {
        private IConfiguration Configuration { get; set; }

        private void InitConfiguration()
        {
            this.Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
        }

        public MoneroBackgroundProcesses()
        {
            InitConfiguration();
            BackgroundProcessUtility.Start(Configuration);
        }

        public void Dispose()
        {
            BackgroundProcessUtility.Stop();
        }
    }
}