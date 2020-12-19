using System;

namespace Monero.Client.Network
{
    public enum MoneroErrorCode
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
        InsufficientUnlockedOutputs = -14,
    }

    public class MoneroRpcException : Exception
    {
        public MoneroErrorCode MoneroErrorCode { get; private set; } = MoneroErrorCode.UnknownError;

        public MoneroRpcException() : base()
        {
        }

        public MoneroRpcException(string message) : base(message)
        {
        }

        public MoneroRpcException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MoneroRpcException(string message, MoneroErrorCode moneroErrorCode) : base(message)
        {
            MoneroErrorCode = moneroErrorCode;
        }

        public override string ToString()
        {
            return $"{nameof(MoneroRpcException)} - {MoneroErrorCode}";
        }
    }

    public class JsonRpcException : Exception
    {
        public JsonRpcErrorCode JsonRpcErrorCode { get; private set; } = JsonRpcErrorCode.UnknownError;

        public JsonRpcException() : base()
        {
        }

        public JsonRpcException(string message) : base(message)
        {
        }

        public JsonRpcException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public JsonRpcException(string message, JsonRpcErrorCode jsonRpcErrorCode) : base(message)
        {
            JsonRpcErrorCode = jsonRpcErrorCode;
        }

        public override string ToString()
        {
            return $"{nameof(JsonRpcException)} - {JsonRpcErrorCode}";
        }
    }
}