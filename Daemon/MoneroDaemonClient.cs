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
        private readonly IMoneroDaemonDataRetriever _moneroRpcDaemonDataRetriever;

        public MoneroDaemonClient(Uri uri)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonDataRetriever(uri);
        }

        public MoneroDaemonClient(Uri uri, HttpMessageHandler httpMessageHandler)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonDataRetriever(uri, httpMessageHandler);
        }

        public MoneroDaemonClient(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonDataRetriever(uri, httpMessageHandler, disposeHandler);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>)
        /// </summary>
        public MoneroDaemonClient(MoneroNetwork networkType)
        {
            _moneroRpcDaemonDataRetriever = new MoneroDaemonDataRetriever(networkType);
        }

        public void Dispose()
        {
            _moneroRpcDaemonDataRetriever.Dispose();
        }

        public async Task<BlockCountResult> GetBlockCountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockCountAsync(token).ConfigureAwait(false);
            if (result == null || result.BlockCountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockCountResponse.result;
        }

        public async Task<BlockHeaderResult> GetLastBlockHeaderAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetLastBlockHeaderAsync(token).ConfigureAwait(false);
            if (result == null || result.BlockHeaderResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderResponse.result;
        }

        public async Task<BlockHeaderResult> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockHeaderByHashAsync(hash, token).ConfigureAwait(false);
            if (result == null || result.BlockHeaderResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderResponse.result;
        }

        public async Task<BlockHeaderResult> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockHeaderByHeightAsync(height, token).ConfigureAwait(false);
            if (result == null || result.BlockHeaderResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderResponse.result;
        }

        public async Task<BlockHeaderRangeResult> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockHeaderRangeAsync(startHeight, endHeight, token).ConfigureAwait(false);
            if (result == null || result.BlockHeaderRangeResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockHeaderRangeResponse.result;
        }

        public async Task<ConnectionResult> GetConnectionsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetConnectionsAsync(token).ConfigureAwait(false);
            if (result == null || result.ConnectionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ConnectionResponse.result;
        }

        public async Task<InformationResult> GetDaemonInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetDaemonInformationAsync(token).ConfigureAwait(false);
            if (result == null || result.DaemonInformationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.DaemonInformationResponse.result;
        }

        public async Task<HardforkInformationResult> GetHardforkInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetHardforkInformationAsync(token).ConfigureAwait(false);
            if (result == null || result.HardforkInformationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.HardforkInformationResponse.result;
        }

        public async Task<BanInformationResult> GetBanInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBansAsync(token).ConfigureAwait(false);
            if (result == null || result.GetBansResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetBansResponse.result;
        }

        public async Task<FlushTransactionPoolResult> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.FlushTransactionPoolAsync(txids, token).ConfigureAwait(false);
            if (result == null || result.FlushTransactionPoolResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FlushTransactionPoolResponse.result;
        }

        public async Task<OutputHistogramResult> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint minCount, uint maxCount, bool unlocked, uint recentCutoff, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetOutputHistogramAsync(amounts, minCount, maxCount, unlocked, recentCutoff, token).ConfigureAwait(false);
            if (result == null || result.OutputHistogramResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.OutputHistogramResponse.result;
        }

        public async Task<CoinbaseTransactionSumResult> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetCoinbaseTransactionSumAsync(height, count, token).ConfigureAwait(false);
            if (result == null || result.CoinbaseTransactionSumReponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CoinbaseTransactionSumReponse.result;
        }

        public async Task<VersionResult> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetVersionAsync(token).ConfigureAwait(false);
            if (result == null || result.VersionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.VersionResponse.result;
        }

        public async Task<FeeEstimateResult> GetFeeEstimateAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            if (result == null || result.FeeEstimateResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FeeEstimateResponse.result;
        }

        public async Task<AlternateChainResult> GetAlternateChainsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetAlternateChainsAsync(token).ConfigureAwait(false);
            if (result == null || result.AlternateChainResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AlternateChainResponse.result;
        }

        public async Task<RelayTransactionResult> RelayTransactionsAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.RelayTransactionsAsync(txids, token).ConfigureAwait(false);
            if (result == null || result.RelayTransactionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.RelayTransactionResponse.result;
        }

        public async Task<SyncronizeInformationResult> SyncInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.SyncInformationAsync(token).ConfigureAwait(false);
            if (result == null || result.SyncronizeInformationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SyncronizeInformationResponse.result;
        }

        public async Task<Block> GetBlockAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockAsync(height, token).ConfigureAwait(false);
            if (result == null || result.BlockResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockResponse.result;
        }

        public async Task<Block> GetBlockAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.GetBlockAsync(hash, token).ConfigureAwait(false);
            if (result == null || result.BlockResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockResponse.result;
        }

        public async Task<SetBansResult> SetBansAsync(IEnumerable<(string host, uint ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var result = await _moneroRpcDaemonDataRetriever.SetBansAsync(bans, token).ConfigureAwait(false);
            if (result == null || result.SetBansResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SetBansResponse.result;
        }
    }
}