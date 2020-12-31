using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Monero.Client.Daemon
{
    public interface IMoneroDaemonClient : IDisposable
    {
        /// <summary>
        /// Look up how many blocks are in the longest chain known to the node.
        /// </summary>
        Task<ulong> GetBlockCountAsync(CancellationToken token = default);
        /// <summary>
        /// Block header information for the most recent block is easily retrieved with this method.
        /// </summary>
        Task<BlockHeader> GetLastBlockHeaderAsync(CancellationToken token = default);
        /// <summary>
        /// Block header information can be retrieved using either a block's hash or height.
        /// This method includes a block's hash as an input parameter to retrieve basic information about the block.
        /// </summary>
        /// <param name="hash">The block's sha256 hash.</param>
        Task<BlockHeader> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default);
        /// <summary>
        /// Similar to get_block_header_by_hash above, this method includes a block's height as an input parameter to retrieve basic information about the block.
        /// </summary>
        /// <param name="height">The block's height.</param>
        Task<BlockHeader> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default);
        /// <summary>
        /// Similar to get_block_header_by_height above, but for a range of blocks.
        /// This method includes a starting block height and an ending block height as parameters to retrieve basic information about the range of blocks.
        /// </summary>
        /// <param name="startHeight">The starting block's height.</param>
        /// <param name="endHeight">The ending block's height.</param>
        Task<List<BlockHeader>> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default);
        /// <summary>
        /// Retrieve information about incoming and outgoing connections to your node.
        /// </summary>
        Task<List<Connection>> GetConnectionsAsync(CancellationToken token = default);
        /// <summary>
        /// Retrieve general information about the state of your node and the network.
        /// </summary>
        Task<DaemonInformation> GetDaemonInformationAsync(CancellationToken token = default);
        /// <summary>
        /// Look up information regarding hard fork voting and readiness.
        /// </summary>
        Task<HardforkInformation> GetHardforkInformationAsync(CancellationToken token = default);
        /// <summary>
        /// Get list of banned IPs.
        /// </summary>
        Task<List<Ban>> GetBanInformationAsync(CancellationToken token = default);
        /// <summary>
        /// Get the status of an address that may or may not be banned.
        /// </summary>
        /// <param name="address"></param>
        Task<BanStatus> GetBanStatusAsync(string address, CancellationToken token = default);
        /// <summary>
        /// Flush tx ids from transaction pool
        /// </summary>
        /// <param name="txids">List of transactions IDs to flush from pool (all tx ids flushed if empty).</param>
        Task<string> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default);
        /// <summary>
        /// Get a histogram of output amounts. For all amounts (possibly filtered by parameters), gives the number of outputs on the chain for that amount. RingCT outputs counts as 0 amount.
        /// </summary>
        Task<List<Distribution>> GetOutputHistogramAsync(IEnumerable<ulong> amounts, ulong fromHeight, ulong toHeight, bool cumulative = false, bool binary = true, bool compress = false, CancellationToken token = default);
        /// <summary>
        /// Get the coinbase amount and the fees amount for n last blocks starting at particular height.
        /// </summary>
        Task<CoinbaseTransactionSumResult> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default);
        /// <summary>
        /// Give the node current version.
        /// </summary>
        Task<uint> GetVersionAsync(CancellationToken token = default);
        /// <summary>
        /// Gives an estimation on fees (piconero) per byte.
        /// </summary>
        Task<ulong> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token = default);
        /// <summary>
        /// Gets all fee estimate parameters, including fees (piconero) per byte.
        /// </summary>
        Task<FeeEstimate> GetFeeEstimateParametersAsync(uint grace_blocks, CancellationToken token = default);
        /// <summary>
        /// Display alternative chains seen by the node.
        /// </summary>
        Task<List<Chain>> GetAlternateChainsAsync(CancellationToken token = default);
        /// <summary>
        /// Relay a transaction.
        /// </summary>
        Task<string> RelayTransactionAsync(string hex, CancellationToken token = default);
        /// <summary>
        /// Get synchronisation informations.
        /// </summary>
        Task<SyncronizationInformation> SyncInformationAsync(CancellationToken token = default);
        /// <summary>
        /// Similar to get_block_header_by_hash above, this method includes a block's height as an input parameter to retrieve basic information about the block.
        /// </summary>
        Task<Block> GetBlockAsync(uint height, CancellationToken token = default);
        /// <summary>
        /// Block header information can be retrieved using either a block's hash or height.
        /// This method includes a block's hash as an input parameter to retrieve basic information about the block.
        /// </summary>
        Task<Block> GetBlockAsync(string hash, CancellationToken token = default);
        /// <summary>
        /// Ban another node by IP.
        /// </summary>
        Task<string> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default);
        /// <summary>
        /// Submit a mined block to the network.
        /// </summary>
        /// <returns>Success of block submission.</returns>
        Task<bool> SubmitBlocksAsync(IEnumerable<string> blockBlob, CancellationToken token = default);
        /// <summary>
        /// Get a block template on which mining a new block.
        /// </summary>
        Task<BlockTemplate> GetBlockTemplateAsync(ulong reserveSize, string walletAddress, string prevBlock = null, string extraNonce = null, CancellationToken token = default);
        Task<PruneBlockchain> PruneBlockchainAsync(bool check = false, CancellationToken token = default);
    }
}
