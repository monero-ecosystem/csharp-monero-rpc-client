using System;
using Monero.Client.Constants;
using Monero.Client.Enums;

namespace Monero.Client.Network
{
    internal class RequestEndpointExtensionRetriever
    {
        public static string FetchEndpoint(Request request)
        {
            return FetchEndpoint(request.Endpoint);
        }

        public static string FetchEndpoint(RequestEndpoint endpoint)
        {
            return endpoint switch
            {
                RequestEndpoint.JsonRpc => "json_rpc",
                RequestEndpoint.TransactionPool => "get_transaction_pool",
                RequestEndpoint.Transactions => "get_transactions",
                _ => throw new InvalidOperationException($"Unknown Endpoint ({endpoint})"),
            };
        }
    }
}