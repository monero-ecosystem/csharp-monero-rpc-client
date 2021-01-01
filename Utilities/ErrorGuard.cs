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

        public static void ThrowIfUnlockTimeIsToolarge(ulong unlockTime, string parameterName)
        {
            if (unlockTime > ErrorThresholds.MaximumUnlockTime)
                throw new InvalidOperationException($"You are attempting to perform an action with a massive unlock time. " +
                    $"{parameterName} is too large. {parameterName} is limited to {ErrorThresholds.MaximumUnlockTime} for your safety.");
        }
    }

    internal class ErrorThresholds
    {
        /// <summary>
        /// There are 365 days in a year. 
        /// I will not let someone lock their funds for longer than a year.
        /// I do not want people making mistakes, and blaming me. 
        /// For example, 0ul - 1 is ulong.MaxValue, which is eternity to have ones' funds locked for.
        /// </summary>
        public static readonly ulong MaximumUnlockTime = (ulong)(TimeSpan.FromDays(365) / BlockchainNetworkDefaults.AverageBlockTime);
    }
}