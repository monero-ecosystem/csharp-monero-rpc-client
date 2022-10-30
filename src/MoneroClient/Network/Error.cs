using System;
using System.Text.Json.Serialization;

namespace Monero.Client.Network
{

    internal class Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonIgnore]
        public JsonRpcErrorCode JsonRpcErrorCode
        {
            get
            {
                if (this.Code <= -32000 && this.Code >= -32099)
                {
                    return JsonRpcErrorCode.ServerError;
                }
                else
                {
                    if (Enum.IsDefined(typeof(JsonRpcErrorCode), this.Code))
                    {
                        return (JsonRpcErrorCode)this.Code;
                    }
                    else
                    {
                        return JsonRpcErrorCode.UnknownError;
                    }
                }
            }
        }
    }
}