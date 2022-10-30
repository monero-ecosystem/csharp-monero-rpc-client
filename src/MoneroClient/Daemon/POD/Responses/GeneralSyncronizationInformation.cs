using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class GeneralSyncronizationInformation
    {
        [JsonPropertyName("info")]
        public Connection Connection { get; set; }
        public override string ToString()
        {
            return $"{this.Connection}";
        }
    }
}