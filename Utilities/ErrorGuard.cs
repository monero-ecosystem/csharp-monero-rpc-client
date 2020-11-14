using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Utilities
{
    internal class ErrorGuard
    {
        public static void ThrowIfWalletNotOpen(bool isWalletOpen, string functionName)
        {
            if (!isWalletOpen)
                throw new InvalidOperationException($"Wallet is not open. Cannot call {functionName}");
            return;
        }

        public static void ThrowIfWalletOpen(bool isWalletOpen, string functionName)
        {
            if (isWalletOpen)
                throw new InvalidOperationException($"Wallet is open. Cannot call {functionName}");
            return;
        }

        public static void ThrowIfResultIsNull(bool resultIsNull, string functionName)
        {
            if (resultIsNull)
                throw new RpcResponseException($"Error experienced when making RPC call in {functionName}");
            return;
        }

        public static void ThrowIfResultIsNull(RpcResponse rpcResponse, string functionName)
        {
            if (rpcResponse == null)
                throw new RpcResponseException($"Error experienced when making RPC call in {functionName}");
            return;
        }
    }
}