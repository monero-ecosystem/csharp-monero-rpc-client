using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Network;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Daemon
{
    public interface IMoneroDaemonClient : IDisposable
    {
        Task<ulong> GetBlockCountAsync(CancellationToken token = default);
        Task<BlockHeader> GetLastBlockHeaderAsync(CancellationToken token = default);
        Task<BlockHeader> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default);
        Task<BlockHeader> GetBlockHeaderByHeightAsync(ulong height, CancellationToken token = default);
        Task<List<BlockHeader>> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default);
        Task<List<Connection>> GetConnectionsAsync(CancellationToken token = default);
        Task<DaemonInformation> GetDaemonInformationAsync(CancellationToken token = default);
        Task<HardforkInformation> GetHardforkInformationAsync(CancellationToken token = default);
        Task<List<Ban>> GetBanInformationAsync(CancellationToken token = default);
        Task<BanStatus> GetBanStatusAsync(string address, CancellationToken token = default);
        Task<string> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<List<Distribution>> GetOutputHistogramAsync(IEnumerable<ulong> amounts, ulong fromHeight, ulong toHeight, bool cumulative = false, bool binary = true, bool compress = false, CancellationToken token = default);
        Task<CoinbaseTransactionSum> GetCoinbaseTransactionSumAsync(ulong height, uint count, CancellationToken token = default);
        Task<uint> GetVersionAsync(CancellationToken token = default);
        Task<ulong> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token = default);
        Task<FeeEstimate> GetFeeEstimateParametersAsync(uint grace_blocks, CancellationToken token = default);
        Task<List<Chain>> GetAlternateChainsAsync(CancellationToken token = default);
        Task<string> RelayTransactionAsync(string hex, CancellationToken token = default);
        Task<SyncronizationInformation> SyncInformationAsync(CancellationToken token = default);
        Task<Block> GetBlockAsync(uint height, CancellationToken token = default);
        Task<Block> GetBlockAsync(string hash, CancellationToken token = default);
        Task<string> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default);
        Task<bool> SubmitBlocksAsync(IEnumerable<string> blockBlob, CancellationToken token = default);
        Task<BlockTemplate> GetBlockTemplateAsync(ulong reserveSize, string walletAddress, string prevBlock = null, string extraNonce = null, CancellationToken token = default);
        Task<TransactionPoolBacklog> GetTransactionPoolBacklogAsync(CancellationToken token = default);
        Task<TransactionPool> GetTransactionPoolAsync(CancellationToken token = default);
        Task<List<Transaction>> GetTransactionsAsync(IEnumerable<string> txHashes, CancellationToken token = default);
    }
}
