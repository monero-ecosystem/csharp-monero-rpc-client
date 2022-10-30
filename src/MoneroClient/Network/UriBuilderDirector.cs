using System;

namespace Monero.Client.Network
{
    internal class UriBuilderDirector : IUriBuilder
    {
        private readonly IUriBuilder uriBuilder;

        public UriBuilderDirector(IUriBuilder uriBuilder)
        {
            this.uriBuilder = uriBuilder;
        }

        public Uri Build()
        {
            return this.uriBuilder.Build();
        }
    }
}