using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Enums;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon
{
    public class MoneroDaemonClient : IMoneroDaemonClient
    {
        private readonly RpcCommunicator moneroRpcCommunicator;
        private readonly object disposalLock = new object();
        private bool disposed = false;

        private MoneroDaemonClient(string host, uint port)
        {
            this.moneroRpcCommunicator = new RpcCommunicator(host, port);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoneroDaemonClient"/> class
        /// using default network settings (<localhost>:<defaultport>)
        /// </summary>
        private MoneroDaemonClient(MoneroNetwork networkType)
        {
            this.moneroRpcCommunicator = new RpcCommunicator(networkType, ConnectionType.Daemon);
        }

        public static Task<MoneroDaemonClient> CreateAsync(string host, uint port, CancellationToken cancellationToken = default)
        {
            var moneroDaemonClient = new MoneroDaemonClient(host, port);
            return moneroDaemonClient.InitializeAsync(cancellationToken);
        }

        public static Task<MoneroDaemonClient> CreateAsync(MoneroNetwork moneroNetwork, CancellationToken cancellationToken = default)
        {
            var moneroDaemonClient = new MoneroDaemonClient(moneroNetwork);
            return moneroDaemonClient.InitializeAsync(cancellationToken);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Look up how many blocks are in the longest chain known to the node.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ulong> GetBlockCountAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBlockCountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockCountResponse, nameof(this.GetBlockCountAsync));
            return result.BlockCountResponse.Result.Count;
        }

        /// <summary>
        /// Block header information for the most recent block is easily retrieved with this method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<BlockHeader> GetLastBlockHeaderAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetLastBlockHeaderAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse, nameof(this.GetLastBlockHeaderAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        /// <summary>
        ///  Retrieve basic information about the block by hash.
        /// </summary>
        public async Task<BlockHeader> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBlockHeaderByHashAsync(hash, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse, nameof(this.GetBlockHeaderByHashAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        /// <summary>
        ///  Retrieve basic information about the block by height.
        /// </summary>
        public async Task<BlockHeader> GetBlockHeaderByHeightAsync(ulong height, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBlockHeaderByHeightAsync(height, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderResponse, nameof(this.GetBlockHeaderByHeightAsync));
            return result.BlockHeaderResponse.Result.BlockHeader;
        }

        /// <summary>
        /// Similar to get_block_header_by_height above, but for a range of blocks.
        /// This method includes a starting block height and an ending block height as parameters to retrieve basic information about the range of blocks.
        /// </summary>
        /// <param name="startHeight">The starting block's height.</param>
        /// <param name="endHeight">The ending block's height.</param>
        public async Task<List<BlockHeader>> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBlockHeaderRangeAsync(startHeight, endHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockHeaderRangeResponse, nameof(this.GetBlockHeaderRangeAsync));
            return result.BlockHeaderRangeResponse.Result.Headers;
        }

        /// <summary>
        /// Retrieve information about incoming and outgoing connections to your node.
        /// </summary>
        public async Task<List<Connection>> GetConnectionsAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetConnectionsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ConnectionResponse, nameof(this.GetConnectionsAsync));
            return result.ConnectionResponse.Result.Connections;
        }

        /// <summary>
        /// Retrieve general information about the state of your node and the network.
        /// </summary>
        public async Task<DaemonInformation> GetDaemonInformationAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetDaemonInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DaemonInformationResponse, nameof(this.GetDaemonInformationAsync));
            return result.DaemonInformationResponse.Result;
        }

        /// <summary>
        /// Look up information regarding hard fork voting and readiness.
        /// </summary>
        public async Task<HardforkInformation> GetHardforkInformationAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetHardforkInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.HardforkInformationResponse, nameof(this.GetHardforkInformationAsync));
            return result.HardforkInformationResponse.Result;
        }

        /// <summary>
        /// Get list of banned IPs.
        /// </summary>
        public async Task<List<Ban>> GetBanInformationAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBansAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBansResponse, nameof(this.GetBanInformationAsync));
            return result.GetBansResponse.Result.Bans;
        }

        /// <summary>
        /// Flush tx ids from transaction pool
        /// </summary>
        /// <param name="txids">List of transactions IDs to flush from pool (all tx ids flushed if empty).</param>
        /// <returns>A string representing transaction pool status.</returns>
        public async Task<string> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.FlushTransactionPoolAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FlushTransactionPoolResponse, nameof(this.FlushTransactionPoolAsync));
            return result.FlushTransactionPoolResponse.Result.Status;
        }

        /// <summary>
        /// Get a histogram of output amounts. For all amounts (possibly filtered by parameters), gives the number of outputs on the chain for that amount. RingCT outputs counts as 0 amount.
        /// </summary>
        public async Task<List<Distribution>> GetOutputHistogramAsync(IEnumerable<ulong> amounts, ulong fromHeight, ulong toHeight, bool cumulative = false, bool binary = true, bool compress = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetOutputHistogramAsync(amounts, fromHeight, toHeight, cumulative, binary, compress, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OutputHistogramResponse, nameof(this.GetOutputHistogramAsync));
            return result.OutputHistogramResponse.Result.Distributions;
        }

        /// <summary>
        /// Get the coinbase amount and the fees amount for n last blocks starting at particular height.
        /// </summary>
        public async Task<CoinbaseTransactionSum> GetCoinbaseTransactionSumAsync(ulong height, uint count, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetCoinbaseTransactionSumAsync(height, count, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CoinbaseTransactionSumReponse, nameof(this.GetCoinbaseTransactionSumAsync));
            return result.CoinbaseTransactionSumReponse.Result;
        }

        /// <summary>
        /// Give the node current version.
        /// </summary>
        public async Task<uint> GetVersionAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetDaemonVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.VersionResponse, nameof(this.GetVersionAsync));
            return result.VersionResponse.Result.Version;
        }

        /// <summary>
        /// Gives an estimation on fees (piconero) per byte.
        /// </summary>
        public async Task<ulong> GetFeeEstimateAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FeeEstimateResponse, nameof(this.GetFeeEstimateAsync));
            return result.FeeEstimateResponse.Result.Fee;
        }

        /// <summary>
        /// Gets all fee estimate parameters, including fees (piconero) per byte.
        /// </summary>
        public async Task<FeeEstimate> GetFeeEstimateParametersAsync(uint graceBlocks, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetFeeEstimateAsync(graceBlocks, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FeeEstimateResponse, nameof(this.GetFeeEstimateAsync));
            return result.FeeEstimateResponse.Result;
        }

        /// <summary>
        /// Display alternative chains seen by the node.
        /// </summary>
        public async Task<List<Chain>> GetAlternateChainsAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAlternateChainsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AlternateChainResponse, nameof(this.GetAlternateChainsAsync));
            return result.AlternateChainResponse.Result.Chains;
        }

        /// <summary>
        /// Relay a transaction.
        /// </summary>
        /// <returns>The transaction hash of the relayed transaction.</returns>
        public async Task<string> RelayTransactionAsync(string hex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.RelayTransactionAsync(hex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RelayTransactionResponse, nameof(this.RelayTransactionAsync));
            return result.RelayTransactionResponse.Result.TxHash;
        }

        /// <summary>
        /// Get synchronisation informations.
        /// </summary>
        public async Task<SyncronizationInformation> SyncInformationAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SyncInformationAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SyncronizeInformationResponse, nameof(this.SyncInformationAsync));
            return result.SyncronizeInformationResponse.Result;
        }

        /// <summary>
        /// Block header information can be retrieved using either a block's hash or height.
        /// This method includes a block's height as an input parameter to retrieve basic information about the block.
        /// </summary>
        public async Task<Block> GetBlockAsync(uint height, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBlockAsync(height, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockResponse, nameof(this.GetBlockAsync));
            return result.BlockResponse.Result;
        }

        /// <summary>
        /// Block header information can be retrieved using either a block's hash or height.
        /// This method includes a block's hash as an input parameter to retrieve basic information about the block.
        /// </summary>
        public async Task<Block> GetBlockAsync(string hash, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBlockAsync(hash, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockResponse, nameof(this.GetBlockAsync));
            return result.BlockResponse.Result;
        }

        /// <summary>
        /// Ban a node by IP.
        /// </summary>
        /// <returns>A string representing the banned node's status.</returns>
        public async Task<string> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SetBansAsync(bans, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetBansResponse, nameof(this.SetBansAsync));
            return result.SetBansResponse.Result.Status;
        }

        /// <summary>
        /// Submit a mined block to the network.
        /// </summary>
        /// <returns>Success of block submission.</returns>
        public async Task<bool> SubmitBlocksAsync(IEnumerable<string> blockBlobs, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SubmitBlocksAsync(blockBlobs, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitBlockResponse, nameof(this.SubmitBlocksAsync));
            return result.SubmitBlockResponse.Result != null;
        }

        /// <summary>
        /// Get a block template on which mining a new block.
        /// </summary>
        public async Task<BlockTemplate> GetBlockTemplateAsync(ulong reserveSize, string walletAddress, string prevBlock = null, string extraNonce = null, CancellationToken token = default)
        {
            if (reserveSize > 255ul)
            {
                throw new InvalidOperationException($"Maximum {nameof(reserveSize)} cannot be greater than 255.");
            }

            var result = await this.moneroRpcCommunicator.GetBlockTemplateAsync(reserveSize, walletAddress, prevBlock, extraNonce, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBlockTemplateResponse, nameof(this.GetBlockTemplateAsync));
            return result.GetBlockTemplateResponse.Result;
        }

        /// <summary>
        /// Get the status of an address that may or may not be banned.
        /// </summary>
        /// <param name="address">IP address of the node whose ban status is to be checked (e.g. 95.216.217.238)</param>
        public async Task<BanStatus> GetBanStatusAsync(string address, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBanStatusAsync(address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetBanStatusResponse, nameof(this.GetBanStatusAsync));
            return result.GetBanStatusResponse.Result;
        }

        /// <summary>
        /// Get the transaction pool's backlog.
        /// </summary>
        public async Task<TransactionPoolBacklog> GetTransactionPoolBacklogAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransactionPoolBacklogAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransactionPoolBacklogResponse, nameof(this.GetTransactionPoolBacklogAsync));
            return result.TransactionPoolBacklogResponse.Result;
        }

        public async Task<TransactionPool> GetTransactionPoolAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransactionPoolAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransactionPoolResponse == null, nameof(this.GetTransactionPoolAsync));
            return result.TransactionPoolResponse;
        }

        public async Task<List<Transaction>> GetTransactionsAsync(IEnumerable<string> txHashes, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransactionsAsync(txHashes, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransactionsResponse == null, nameof(this.GetTransactionsAsync));
            return result.TransactionsResponse?.Transactions;
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this.disposalLock)
            {
                if (this.disposed)
                {
                    return;
                }
                else
                {
                    this.disposed = true;
                }
            }

            if (disposing)
            {
                // Free managed objects.
                this.moneroRpcCommunicator.Dispose();
            }

            // Free unmanaged objects.
        }

        private Task<MoneroDaemonClient> InitializeAsync(CancellationToken cancellationToken)
        {
            // Nothing to do yet.
            return Task.FromResult(this);
        }
    }
}