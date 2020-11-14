using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Utilities;
using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Network;

namespace Monero.Client.Daemon
{
    public class MoneroDaemonClient : IMoneroDaemonClient
    {
        private readonly RpcCommunicator _moneroRpcCommunicator;

        public MoneroDaemonClient(Uri uri)
        {
            _moneroRpcCommunicator = new RpcCommunicator(uri);
        }

        public MoneroDaemonClient(Uri uri, HttpMessageHandler httpMessageHandler)
        {
            _moneroRpcCommunicator = new RpcCommunicator(uri, httpMessageHandler);
        }

        public MoneroDaemonClient(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _moneroRpcCommunicator = new RpcCommunicator(uri, httpMessageHandler, disposeHandler);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>)
        /// </summary>
        public MoneroDaemonClient(MoneroNetwork networkType)
        {
            _moneroRpcCommunicator = new RpcCommunicator(networkType);
        }

        public void Dispose()
        {
            _moneroRpcCommunicator.Dispose();
        }

        public async Task<ulong> GetBlockCountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockCountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockCountResponse?.Result == null, nameof(GetBlockCountAsync));
            return result.BlockCountResponse.Result.Count;
        }

        public async Task<BlockHeader> GetLastBlockHeaderAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetLastBlockHeaderAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse?.Result == null, nameof(GetLastBlockHeaderAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<BlockHeader> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockHeaderByHashAsync(hash, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse?.Result == null, nameof(GetBlockHeaderByHashAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<BlockHeader> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockHeaderByHeightAsync(height, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse?.Result == null, nameof(GetBlockHeaderByHeightAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<List<BlockHeader>> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockHeaderRangeAsync(startHeight, endHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderRangeResponse?.Result == null, nameof(GetBlockHeaderRangeAsync));
            return result.BlockHeaderRangeResponse.Result.Headers;
        }

        public async Task<List<Connection>> GetConnectionsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetConnectionsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ConnectionResponse?.Result == null, nameof(GetConnectionsAsync));
            return result.ConnectionResponse.Result.Connections;
        }

        public async Task<DaemonInformation> GetDaemonInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetDaemonInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DaemonInformationResponse?.Result == null, nameof(GetDaemonInformationAsync));
            return result.DaemonInformationResponse.Result;
        }

        public async Task<HardforkInformation> GetHardforkInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetHardforkInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.HardforkInformationResponse?.Result == null, nameof(GetHardforkInformationAsync));
            return result.HardforkInformationResponse.Result;
        }

        public async Task<List<Ban>> GetBanInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBansAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBansResponse?.Result == null, nameof(GetBanInformationAsync));
            return result.GetBansResponse.Result.Bans;
        }

        public async Task<string> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.FlushTransactionPoolAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FlushTransactionPoolResponse?.Result == null, nameof(FlushTransactionPoolAsync));
            return result.FlushTransactionPoolResponse.Result.Status;
        }

        public async Task<List<Histogram>> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint minCount, uint maxCount, bool unlocked, uint recentCutoff, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetOutputHistogramAsync(amounts, minCount, maxCount, unlocked, recentCutoff, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OutputHistogramResponse?.Result == null, nameof(GetOutputHistogramAsync));
            return result.OutputHistogramResponse.Result.Histograms;
        }

        public async Task<CoinbaseTransactionSumResult> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetCoinbaseTransactionSumAsync(height, count, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CoinbaseTransactionSumReponse?.Result == null, nameof(GetCoinbaseTransactionSumAsync));
            return result.CoinbaseTransactionSumReponse.Result;
        }

        public async Task<uint> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetDaemonVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.VersionResponse?.Result == null, nameof(GetVersionAsync));
            return result.VersionResponse.Result.Version;
        }

        public async Task<ulong> GetFeeEstimateAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FeeEstimateResponse?.Result == null, nameof(GetFeeEstimateAsync));
            return result.FeeEstimateResponse.Result.Fee;
        }

        public async Task<List<Chain>> GetAlternateChainsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAlternateChainsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AlternateChainResponse?.Result == null, nameof(GetAlternateChainsAsync));
            return result.AlternateChainResponse.Result.Chains;
        }

        public async Task<string> RelayTransactionsAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.RelayTransactionsAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RelayTransactionResponse?.Result == null, nameof(RelayTransactionsAsync));
            return result.RelayTransactionResponse.Result.Status;
        }

        public async Task<SyncronizationInformation> SyncInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SyncInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SyncronizeInformationResponse?.Result == null, nameof(SyncInformationAsync));
            return result.SyncronizeInformationResponse.Result;
        }

        public async Task<Block> GetBlockAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockAsync(height, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockResponse?.Result == null, nameof(GetBlockAsync));
            return result.BlockResponse.Result;
        }

        public async Task<Block> GetBlockAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockAsync(hash, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockResponse?.Result == null, nameof(GetBlockAsync));
            return result.BlockResponse.Result;
        }

        public async Task<string> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SetBansAsync(bans, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetBansResponse?.Result == null, nameof(SetBansAsync));
            return result.SetBansResponse.Result.Status;
        }
    }
}