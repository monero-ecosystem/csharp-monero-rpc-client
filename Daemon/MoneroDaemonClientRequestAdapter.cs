using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using MoneroClient.Daemon.POD.Responses;
using MoneroClient.Daemon.POD.Requests;
using MoneroClient.Network;

namespace MoneroClient.Daemon
{
    internal class MoneroDaemonClientRequestAdapter
    {
        private readonly Uri _uri;

        public MoneroDaemonClientRequestAdapter(Uri uri)
        {
            _uri = uri;
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroDaemonResponseSubType subType, DaemonRequestParameters requestParams, CancellationToken token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _uri);
            var request = GetRequest(subType, requestParams);
            return SerializeRequest(httpRequestMessage, request, token);
        }

        private static DaemonRequest GetRequest(MoneroDaemonResponseSubType subType, DaemonRequestParameters requestParams)
        {
            return subType switch
            {
                MoneroDaemonResponseSubType.BlockCount => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_count",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.BlockHeaderByHash => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_header_by_hash",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.BlockHeaderByHeight => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_header_by_height",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.BlockHeaderByRange => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_headers_range",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.BlockHeaderByRecency => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_last_block_header",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.AllConnections => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_connections",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.NodeInformation => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_info",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.HardforkInformation => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "hard_fork_info",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.BanInformation => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_bans",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.FlushTransactionPool => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "flush_txpool",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.OutputHistogram => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_output_histogram",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.CoinbaseTransactionSum => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_coinbase_tx_sum",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.FeeEstimate => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_fee_estimate",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.AlternateChain => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_alternate_chains",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.RelayTransaction => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "relay_tx",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.SyncInformation => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sync_info",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.Block => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block",
                    @params = requestParams,
                },
                MoneroDaemonResponseSubType.SetBans => new DaemonRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "set_bans",
                    @params = requestParams,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroDaemonResponseSubType ({subType})"),
            };
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, DaemonRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<DaemonRequest>(ms, request, new JsonSerializerOptions() { IgnoreNullValues = true, }).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }
    }
}