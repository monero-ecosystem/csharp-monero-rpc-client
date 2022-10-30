using System;

namespace Monero.Client.Network
{
    public class JsonRpcException : Exception
    {
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
            this.JsonRpcErrorCode = jsonRpcErrorCode;
        }

        public JsonRpcErrorCode JsonRpcErrorCode { get; private set; } = JsonRpcErrorCode.UnknownError;

        public override string ToString()
        {
            return $"{nameof(JsonRpcException)} - {this.JsonRpcErrorCode}";
        }
    }
}