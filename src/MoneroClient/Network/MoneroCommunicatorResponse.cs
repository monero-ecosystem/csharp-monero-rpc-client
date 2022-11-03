using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Enums;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Network
{
    internal class MoneroCommunicatorResponse
    {
        internal MoneroResponseType MoneroResponseType { get; set; } = MoneroResponseType.None;
        internal MoneroResponseSubType MoneroResponseSubType { get; set; }

        // Daemon-related responses.
        internal BlockCountResponse BlockCountResponse { get; set; }
        internal BlockHeaderResponse BlockHeaderResponse { get; set; }
        internal BlockHeaderRangeResponse BlockHeaderRangeResponse { get; set; }
        internal ConnectionResponse ConnectionResponse { get; set; }
        internal DaemonInformationResponse DaemonInformationResponse { get; set; }
        internal HardforkInformationResponse HardforkInformationResponse { get; set; }
        internal GetBansResponse GetBansResponse { get; set; }
        internal FlushTransactionPoolResponse FlushTransactionPoolResponse { get; set; }
        internal OutputHistogramResponse OutputHistogramResponse { get; set; }
        internal CoinbaseTransactionSumResponse CoinbaseTransactionSumReponse { get; set; }
        internal VersionResponse VersionResponse { get; set; }
        internal FeeEstimateResponse FeeEstimateResponse { get; set; }
        internal AlternateChainResponse AlternateChainResponse { get; set; }
        internal RelayTransactionResponse RelayTransactionResponse { get; set; }
        internal SyncronizeInformationResponse SyncronizeInformationResponse { get; set; }
        internal TransactionPoolBacklogResponse TransactionPoolBacklogResponse { get; set; }
        internal BlockResponse BlockResponse { get; set; }
        internal SetBansResponse SetBansResponse { get; set; }
        internal SubmitBlockResponse SubmitBlockResponse { get; set; }
        internal GetBlockTemplateResponse GetBlockTemplateResponse { get; set; }
        internal GetBanStatusResponse GetBanStatusResponse { get; set; }
        internal PruneBlockchainResponse PruneBlockchainResponse { get; set; }
        internal TransactionSet TransactionsResponse { get; set; }
        internal TransactionPool TransactionPoolResponse { get; set; }

        // Wallet-related responses.
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
        internal MultiSigInformationResponse MultiSigInformationResponse { get; set; }
        internal PrepareMultiSigResponse PrepareMultiSigResponse { get; set; }
        internal MakeMultiSigResponse MakeMultiSigResponse { get; set; }
        internal ExportMultiSigInfoResponse ExportMultiSigInfoResponse { get; set; }
        internal ImportMultiSigInfoResponse ImportMultiSigInfoResponse { get; set; }
        internal FinalizeMultiSigResponse FinalizeMultiSigResponse { get; set; }
        internal SignMultiSigTransactionResponse SignMultiSigTransactionResponse { get; set; }
        internal SubmitMultiSigTransactionResponse SubmitMultiSigTransactionResponse { get; set; }
        internal DescribeTransferResponse DescribeTransferResponse { get; set; }
        internal SweepSingleResponse SweepSingleResponse { get; set; }
        internal PaymentDetailResponse PaymentDetailResponse { get; set; }
        internal SetAttributeResponse SetAttributeResponse { get; set; }
        internal GetAttributeResponse GetAttributeResponse { get; set; }
        internal ValidateAddressResponse ValidateAddressResponse { get; set; }
    }
}