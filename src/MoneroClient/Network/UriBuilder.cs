using System;

namespace Monero.Client.Network
{
    internal interface IUriBuilder
    {
        Uri Build();
    }

    internal class UriBuilder : IUriBuilder
    {
        private readonly string url;
        private readonly string endpoint;
        private readonly uint port;

        public UriBuilder(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public UriBuilder(string url, uint port, string endpoint)
        {
            this.url = url;
            this.port = port;
            this.endpoint = endpoint;
        }

        public Uri Build()
        {
            return new Uri($"http://{this.url}:{this.port}/{this.endpoint}");
        }
    }
}