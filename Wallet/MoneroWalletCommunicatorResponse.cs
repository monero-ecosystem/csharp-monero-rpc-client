using System;
using System.Collections.Generic;
using System.Text;
using Monero.Client.Network;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    public enum MoneroWalletResponseType
    {
        None,
        Account,
        Address,
        Blockchain,
        Transaction,
        Wallet,
        Miscellaneous
    }

    public enum MoneroWalletResponseSubType
    {
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

    internal class MoneroWalletCommunicatorResponse
    {
        internal MoneroWalletResponseType MoneroWalletResponseType { get; set; } = MoneroWalletResponseType.None;
        internal MoneroWalletResponseSubType MoneroWalletResponseSubType { get; set; }
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