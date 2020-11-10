using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Daemon.POD.Responses;

namespace Monero.Client.Daemon
{
    internal interface IMoneroDaemonDataRetriever : IDisposable
    {
        Task<MoneroDaemonResponse> GetBlockCountAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetLastBlockHeaderAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetConnectionsAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetDaemonInformationAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetHardforkInformationAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetBansAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint min_count, uint max_count, bool unlocked, uint recent_cutoff, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetVersionAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetAlternateChainsAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> RelayTransactionsAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<MoneroDaemonResponse> SyncInformationAsync(CancellationToken token = default);
        Task<MoneroDaemonResponse> GetBlockAsync(uint height, CancellationToken token = default);
        Task<MoneroDaemonResponse> GetBlockAsync(string hash, CancellationToken token = default);
        Task<MoneroDaemonResponse> SetBansAsync(IEnumerable<(string host, uint ip, bool ban,uint seconds)> bans, CancellationToken token = default);
    }
}
