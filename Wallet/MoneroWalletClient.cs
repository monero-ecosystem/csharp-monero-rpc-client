using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Network;
using Monero.Client.Utilities;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    public class MoneroWalletClient : IMoneroWalletClient
    {
        private readonly IMoneroWalletRpcCommunicator _moneroRpcWalletCommunicator;
        private bool _walletCurrentlyOpen = false;
        private string _walletCurrentlyOpenName = string.Empty;
        public MoneroWalletClient(Uri uri)
        {
            _moneroRpcWalletCommunicator = new MoneroWalletRpcCommunicator(uri);
        }

        public MoneroWalletClient(Uri uri, HttpMessageHandler httpMessageHandler)
        {
            _moneroRpcWalletCommunicator = new MoneroWalletRpcCommunicator(uri, httpMessageHandler);
        }

        public MoneroWalletClient(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _moneroRpcWalletCommunicator = new MoneroWalletRpcCommunicator(uri, httpMessageHandler, disposeHandler);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>)
        /// </summary>
        public MoneroWalletClient(MoneroNetwork networkType)
        {
            _moneroRpcWalletCommunicator = new MoneroWalletRpcCommunicator(networkType);
        }

        public void Dispose()
        {
            if (_walletCurrentlyOpen)
            {
                this.CloseWalletAsync().GetAwaiter().GetResult();
            }
            _moneroRpcWalletCommunicator.Dispose();
        }

        public async Task<Balance> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetBalanceAsync));
            var result = await _moneroRpcWalletCommunicator.GetBalanceAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse?.Result == null, nameof(GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        public async Task<Balance> GetBalanceAsync(uint accountIndex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetBalanceAsync));
            var result = await _moneroRpcWalletCommunicator.GetBalanceAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse?.Result == null, nameof(GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAddressAsync));
            var result = await _moneroRpcWalletCommunicator.GetAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse?.Result == null, nameof(GetAddressAsync));
            return result.AddressResponse.Result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAddressAsync));
            var result = await _moneroRpcWalletCommunicator.GetAddressAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse?.Result == null, nameof(GetAddressAsync));
            return result.AddressResponse.Result;
        }

        public async Task<AddressIndex> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAddressIndexAsync));
            var result = await _moneroRpcWalletCommunicator.GetAddressIndexAsync(address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressIndexResponse?.Result == null, nameof(GetAddressIndexAsync));
            return result.AddressIndexResponse.Result;
        }

        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(CreateAddressAsync));
            var result = await _moneroRpcWalletCommunicator.CreateAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse?.Result == null, nameof(CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(CreateAddressAsync));
            var result = await _moneroRpcWalletCommunicator.CreateAddressAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse?.Result == null, nameof(CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        public async Task<AddressLabel> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(LabelAddressAsync));
            var result = await _moneroRpcWalletCommunicator.LabelAddressAsync(majorIndex, minorIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressLabelResponse?.Result == null, nameof(LabelAddressAsync));
            return result.AddressLabelResponse.Result;
        }

        public async Task<AccountResult> GetAccountsAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAccountsAsync));
            var result = await _moneroRpcWalletCommunicator.GetAccountsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse?.Result == null, nameof(GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        public async Task<AccountResult> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAccountsAsync));
            var result = await _moneroRpcWalletCommunicator.GetAccountsAsync(tag, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse?.Result == null, nameof(GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        public async Task<CreateAccount> CreateAccountAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(CreateAccountAsync));
            var result = await _moneroRpcWalletCommunicator.CreateAccountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse?.Result == null, nameof(CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        public async Task<CreateAccount> CreateAccountAsync(string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(CreateAccountAsync));
            var result = await _moneroRpcWalletCommunicator.CreateAccountAsync(label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse?.Result == null, nameof(CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        public async Task<AccountLabel> LabelAccountAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(LabelAccountAsync));
            var result = await _moneroRpcWalletCommunicator.LabelAccountAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountLabelResponse?.Result == null, nameof(LabelAccountAsync));
            return result.AccountLabelResponse.Result;
        }

        public async Task<AccountTags> GetAccountTagsAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAccountTagsAsync));
            var result = await _moneroRpcWalletCommunicator.GetAccountTagsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountTagsResponse?.Result == null, nameof(GetAccountTagsAsync));
            return result.AccountTagsResponse.Result;
        }

        public async Task<TagAccounts> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TagAccountsAsync));
            var result = await _moneroRpcWalletCommunicator.TagAccountsAsync(tag, accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TagAccountsResponse?.Result == null, nameof(TagAccountsAsync));
            return result.TagAccountsResponse.Result;
        }

        public async Task<UntagAccounts> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(UntagAccountsAsync));
            var result = await _moneroRpcWalletCommunicator.UntagAccountsAsync(accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.UntagAccountsResponse?.Result == null, nameof(UntagAccountsAsync));
            return result.UntagAccountsResponse.Result;
        }

        public async Task<AccountTagAndDescription> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SetAccountTagDescriptionAsync));
            var result = await _moneroRpcWalletCommunicator.SetAccountTagDescriptionAsync(tag, description, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetAccountTagAndDescriptionResponse?.Result == null, nameof(SetAccountTagDescriptionAsync));
            return result.SetAccountTagAndDescriptionResponse.Result;
        }

        public async Task<BlockchainHeight> GetHeightAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetHeightAsync));
            var result = await _moneroRpcWalletCommunicator.GetHeightAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockchainHeightResponse?.Result == null, nameof(GetHeightAsync));
            return result.BlockchainHeightResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferAsync));
            var result = await _moneroRpcWalletCommunicator.TransferAsync(transactions, transferPriority, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse?.Result == null, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, uint unlockTime = 0, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferAsync));
            var result = await _moneroRpcWalletCommunicator.TransferAsync(transactions, transferPriority, getTxKey, getTxHex, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse?.Result == null, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferAsync));
            var result = await _moneroRpcWalletCommunicator.TransferAsync(transactions, transferPriority, ringSize, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse?.Result == null, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferAsync));
            var result = await _moneroRpcWalletCommunicator.TransferAsync(transactions, transferPriority, ringSize, accountIndex, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse?.Result == null, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferSplitAsync));
            var result = await _moneroRpcWalletCommunicator.TransferSplitAsync(transactions, transferPriority, newAlgorithm, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse?.Result == null, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, uint unlockTime = 0, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferSplitAsync));
            var result = await _moneroRpcWalletCommunicator.TransferSplitAsync(transactions, transferPriority, getTxKey, getTxHex, newAlgorithm, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse?.Result == null, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferSplitAsync));
            var result = await _moneroRpcWalletCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse?.Result == null, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(TransferSplitAsync));
            var result = await _moneroRpcWalletCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, accountIndex, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse?.Result == null, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SignTransfer> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SignTransferAsync));
            var result = await _moneroRpcWalletCommunicator.SignTransferAsync(unsignedTxSet, exportRaw, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignTransferResponse?.Result == null, nameof(SignTransferAsync));
            return result.SignTransferResponse.Result;
        }

        public async Task<SubmitTransfer> SubmitTransferAsync(string txDataHex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SubmitTransferAsync));
            var result = await _moneroRpcWalletCommunicator.SubmitTransferAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitTransferResponse?.Result == null, nameof(SubmitTransferAsync));
            return result.SubmitTransferResponse.Result;
        }

        public async Task<SweepDust> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SweepDustAsync));
            var result = await _moneroRpcWalletCommunicator.SweepDustAsync(getTxKey, getTxHex, getTxMetadata, doNotRelay, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepDustResponse?.Result == null, nameof(SweepDustAsync));
            return result.SweepDustResponse.Result;
        }

        public async Task<SplitFundTransfer> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SweepAllAsync));
            var result = await _moneroRpcWalletCommunicator.SweepAllAsync(address, accountIndex, transactionPriority, ringSize, unlockTime, belowAmount, getTxKeys, getTxHex, getTxMetadata, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepAllResponse?.Result == null, nameof(SweepAllAsync));
            return result.SweepAllResponse.Result;
        }

        public async Task<SaveWallet> SaveWalletAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SaveWalletAsync));
            var result = await _moneroRpcWalletCommunicator.SaveWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SaveWalletResponse?.Result == null, nameof(SaveWalletAsync));
            return result.SaveWalletResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetIncomingTransfersAsync));
            var result = await _moneroRpcWalletCommunicator.GetIncomingTransfersAsync(transferType, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse?.Result == null, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetIncomingTransfersAsync));
            var result = await _moneroRpcWalletCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse?.Result == null, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetIncomingTransfersAsync));
            var result = await _moneroRpcWalletCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, subaddrIndices, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse?.Result == null, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<QueryKey> GetPrivateKey(KeyType keyType, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetPrivateKey));
            var result = await _moneroRpcWalletCommunicator.GetPrivateKey(keyType, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.QueryKeyResponse?.Result == null, nameof(GetPrivateKey));
            return result.QueryKeyResponse.Result;
        }

        public async Task<StopWalletResult> StopWalletAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(StopWalletAsync));
            var result = await _moneroRpcWalletCommunicator.StopWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.StopWalletResponse?.Result == null, nameof(StopWalletAsync));
            return result.StopWalletResponse.Result;
        }

        public async Task<SetTransactionNotes> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SetTransactionNotesAsync));
            var result = await _moneroRpcWalletCommunicator.SetTransactionNotesAsync(txids, notes, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetTransactionNotesResponse?.Result == null, nameof(SetTransactionNotesAsync));
            return result.SetTransactionNotesResponse.Result;
        }

        public async Task<GetTransactionNotes> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetTransactionNotesAsync));
            var result = await _moneroRpcWalletCommunicator.GetTransactionNotesAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionNotesResponse?.Result == null, nameof(GetTransactionNotesAsync));
            return result.GetTransactionNotesResponse.Result;
        }

        public async Task<GetTransactionKey> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetTransactionKeyAsync));
            var result = await _moneroRpcWalletCommunicator.GetTransactionKeyAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionKeyResponse?.Result == null, nameof(GetTransactionKeyAsync));
            return result.GetTransactionKeyResponse.Result;
        }

        public async Task<CheckTransactionKey> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(CheckTransactionKeyAsync));
            var result = await _moneroRpcWalletCommunicator.CheckTransactionKeyAsync(txid, txKey, address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CheckTransactionKeyResponse?.Result == null, nameof(CheckTransactionKeyAsync));
            return result.CheckTransactionKeyResponse.Result;
        }

        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetTransfersAsync));
            var result = await _moneroRpcWalletCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse?.Result == null, nameof(GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint minHeight, uint maxHeight, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetTransfersAsync));
            var result = await _moneroRpcWalletCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, minHeight, maxHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse?.Result == null, nameof(GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        public async Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetTransferByTxidAsync));
            var result = await _moneroRpcWalletCommunicator.GetTransferByTxidAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse?.Result == null, nameof(GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result;
        }

        public async Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetTransferByTxidAsync));
            var result = await _moneroRpcWalletCommunicator.GetTransferByTxidAsync(txid, accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse?.Result == null, nameof(GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result;
        }

        public async Task<Signature> SignAsync(string data, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SignAsync));
            var result = await _moneroRpcWalletCommunicator.SignAsync(data, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignResponse?.Result == null, nameof(SignAsync));
            return result.SignResponse.Result;
        }

        public async Task<VerifyResult> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(VerifyAsync));
            var result = await _moneroRpcWalletCommunicator.VerifyAsync(data, address, signature, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.VerifyResponse?.Result == null, nameof(VerifyAsync));
            return result.VerifyResponse.Result;
        }

        public async Task<ExportOutputs> ExportOutputsAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ExportOutputsAsync));
            var result = await _moneroRpcWalletCommunicator.ExportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportOutputsResponse?.Result == null, nameof(ExportOutputsAsync));
            return result.ExportOutputsResponse.Result;
        }

        public async Task<ImportOutputsResult> ImportOutputsAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ImportOutputsAsync));
            var result = await _moneroRpcWalletCommunicator.ImportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportOutputsResponse?.Result == null, nameof(ImportOutputsAsync));
            return result.ImportOutputsResponse.Result;
        }

        public async Task<ExportKeyImages> ExportKeyImagesAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ExportKeyImagesAsync));
            var result = await _moneroRpcWalletCommunicator.ExportKeyImagesAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportKeyImagesResponse?.Result == null, nameof(ExportKeyImagesAsync));
            return result.ExportKeyImagesResponse.Result;
        }

        public async Task<ImportKeyImages> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ImportKeyImagesAsync));
            var result = await _moneroRpcWalletCommunicator.ImportKeyImagesAsync(signedKeyImages, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportKeyImagesResponse?.Result == null, nameof(ImportKeyImagesAsync));
            return result.ImportKeyImagesResponse.Result;
        }

        public async Task<MakeUri> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(MakeUriAsync));
            var result = await _moneroRpcWalletCommunicator.MakeUriAsync(address, amount, recipientName, txDescription, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeUriResponse?.Result == null, nameof(MakeUriAsync));
            return result.MakeUriResponse.Result;
        }

        public async Task<ParseUri> ParseUriAsync(string uri, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ParseUriAsync));
            var result = await _moneroRpcWalletCommunicator.ParseUriAsync(uri, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ParseUriResponse?.Result == null, nameof(ParseUriAsync));
            return result.ParseUriResponse.Result;
        }

        public async Task<GetAddressBook> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetAddressBookAsync));
            var result = await _moneroRpcWalletCommunicator.GetAddressBookAsync(entries, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetAddressBookResponse?.Result == null, nameof(GetAddressBookAsync));
            return result.GetAddressBookResponse.Result;
        }

        public async Task<AddAddressBook> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(AddAddressBookAsync));
            var result = await _moneroRpcWalletCommunicator.AddAddressBookAsync(address, description, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddAddressBookResponse?.Result == null, nameof(AddAddressBookAsync));
            return result.AddAddressBookResponse.Result;
        }

        public async Task<DeleteAddressBook> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(DeleteAddressBookAsync));
            var result = await _moneroRpcWalletCommunicator.DeleteAddressBookAsync(index, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DeleteAddressBookResponse?.Result == null, nameof(DeleteAddressBookAsync));
            return result.DeleteAddressBookResponse.Result;
        }

        public async Task<RefreshWallet> RefreshWalletAsync(uint startHeight, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(RefreshWalletAsync));
            var result = await _moneroRpcWalletCommunicator.RefreshWalletAsync(startHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RefreshWalletResponse?.Result == null, nameof(RefreshWalletAsync));
            return result.RefreshWalletResponse.Result;
        }

        public async Task<RescanSpent> RescanSpentAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(RescanSpentAsync));
            var result = await _moneroRpcWalletCommunicator.RescanSpentAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RescanSpentResponse?.Result == null, nameof(RescanSpentAsync));
            return result.RescanSpentResponse.Result;
        }

        public async Task<CreateWallet> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletOpen(_walletCurrentlyOpen, nameof(CreateWalletAsync));
            var result = await _moneroRpcWalletCommunicator.CreateWalletAsync(filename, language, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateWalletResponse?.Result == null, nameof(CreateWalletAsync));
            _walletCurrentlyOpen = true;
            _walletCurrentlyOpenName = filename;
            return result.CreateWalletResponse.Result;
        }

        public async Task<OpenWallet> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletOpen(_walletCurrentlyOpen, nameof(OpenWalletAsync));
            var result = await _moneroRpcWalletCommunicator.OpenWalletAsync(filename, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OpenWalletResponse?.Result == null, nameof(OpenWalletAsync));
            _walletCurrentlyOpen = true;
            _walletCurrentlyOpenName = filename;
            return result.OpenWalletResponse.Result;
        }

        // Note: After you close a wallet, every subsequent query will fail.
        public async Task<CloseWallet> CloseWalletAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(CloseWalletAsync));
            var result = await _moneroRpcWalletCommunicator.CloseWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CloseWalletResponse?.Result == null, nameof(CloseWalletAsync));
            _walletCurrentlyOpen = false;
            _walletCurrentlyOpenName = string.Empty;
            return result.CloseWalletResponse.Result;
        }

        public async Task<ChangeWalletPassword> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ChangeWalletPasswordAsync));
            var result = await _moneroRpcWalletCommunicator.ChangeWalletPasswordAsync(oldPassword, newPassword, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ChangeWalletPasswordResponse?.Result == null, nameof(ChangeWalletPasswordAsync));
            return result.ChangeWalletPasswordResponse.Result;
        }

        public async Task<GetVersion> GetVersionAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(GetVersionAsync));
            var result = await _moneroRpcWalletCommunicator.GetVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetRpcVersionResponse?.Result == null, nameof(GetVersionAsync));
            return result.GetRpcVersionResponse.Result;
        }

        public async Task<IsMultiSigInformation> IsMultiSigAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(IsMultiSigAsync));
            var result = await _moneroRpcWalletCommunicator.IsMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IsMultiSigInformationResponse?.Result == null, nameof(IsMultiSigAsync));
            return result.IsMultiSigInformationResponse.Result;
        }

        public async Task<PrepareMultiSig> PrepareMultiSigAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(PrepareMultiSigAsync));
            var result = await _moneroRpcWalletCommunicator.PrepareMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.PrepareMultiSigResponse?.Result == null, nameof(PrepareMultiSigAsync));
            return result.PrepareMultiSigResponse.Result;
        }

        public async Task<MakeMultiSig> MakeMultiSigAsync(IEnumerable<string> multiSigInfo, uint threshold, string password, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(MakeMultiSigAsync));
            var result = await _moneroRpcWalletCommunicator.MakeMultiSigAsync(multiSigInfo, threshold, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeMultiSigResponse?.Result == null, nameof(MakeMultiSigAsync));
            return result.MakeMultiSigResponse.Result;
        }

        public async Task<ExportMultiSigInformation> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ExportMultiSigInfoAsync));
            var result = await _moneroRpcWalletCommunicator.ExportMultiSigInfoAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportMultiSigInfoResponse?.Result == null, nameof(ExportMultiSigInfoAsync));
            return result.ExportMultiSigInfoResponse.Result;
        }

        public async Task<ImportMultiSigInformation> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(ImportMultiSigInfoAsync));
            var result = await _moneroRpcWalletCommunicator.ImportMultiSigInfoAsync(info, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportMultiSigInfoResponse?.Result == null, nameof(ImportMultiSigInfoAsync));
            return result.ImportMultiSigInfoResponse.Result;
        }

        public async Task<FinalizeMultiSig> FinalizeMultiSigAsync(IEnumerable<string> multiSigInfo, string password, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(FinalizeMultiSigAsync));
            var result = await _moneroRpcWalletCommunicator.FinalizeMultiSigAsync(multiSigInfo, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FinalizeMultiSigResponse?.Result == null, nameof(FinalizeMultiSigAsync));
            return result.FinalizeMultiSigResponse.Result;
        }

        public async Task<SignMultiSigTransactionResult> SignMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SignMultiSigAsync));
            var result = await _moneroRpcWalletCommunicator.SignMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignMultiSigTransactionResponse?.Result == null, nameof(SignMultiSigAsync));
            return result.SignMultiSigTransactionResponse.Result;
        }

        public async Task<SubmitMultiSig> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfWalletNotOpen(_walletCurrentlyOpen, nameof(SubmitMultiSigAsync));
            var result = await _moneroRpcWalletCommunicator.SubmitMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitMultiSigTransactionResponse?.Result == null, nameof(SubmitMultiSigAsync));
            return result.SubmitMultiSigTransactionResponse.Result;
        }
    }
}