using System;

namespace Monero.Client.Network
{
    internal enum RequestEndpoint
    {
        // Generic endpoint
        JsonRpc,

        // Custom endpoints
        // https://www.getmonero.org/resources/developer-guides/daemon-rpc.html#get_transaction_pool
        TransactionPool,
        Transactions,
    }

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