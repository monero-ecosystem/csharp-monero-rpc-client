using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Enums
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
}