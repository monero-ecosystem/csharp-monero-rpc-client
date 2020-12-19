using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class RpcResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; set; }
        [JsonPropertyName("error")]
        public Error Error { get; set; }
        [JsonIgnore()]
        public bool ContainsError
        {
            get
            {
                return Error != null && Error.Code != default;
            }
        }
    }

    internal class Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonIgnore()]
        public JsonRpcErrorCode JsonRpcErrorCode
        {
            get
            {
                if (Code <= -32000 && Code >= -32099)
                    return JsonRpcErrorCode.ServerError;
                else if (Code == -32700)
                    return JsonRpcErrorCode.ParseError;
                else if (Code == -32600)
                    return JsonRpcErrorCode.InvalidRequest;
                else if (Code == -32601)
                    return JsonRpcErrorCode.MethodNotFound;
                else if (Code == -32602)
                    return JsonRpcErrorCode.InvalidParameters;
                else if (Code == -32603)
                    return JsonRpcErrorCode.InternalJsonError;
                else
                    return JsonRpcErrorCode.UnknownError;
            }
        }
    }

    public enum JsonRpcErrorCode
    {
        /// <summary>
        /// Catch-all.
        /// </summary>
        UnknownError = 0,
        /// <summary>
        /// Invalid JSON was received by the server.
        /// An error occurred on the server while parsing the JSON text.
        /// </summary>
        ParseError = -32700,
        /// <summary>
        /// The JSON sent is not a valid Request object.
        /// </summary>
        InvalidRequest = -32600,
        /// <summary>
        /// The method does not exist / is not available.
        /// </summary>
        MethodNotFound = -32601,
        /// <summary>
        /// Invalid method parameter(s).
        /// </summary>
        InvalidParameters = -32602,
        /// <summary>
        /// Internal JSON-RPC error.
        /// </summary>
        InternalJsonError = -32603,
        /// <summary>
        /// Reserved for implementation-defined server-errors.
        /// </summary>
        ServerError = -32000,
    }
}