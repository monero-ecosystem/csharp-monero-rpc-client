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

    public class MoneroWalletResponse
    {
        public MoneroWalletResponseType MoneroWalletResponseType { get; set; } = MoneroWalletResponseType.None;
        public MoneroWalletResponseSubType MoneroWalletResponseSubType { get; set; }
        public BalanceResponse BalanceResponse { get; set; }
        public AddressResponse AddressResponse { get; set; }
        public AddressIndexResponse AddressIndexResponse { get; set; }
        public AddressCreationResponse AddressCreationResponse { get; set; }
        public AddressLabelResponse AddressLabelResponse { get; set; }
        public AccountResponse AccountResponse { get; set; }
        public CreateAccountResponse CreateAccountResponse { get; set; }
        public AccountLabelResponse AccountLabelResponse { get; set; }
        public AccountTagsResponse AccountTagsResponse { get; set; }
        public TagAccountsResponse TagAccountsResponse { get; set; }
        public UntagAccountsResponse UntagAccountsResponse { get; set; }
        public AccountTagAndDescriptionResponse AccountTagAndDescriptionResponse { get; set; }
        public BlockchainHeightResponse BlockchainHeightResponse { get; set; }
        public FundTransferResponse FundTransferResponse { get; set; }
        public FundTransferSplitResponse FundTransferSplitResponse { get; set; }
        public SignTransferResponse SignTransferResponse { get; set; }
        public SubmitTransferResponse SubmitTransferResponse { get; set; }
        public SweepDustResponse SweepDustResponse { get; set; }
        public SweepAllResponse SweepAllResponse { get; set; }
        public SaveWalletResponse SaveWalletResponse { get; set; }
        public StopWalletResponse StopWalletResponse { get; set; }
        public IncomingTransfersResponse IncomingTransfersResponse { get; set; }
        public QueryKeyResponse QueryKeyResponse { get; set; }
        public SetTransactionNotesResponse SetTransactionNotesResponse { get; set; }
        public GetTransactionNotesResponse GetTransactionNotesResponse { get; set; }
        public GetTransactionKeyResponse GetTransactionKeyResponse { get; set; }
        public CheckTransactionKeyResponse CheckTransactionKeyResponse { get; set; }
        public ShowTransfersResponse ShowTransfersResponse { get; set; }
        public ShowTransferByTxidResponse ShowTransferByTxidResponse { get; set; }
        public SignResponse SignResponse { get; set; }
        public VerifyResponse VerifyResponse { get; set; }
        public ExportOutputsResponse ExportOutputsResponse { get; set; }
        public ImportOutputsResponse ImportOutputsResponse { get; set; }
        public ExportKeyImagesResponse ExportKeyImagesResponse { get; set; }
        public ImportKeyImagesResponse ImportKeyImagesResponse { get; set; }
        public MakeUriResponse MakeUriResponse { get; set; }
        public ParseUriResponse ParseUriResponse { get; set; }
        public GetAddressBookResponse GetAddressBookResponse { get; set; }
        public AddAddressBookResponse AddAddressBookResponse { get; set; }
        public DeleteAddressBookResponse DeleteAddressBookResponse { get; set; }
        public RefreshWalletResponse RefreshWalletResponse { get; set; }
        public RescanSpentResponse RescanSpentResponse { get; set; }
        public LanguagesResponse LanguagesResponse { get; set; }
        public CreateWalletResponse CreateWalletResponse { get; set; }
        public OpenWalletResponse OpenWalletResponse { get; set; }
        public CloseWalletResponse CloseWalletResponse { get; set; }
        public ChangeWalletPasswordResponse ChangeWalletPasswordResponse { get; set; }
        public GetRpcVersionResponse GetRpcVersionResponse { get; set; }
        public IsMultiSigResponse IsMultiSigResponse { get; set; }
        public PrepareMultiSigResponse PrepareMultiSigResponse { get; set; }
        public MakeMultiSigResponse MakeMultiSigResponse { get; set; }
        public ExportMultiSigInfoResponse ExportMultiSigInfoResponse { get; set; }
        public ImportMultiSigInfoResponse ImportMultiSigInfoResponse { get; set; }
        public FinalizeMultiSigResponse FinalizeMultiSigResponse { get; set; }
        public SignMultiSigTransactionResponse SignMultiSigTransactionResponse { get; set; }
        public SubmitMultiSigTransactionResponse SubmitMultiSigTransactionResponse { get; set; }
    }
}