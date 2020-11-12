using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Daemon.POD.Responses;

namespace Monero.Client.Daemon
{
    public enum MoneroDaemonResponseType
    {
        None,
        Block,
        BlockCount,
        BlockHeader,
        BlockHeaderRange,
        Connection,
        Information,
        TransactionPool,
        Coinbase,
        Blockchain,
        Transaction,
    }

    public enum MoneroDaemonResponseSubType
    {
        Block,
        BlockCount,
        BlockHeaderByHash,
        BlockHeaderByHeight,
        BlockHeaderByRange,
        BlockHeaderByRecency, // Last Block Header.
        AllConnections,
        NodeInformation,
        HardforkInformation,
        BanInformation,
        FlushTransactionPool,
        OutputHistogram,
        CoinbaseTransactionSum,
        Version,
        FeeEstimate,
        AlternateChain,
        RelayTransaction,
        SyncInformation,
        SetBans,
    }

    internal class MoneroDaemonCommunicatorResponse
    {
        public MoneroDaemonResponseType MoneroNodeResponseType { get; set; } = MoneroDaemonResponseType.None;
        public MoneroDaemonResponseSubType MoneroNodeResponseSubType { get; set; }
        public BlockCountResponse BlockCountResponse { get; set; }
        public BlockHeaderResponse BlockHeaderResponse { get; set; }
        public BlockHeaderRangeResponse BlockHeaderRangeResponse { get; set; }
        public ConnectionResponse ConnectionResponse { get; set; }
        public DaemonInformationResponse DaemonInformationResponse { get; set; }
        public HardforkInformationResponse HardforkInformationResponse { get; set; }
        public GetBansResponse GetBansResponse { get; set; }
        public FlushTransactionPoolResponse FlushTransactionPoolResponse { get; set; }
        public OutputHistogramResponse OutputHistogramResponse { get; set; }
        public CoinbaseTransactionSumResponse CoinbaseTransactionSumReponse { get; set; }
        public VersionResponse VersionResponse { get; set; }
        public FeeEstimateResponse FeeEstimateResponse { get; set; }
        public AlternateChainResponse AlternateChainResponse { get; set; }
        public RelayTransactionResponse RelayTransactionResponse { get; set; }
        public SyncronizeInformationResponse SyncronizeInformationResponse { get; set; }
        public TransactionPoolBacklogResponse TransactionPoolBacklogResponse { get; set; }
        public BlockResponse BlockResponse { get; set; }
        public SetBansResponse SetBansResponse { get; set; }
    }
}