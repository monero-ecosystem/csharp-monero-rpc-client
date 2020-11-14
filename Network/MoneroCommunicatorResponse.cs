using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Network
{
    internal enum MoneroResponseType
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
        Account,
        Address,
        Wallet,
        Daemon,
        Miscellaneous,
    }

    internal enum MoneroResponseSubType
    {
        // Daemon
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
        DaemonVersion,
        FeeEstimate,
        AlternateChain,
        RelayTransaction,
        SyncInformation,
        SetBans,

        // Wallet
        Balance,
        Address,
        AddressIndex,
        AddressCreation,
        AddressLabeling,
        Account,
        AccountCreation,
        AccountLabeling,
        AccountTags,
        AccountTagging,
        AccountUntagging,
        AccountTagAndDescriptionSetting,
        Height,
        FundTransfer,
        FundTransferSplit,
        SignTransfer,   // Cold-signing process.
        SubmitTransfer, // Cold-signing process.
        SweepDust,
        SweepAll,
        SaveWallet,
        StopWallet,
        IncomingTransfers,
        QueryPrivateKey,
        SetTransactionNotes,
        GetTransactionNotes,
        GetTransactionKey,
        CheckTransactionKey,
        Transfers,
        TransferByTxid,
        Sign,
        Verify,
        ImportOutputs,
        ExportOutputs,
        ImportKeyImages,
        ExportKeyImages,
        MakeUri,
        ParseUri,
        GetAddressBook,
        AddAddressBook,
        DeleteAddressBook,
        Refresh,
        RescanSpent,
        Languages,
        CreateWallet,
        OpenWallet,
        CloseWallet,
        ChangeWalletPassword,
        RpcVersion,
        IsMultiSig,
        PrepareMultiSig,
        MakeMultiSig,
        ExportMultiSigInfo,
        ImportMultiSigInfo,
        FinalizeMultiSig,
        SignMultiSigTransaction,
        SubmitMultiSigTransaction,
    }

    internal class MoneroCommunicatorResponse
    {
        public MoneroResponseType MoneroResponseType { get; set; } = MoneroResponseType.None;
        public MoneroResponseSubType MoneroResponseSubType { get; set; }
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
        internal BalanceResponse BalanceResponse { get; set; }
        internal AddressResponse AddressResponse { get; set; }
        internal AddressIndexResponse AddressIndexResponse { get; set; }
        internal AddressCreationResponse AddressCreationResponse { get; set; }
        internal AddressLabelResponse AddressLabelResponse { get; set; }
        internal AccountResponse AccountResponse { get; set; }
        internal CreateAccountResponse CreateAccountResponse { get; set; }
        internal AccountLabelResponse AccountLabelResponse { get; set; }
        internal AccountTagsResponse AccountTagsResponse { get; set; }
        internal TagAccountsResponse TagAccountsResponse { get; set; }
        internal UntagAccountsResponse UntagAccountsResponse { get; set; }
        internal AccountTagAndDescriptionResponse SetAccountTagAndDescriptionResponse { get; set; }
        internal BlockchainHeightResponse BlockchainHeightResponse { get; set; }
        internal FundTransferResponse FundTransferResponse { get; set; }
        internal SplitFundTransferResponse FundTransferSplitResponse { get; set; }
        internal SignTransferResponse SignTransferResponse { get; set; }
        internal SubmitTransferResponse SubmitTransferResponse { get; set; }
        internal SweepDustResponse SweepDustResponse { get; set; }
        internal SweepAllResponse SweepAllResponse { get; set; }
        internal SaveWalletResponse SaveWalletResponse { get; set; }
        internal StopWalletResponse StopWalletResponse { get; set; }
        internal IncomingTransfersResponse IncomingTransfersResponse { get; set; }
        internal QueryKeyResponse QueryKeyResponse { get; set; }
        internal SetTransactionNotesResponse SetTransactionNotesResponse { get; set; }
        internal GetTransactionNotesResponse GetTransactionNotesResponse { get; set; }
        internal GetTransactionKeyResponse GetTransactionKeyResponse { get; set; }
        internal CheckTransactionKeyResponse CheckTransactionKeyResponse { get; set; }
        internal ShowTransfersResponse ShowTransfersResponse { get; set; }
        internal GetTransferByTxidResponse TransferByTxidResponse { get; set; }
        internal SignResponse SignResponse { get; set; }
        internal VerifyResponse VerifyResponse { get; set; }
        internal ExportOutputsResponse ExportOutputsResponse { get; set; }
        internal ImportOutputsResponse ImportOutputsResponse { get; set; }
        internal ExportKeyImagesResponse ExportKeyImagesResponse { get; set; }
        internal ImportKeyImagesResponse ImportKeyImagesResponse { get; set; }
        internal MakeUriResponse MakeUriResponse { get; set; }
        internal ParseUriResponse ParseUriResponse { get; set; }
        internal GetAddressBookResponse GetAddressBookResponse { get; set; }
        internal AddAddressBookResponse AddAddressBookResponse { get; set; }
        internal DeleteAddressBookResponse DeleteAddressBookResponse { get; set; }
        internal RefreshWalletResponse RefreshWalletResponse { get; set; }
        internal RescanSpentResponse RescanSpentResponse { get; set; }
        internal LanguagesResponse LanguagesResponse { get; set; }
        internal CreateWalletResponse CreateWalletResponse { get; set; }
        internal OpenWalletResponse OpenWalletResponse { get; set; }
        internal CloseWalletResponse CloseWalletResponse { get; set; }
        internal ChangeWalletPasswordResponse ChangeWalletPasswordResponse { get; set; }
        internal GetRpcVersionResponse GetRpcVersionResponse { get; set; }
        internal IsMultiSigInformationResponse IsMultiSigInformationResponse { get; set; }
        internal PrepareMultiSigResponse PrepareMultiSigResponse { get; set; }
        internal MakeMultiSigResponse MakeMultiSigResponse { get; set; }
        internal ExportMultiSigInfoResponse ExportMultiSigInfoResponse { get; set; }
        internal ImportMultiSigInfoResponse ImportMultiSigInfoResponse { get; set; }
        internal FinalizeMultiSigResponse FinalizeMultiSigResponse { get; set; }
        internal SignMultiSigTransactionResponse SignMultiSigTransactionResponse { get; set; }
        internal SubmitMultiSigTransactionResponse SubmitMultiSigTransactionResponse { get; set; }
    }
}