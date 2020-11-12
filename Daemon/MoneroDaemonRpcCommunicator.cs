using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Daemon.POD.Requests;
using Monero.Client.Network;

namespace Monero.Client.Daemon
{
    internal class MoneroDaemonRpcCommunicator : IMoneroDaemonRpcCommunicator
    {
        private readonly HttpClient _httpClient;
        private readonly MoneroDaemonClientRequestAdapter _requestAdapter;

        private MoneroDaemonRpcCommunicator()
        {
            _httpClient = new HttpClient();
        }

        private MoneroDaemonRpcCommunicator(HttpMessageHandler httpMessageHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler);
        }

        private MoneroDaemonRpcCommunicator(HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler, disposeHandler);
        }

        public MoneroDaemonRpcCommunicator(Uri uri) : this()
        {
            _requestAdapter = new MoneroDaemonClientRequestAdapter(uri);
        }

        public MoneroDaemonRpcCommunicator(Uri uri, HttpMessageHandler httpMessageHandler) : this(httpMessageHandler)
        {
            _requestAdapter = new MoneroDaemonClientRequestAdapter(uri);
        }

        public MoneroDaemonRpcCommunicator(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler) : this(httpMessageHandler, disposeHandler)
        {
            _requestAdapter = new MoneroDaemonClientRequestAdapter(uri);
        }

        public MoneroDaemonRpcCommunicator(MoneroNetwork networkType) : this()
        {
            Uri uri;
            switch (networkType)
            {
                case MoneroNetwork.Mainnet:
                    uri = new Uri(MoneroNetworkDefaults.DaemonMainnetUri);
                    break;
                case MoneroNetwork.Stagenet:
                    uri = new Uri(MoneroNetworkDefaults.DaemonStagenetUri);
                    break;
                case MoneroNetwork.Testnet:
                    uri = new Uri(MoneroNetworkDefaults.DaemonTestnetUri);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown MoneroNetwork ({networkType})");
            }
            _requestAdapter = new MoneroDaemonClientRequestAdapter(uri);
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBlockCountAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.BlockCount, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockCountResponse responseObject = await JsonSerializer.DeserializeAsync<BlockCountResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.BlockCount,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.BlockCount,
                BlockCountResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBlockHeaderByHashAsync(string hash, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new InvalidOperationException("Hash cannot be null or whitespace");
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.BlockHeaderByHash, new DaemonRequestParameters() { hash = hash,}, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.BlockHeader,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.BlockHeaderByHash,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBlockHeaderByHeightAsync(uint height, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.BlockHeaderByHeight, new DaemonRequestParameters() { height = height, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.BlockHeader,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.BlockHeaderByHeight,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token)
        {
            if (endHeight < startHeight)
                throw new InvalidOperationException($"startHeight ({startHeight}) cannot be greater than endHeight ({endHeight})");
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.BlockHeaderByRange, new DaemonRequestParameters() { start_height = startHeight, end_height = endHeight, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockHeaderRangeResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderRangeResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.BlockHeader,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.BlockHeaderByRange,
                BlockHeaderRangeResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetConnectionsAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.AllConnections, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ConnectionResponse responseObject = await JsonSerializer.DeserializeAsync<ConnectionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Connection,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.AllConnections,
                ConnectionResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetDaemonInformationAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.NodeInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            DaemonInformationResponse responseObject = await JsonSerializer.DeserializeAsync<DaemonInformationResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.NodeInformation,
                DaemonInformationResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetHardforkInformationAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.HardforkInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            HardforkInformationResponse responseObject = await JsonSerializer.DeserializeAsync<HardforkInformationResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.NodeInformation,
                HardforkInformationResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBansAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.BanInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            GetBansResponse responseObject = await JsonSerializer.DeserializeAsync<GetBansResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.NodeInformation,
                GetBansResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetLastBlockHeaderAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.BlockHeaderByRecency, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.BlockHeader,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.BlockHeaderByRecency,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.FlushTransactionPool, new DaemonRequestParameters() { txids = txids,}, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            FlushTransactionPoolResponse responseObject = await JsonSerializer.DeserializeAsync<FlushTransactionPoolResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.TransactionPool,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.FlushTransactionPool,
                FlushTransactionPoolResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint min_count, uint max_count, bool unlocked, uint recent_cutoff, CancellationToken token)
        {
            if (min_count > max_count)
                throw new InvalidOperationException($"min_count ({min_count}) cannot be greater than max_count ({max_count})");
            var requestParameters = new DaemonRequestParameters() 
            { 
                amounts = amounts, 
                min_count = min_count, 
                max_count = max_count, 
                unlocked = unlocked, 
                recent_cutoff = recent_cutoff,
            };

            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.OutputHistogram, requestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            OutputHistogramResponse responseObject = await JsonSerializer.DeserializeAsync<OutputHistogramResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.OutputHistogram,
                OutputHistogramResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.CoinbaseTransactionSum, new DaemonRequestParameters() { height = height, count = count, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            CoinbaseTransactionSumResponse responseObject = await JsonSerializer.DeserializeAsync<CoinbaseTransactionSumResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Coinbase,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.CoinbaseTransactionSum,
                CoinbaseTransactionSumReponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetVersionAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.Version, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            VersionResponse responseObject = await JsonSerializer.DeserializeAsync<VersionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.Version,
                VersionResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.FeeEstimate, new DaemonRequestParameters() { grace_blocks = grace_blocks }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            FeeEstimateResponse responseObject = await JsonSerializer.DeserializeAsync<FeeEstimateResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.FeeEstimate,
                FeeEstimateResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetAlternateChainsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.AlternateChain, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AlternateChainResponse responseObject = await JsonSerializer.DeserializeAsync<AlternateChainResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Blockchain,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.AlternateChain,
                AlternateChainResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> RelayTransactionsAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.AlternateChain, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            RelayTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<RelayTransactionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Transaction,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.RelayTransaction,
                RelayTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> SyncInformationAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.SyncInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SyncronizeInformationResponse responseObject = await JsonSerializer.DeserializeAsync<SyncronizeInformationResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Information,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.SyncInformation,
                SyncronizeInformationResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBlockAsync(uint height, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.Block, new DaemonRequestParameters() { height = height }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockResponse responseObject = await JsonSerializer.DeserializeAsync<BlockResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Block,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.Block,
                BlockResponse = responseObject,
            };
        }

        public async Task<MoneroDaemonCommunicatorResponse> GetBlockAsync(string hash, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.Block, new DaemonRequestParameters() { hash = hash}, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockResponse responseObject = await JsonSerializer.DeserializeAsync<BlockResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Block,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.Block,
                BlockResponse = responseObject,
            };
        }

        private List<POD.Requests.NodeBan> BanInformationToBans(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans)
        {
            var requestBans = new List<POD.Requests.NodeBan>();
            foreach (var ban in bans)
            {
                requestBans.Add(new POD.Requests.NodeBan()
                {
                    Host = ban.host,
                    IP = ban.ip,
                    Ban = ban.ban,
                    Seconds = ban.seconds,
                });
            }
            return requestBans;
        }

        public async Task<MoneroDaemonCommunicatorResponse> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var daemonRequestParameters = new DaemonRequestParameters()
            {
                bans = BanInformationToBans(bans)
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroDaemonResponseSubType.SetBans, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SetBansResponse responseObject = await JsonSerializer.DeserializeAsync<SetBansResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroDaemonCommunicatorResponse()
            {
                MoneroNodeResponseType = MoneroDaemonResponseType.Connection,
                MoneroNodeResponseSubType = MoneroDaemonResponseSubType.SetBans,
                SetBansResponse = responseObject,
            };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
