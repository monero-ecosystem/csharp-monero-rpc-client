using Microsoft.VisualStudio.TestPlatform.TestHost;
using MoneroClient.IntegrationTests.Utilities;
using System;

namespace MoneroClient.IntegrationTests.BaseClasses
{
    public class MoneroBackgroundProcesses : IDisposable
    {
        public MoneroBackgroundProcesses()
        {
            BackgroundProcessUtility.Start();
        }

        public void Dispose()
        {
            BackgroundProcessUtility.Stop();
        }
    }
}