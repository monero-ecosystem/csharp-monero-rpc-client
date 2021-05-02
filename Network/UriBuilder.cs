using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    internal interface IUriBuilder
    {
        Uri Build();
    }

    internal class UriBuilder : IUriBuilder
    {
        private readonly string _url;
        private readonly string _endpoint;
        private readonly uint _port;
        
        public UriBuilder(string url, uint port, string endpoint)
        {
            _url = url;
            _port = port;
            _endpoint = endpoint;
        }

        public Uri Build()
        {
            return new Uri($"http://{_url}:{_port}/{_endpoint}");
        }
    }

    internal class UriBuilderDirector : IUriBuilder
    {
        private readonly IUriBuilder _uriBuilder;

        public UriBuilderDirector(IUriBuilder uriBuilder)
        {
            _uriBuilder = uriBuilder;
        }

        public Uri Build()
        {
            return _uriBuilder.Build();
        }
    }
}