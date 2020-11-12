using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Network;

namespace Monero.Client.Daemon
{
    public class MoneroDaemonClient : IMoneroDaemonClient
    {
        private readonly IMoneroDaemonRpcCommunicator _moneroRpcDaemonDataRetriever;

        public MoneroDaemonClient(Uri uri)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonRpcCommunicator(uri);
        }

        public MoneroDaemonClient(Uri uri, HttpMessageHandler httpMessageHandler)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonRpcCommunicator(uri, httpMessageHandler);
        }

        public MoneroDaemonClient(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonRpcCommunicator(uri, httpMessageHandler, disposeHandler);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>)
        /// </summary>
        public MoneroDaemonClient(MoneroNetwork networkType)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonRpcCommunicator(networkType);
        }

        public void Dispose()
        {
            _moneroRpcDaemonDataRetriever.Dispose();
        }

        public async Task<ulong> GetBlockCountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockCountAsync(token).ConfigureAwait(false);
            if ( result?.BlockCountResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockCountResponse.Result.Count;
        }

        public async Task<BlockHeader> GetLastBlockHeaderAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetLastBlockHeaderAsync(token).ConfigureAwait(false);
            if (result?.BlockHeaderResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<BlockHeader> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockHeaderByHashAsync(hash, token).ConfigureAwait(false);
            if (result == null || result.BlockHeaderResponse == null || result.BlockHeaderResponse.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<BlockHeader> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockHeaderByHeightAsync(height, token).ConfigureAwait(false);
            if (result?.BlockHeaderResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<List<BlockHeader>> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockHeaderRangeAsync(startHeight, endHeight, token).ConfigureAwait(false);
            if (result?.BlockHeaderRangeResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderRangeResponse.Result.Headers;
        }

        public async Task<List<Connection>> GetConnectionsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetConnectionsAsync(token).ConfigureAwait(false);
            if (result?.ConnectionResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ConnectionResponse.Result.Connections;
        }

        public async Task<DaemonInformation> GetDaemonInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetDaemonInformationAsync(token).ConfigureAwait(false);
            if (result?.DaemonInformationResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.DaemonInformationResponse.Result;
        }

        public async Task<HardforkInformation> GetHardforkInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetHardforkInformationAsync(token).ConfigureAwait(false);
            if (result?.HardforkInformationResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.HardforkInformationResponse.Result;
        }

        public async Task<List<Ban>> GetBanInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBansAsync(token).ConfigureAwait(false);
            if (result?.GetBansResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetBansResponse.Result.Bans;
        }

        public async Task<string> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.FlushTransactionPoolAsync(txids, token).ConfigureAwait(false);
            if (result?.FlushTransactionPoolResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FlushTransactionPoolResponse.Result.Status;
        }

        public async Task<List<Histogram>> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint minCount, uint maxCount, bool unlocked, uint recentCutoff, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetOutputHistogramAsync(amounts, minCount, maxCount, unlocked, recentCutoff, token).ConfigureAwait(false);
            if (result?.OutputHistogramResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.OutputHistogramResponse.Result.Histograms;
        }

        public async Task<CoinbaseTransactionSumResult> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetCoinbaseTransactionSumAsync(height, count, token).ConfigureAwait(false);
            if (result?.CoinbaseTransactionSumReponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CoinbaseTransactionSumReponse.Result;
        }

        public async Task<uint> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetVersionAsync(token).ConfigureAwait(false);
            if (result?.VersionResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.VersionResponse.Result.Version;
        }

        public async Task<ulong> GetFeeEstimateAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            if (result?.FeeEstimateResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FeeEstimateResponse.Result.Fee;
        }

        public async Task<List<Chain>> GetAlternateChainsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetAlternateChainsAsync(token).ConfigureAwait(false);
            if (result?.AlternateChainResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AlternateChainResponse.Result.Chains;
        }

        public async Task<string> RelayTransactionsAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.RelayTransactionsAsync(txids, token).ConfigureAwait(false);
            if (result?.RelayTransactionResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.RelayTransactionResponse.Result.Status;
        }

        public async Task<SyncronizationInformation> SyncInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.SyncInformationAsync(token).ConfigureAwait(false);
            if (result?.SyncronizeInformationResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SyncronizeInformationResponse.Result;
        }

        public async Task<Block> GetBlockAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockAsync(height, token).ConfigureAwait(false);
            if (result.BlockResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockResponse.Result;
        }

        public async Task<Block> GetBlockAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockAsync(hash, token).ConfigureAwait(false);
            if (result?.BlockResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockResponse.Result;
        }

        public async Task<string> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.SetBansAsync(bans, token).ConfigureAwait(false);
            if (result?.SetBansResponse?.Result == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SetBansResponse.Result.Status;
        }
    }
}