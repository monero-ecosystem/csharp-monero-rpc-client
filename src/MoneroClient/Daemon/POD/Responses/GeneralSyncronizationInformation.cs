using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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