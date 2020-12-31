using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Network;
using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
            _moneroRpcCommunicator = new RpcCommunicator(networkType, ConnectionType.Daemon);
        }

        public static Task<MoneroDaemonClient> CreateAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            var moneroDaemonClient = new MoneroDaemonClient(uri);
            return moneroDaemonClient.InitializeAsync(cancellationToken);
        }

        public static Task<MoneroDaemonClient> CreateAsync(MoneroNetwork moneroNetwork, CancellationToken cancellationToken = default)
        {
            var moneroDaemonClient = new MoneroDaemonClient(moneroNetwork);
            return moneroDaemonClient.InitializeAsync(cancellationToken);
        }

        private Task<MoneroDaemonClient> InitializeAsync(CancellationToken cancellationToken)
        {
            // Nothing to do yet.
            return Task.FromResult(this);
        }

        public void Dispose()
        {
            _moneroRpcCommunicator.Dispose();
        }

        public async Task<ulong> GetBlockCountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockCountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockCountResponse, nameof(GetBlockCountAsync));
            return result.BlockCountResponse.Result.Count;
        }

        public async Task<BlockHeader> GetLastBlockHeaderAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetLastBlockHeaderAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse, nameof(GetLastBlockHeaderAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<BlockHeader> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockHeaderByHashAsync(hash, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse, nameof(GetBlockHeaderByHashAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<BlockHeader> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockHeaderByHeightAsync(height, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse, nameof(GetBlockHeaderByHeightAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        public async Task<List<BlockHeader>> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockHeaderRangeAsync(startHeight, endHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderRangeResponse, nameof(GetBlockHeaderRangeAsync));
            return result.BlockHeaderRangeResponse.Result.Headers;
        }

        public async Task<List<Connection>> GetConnectionsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetConnectionsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ConnectionResponse, nameof(GetConnectionsAsync));
            return result.ConnectionResponse.Result.Connections;
        }

        public async Task<DaemonInformation> GetDaemonInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetDaemonInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DaemonInformationResponse, nameof(GetDaemonInformationAsync));
            return result.DaemonInformationResponse.Result;
        }

        public async Task<HardforkInformation> GetHardforkInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetHardforkInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.HardforkInformationResponse, nameof(GetHardforkInformationAsync));
            return result.HardforkInformationResponse.Result;
        }

        public async Task<List<Ban>> GetBanInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBansAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBansResponse, nameof(GetBanInformationAsync));
            return result.GetBansResponse.Result.Bans;
        }

        public async Task<string> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.FlushTransactionPoolAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FlushTransactionPoolResponse, nameof(FlushTransactionPoolAsync));
            return result.FlushTransactionPoolResponse.Result.Status;
        }

        public async Task<List<Distribution>> GetOutputHistogramAsync(IEnumerable<ulong> amounts, ulong fromHeight, ulong toHeight, bool cumulative = false, bool binary = true, bool compress = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetOutputHistogramAsync(amounts, fromHeight, toHeight, cumulative, binary, compress, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OutputHistogramResponse, nameof(GetOutputHistogramAsync));
            return result.OutputHistogramResponse.Result.Distributions;
        }

        public async Task<CoinbaseTransactionSum> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetCoinbaseTransactionSumAsync(height, count, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CoinbaseTransactionSumReponse, nameof(GetCoinbaseTransactionSumAsync));
            return result.CoinbaseTransactionSumReponse.Result;
        }

        public async Task<uint> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetDaemonVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.VersionResponse, nameof(GetVersionAsync));
            return result.VersionResponse.Result.Version;
        }

        public async Task<ulong> GetFeeEstimateAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FeeEstimateResponse, nameof(GetFeeEstimateAsync));
            return result.FeeEstimateResponse.Result.Fee;
        }

        public async Task<FeeEstimate> GetFeeEstimateParametersAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FeeEstimateResponse, nameof(GetFeeEstimateAsync));
            return result.FeeEstimateResponse.Result;
        }

        public async Task<List<Chain>> GetAlternateChainsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAlternateChainsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AlternateChainResponse, nameof(GetAlternateChainsAsync));
            return result.AlternateChainResponse.Result.Chains;
        }

        public async Task<string> RelayTransactionAsync(string hex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.RelayTransactionAsync(hex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RelayTransactionResponse, nameof(RelayTransactionAsync));
            return result.RelayTransactionResponse.Result.TxHash;
        }

        public async Task<SyncronizationInformation> SyncInformationAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SyncInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SyncronizeInformationResponse, nameof(SyncInformationAsync));
            return result.SyncronizeInformationResponse.Result;
        }

        public async Task<Block> GetBlockAsync(uint height, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockAsync(height, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockResponse, nameof(GetBlockAsync));
            return result.BlockResponse.Result;
        }

        public async Task<Block> GetBlockAsync(string hash, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBlockAsync(hash, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockResponse, nameof(GetBlockAsync));
            return result.BlockResponse.Result;
        }

        public async Task<string> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SetBansAsync(bans, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetBansResponse, nameof(SetBansAsync));
            return result.SetBansResponse.Result.Status;
        }

        public async Task<bool> SubmitBlocksAsync(IEnumerable<string> blockBlobs, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SubmitBlocksAsync(blockBlobs, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitBlockResponse, nameof(SubmitBlocksAsync));
            return result.SubmitBlockResponse.Result != null;
        }

        public async Task<BlockTemplate> GetBlockTemplateAsync(ulong reserveSize, string walletAddress, string prevBlock = null, string extraNonce = null, CancellationToken token = default)
        {
            if (reserveSize > 255ul)
                throw new InvalidOperationException($"Maximum {nameof(reserveSize)} cannot be greater than 255.");
            var result = await _moneroRpcCommunicator.GetBlockTemplateAsync(reserveSize, walletAddress, prevBlock, extraNonce, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBlockTemplateResponse, nameof(GetBlockTemplateAsync));
            return result.GetBlockTemplateResponse.Result;
        }

        public async Task<BanStatus> GetBanStatusAsync(string address, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBanStatusAsync(address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBanStatusResponse, nameof(GetBanStatusAsync));
            return result.GetBanStatusResponse.Result;
        }

        public async Task<TransactionPoolBacklog> GetTransactionPoolBacklogAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransactionPoolBacklogAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransactionPoolBacklogResponse, nameof(GetTransactionPoolBacklogAsync));
            return result.TransactionPoolBacklogResponse.Result;
        }

        //// Not supporting this currently, because it doesn't sound useful.
        //// Pruning a synced chain is a bad idea. Resyncing in pruned-mode would be faster.
        //public async Task<PruneBlockchain> PruneBlockchainAsync(bool check = false, CancellationToken token = default)
        //{
        //    var result = await _moneroRpcCommunicator.PruneBlockchainAsync(check, token).ConfigureAwait(false);
        //    ErrorGuard.ThrowIfResultIsNull(result?.PruneBlockchainResponse, nameof(PruneBlockchainAsync));
        //    return result.PruneBlockchainResponse.Result;
        //}
    }
}