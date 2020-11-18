using System;

namespace Monero.Client.Network
{
    public enum RpcErrorCode
    {
        UnknownError = -1,
        WrongAddress = -2,
        DaemonIsBusy = -3,
        GenericTransferError = -4,
        WrongPaymentID = -5,
        TransferType = -6,
        Denied = -7,
        WrongTxid = -8,
        WrongSignature = -9,
        WrongKeyImage = -10,
        WrongUri = -11,
        WrongIndex = -12,
        NotOpen = -13,
    }

    public class RpcResponseException : Exception
    {
        public RpcErrorCode RpcErrorCode { get; private set; } = RpcErrorCode.UnknownError;

        public RpcResponseException() : base()
        {
        }

        public RpcResponseException(string message) : base(message)
        {
        }

        public RpcResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RpcResponseException(string message, RpcErrorCode rpcErrorCode) : base(message)
        {
            RpcErrorCode = rpcErrorCode;
        }
    }
}