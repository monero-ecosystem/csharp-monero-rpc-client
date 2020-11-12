using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Daemon.POD.Responses;

namespace Monero.Client.Daemon
{
    internal interface IMoneroDaemonRpcCommunicator : IDisposable
    {
        Task<MoneroDaemonCommunicatorResponse> GetBlockCountAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetLastBlockHeaderAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetBlockHeaderByHashAsync(string hash, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetBlockHeaderByHeightAsync(uint height, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetConnectionsAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetDaemonInformationAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetHardforkInformationAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetBansAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint min_count, uint max_count, bool unlocked, uint recent_cutoff, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetVersionAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetAlternateChainsAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> RelayTransactionsAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> SyncInformationAsync(CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetBlockAsync(uint height, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> GetBlockAsync(string hash, CancellationToken token = default);
        Task<MoneroDaemonCommunicatorResponse> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban,uint seconds)> bans, CancellationToken token = default);
    }
}
