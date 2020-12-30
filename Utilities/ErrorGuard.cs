using Monero.Client.Network;
using System;
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
            string errorMessage = $"Error experienced when making RPC call in {functionName}.";
            if (resultIsNull)
                throw new JsonRpcException(errorMessage);
        }

        public static void ThrowIfResultIsNull(RpcResponse rpcResponse, string functionName)
        {
            StringBuilder errorMessageBuilder = new StringBuilder($"Error experienced when making RPC call in {functionName}.");
            if (rpcResponse == null)
                throw new JsonRpcException(errorMessageBuilder.ToString());
            else if (rpcResponse.ContainsError)
            {
                errorMessageBuilder.Append($" JsonRpcError: {rpcResponse.Error.Message}");
                throw new JsonRpcException(errorMessageBuilder.ToString(), rpcResponse.Error.JsonRpcErrorCode);
            }
        }

        public static void ThrowIfNullOrWhiteSpace(string objectCheked, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(objectCheked))
                throw new InvalidOperationException($"Cannot be null or whitespace (Parameter: {parameterName})");
            return;
        }
    }
}