using System;

namespace Monero.Client.Network
{
    internal interface IUriBuilder
    {
        Uri Build();
    }

    internal class UriBuilder : IUriBuilder
    {
        private readonly string host;
        private readonly string endpoint;
        private readonly uint port;

        public UriBuilder(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public UriBuilder(string host, uint port, string endpoint)
        {
            this.host = host;
            this.port = port;
            this.endpoint = endpoint;
        }

        public Uri Build()
        {
            return new Uri($"http://{this.host}:{this.port}/{this.endpoint}");
        }
    }
}