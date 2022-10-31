using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Monero.Client.Utilities;
using Xunit;

namespace MoneroClient.UnitTests.NetworkTests
{
    public class UriBuilderTests
    {
        [Theory]
        [InlineData("monero.com", 8080, "rpc", "http://monero.com:8080/rpc")]
        [InlineData("xmr.something.com", 1234, "rpc/json", "http://xmr.something.com:1234/rpc/json")]
        public void UriBuilder_ValidParams_ReturnsCorrectUrl(string host, uint port, string endpoint, string expectedUrl)
        {
            var url = new Monero.Client.Network.UriBuilder(host, port, endpoint).Build();
            Assert.Equal(expectedUrl, url.ToString());
        }
    }
}
