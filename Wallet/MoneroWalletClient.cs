using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Network;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    public class MoneroWalletClient : IMoneroWalletClient
    {
        private readonly IMoneroWalletDataRetriever _moneroRpcWalletDataRetriever;

        public MoneroWalletClient(Uri uri)
        {
            _moneroRpcWalletDataRetriever = new MoneroWalletDataRetriever(uri);
        }

        public MoneroWalletClient(Uri uri, HttpMessageHandler httpMessageHandler)
        {
            _moneroRpcWalletDataRetriever = new MoneroWalletDataRetriever(uri, httpMessageHandler);
        }

        public MoneroWalletClient(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _moneroRpcWalletDataRetriever = new MoneroWalletDataRetriever(uri, httpMessageHandler, disposeHandler);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>)
        /// </summary>
        public MoneroWalletClient(MoneroNetwork networkType)
        {
            _moneroRpcWalletDataRetriever = new MoneroWalletDataRetriever(networkType);
        }

        public void Dispose()
        {
            _moneroRpcWalletDataRetriever.Dispose();
        }

        public async Task<Balance> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetBalanceAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            if (result == null || result.BalanceResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BalanceResponse.Result;
        }

        public async Task<Balance> GetBalanceAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetBalanceAsync(accountIndex, token).ConfigureAwait(false);
            if (result == null || result.BalanceResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BalanceResponse.Result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressAsync(accountIndex, token).ConfigureAwait(false);
            if (result == null || result.AddressResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressResponse.Result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            if (result == null || result.AddressResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressResponse.Result;
        }

        public async Task<AddressIndex> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressIndexAsync(address, token).ConfigureAwait(false);
            if (result == null || result.AddressIndexResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressIndexResponse.Result;
        }

        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAddressAsync(accountIndex, token).ConfigureAwait(false);
            if (result == null || result.AddressCreationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressCreationResponse.Result;
        }

        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAddressAsync(accountIndex, label, token).ConfigureAwait(false);
            if (result == null || result.AddressCreationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressCreationResponse.Result;
        }

        public async Task<AddressLabel> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.LabelAddressAsync(majorIndex, minorIndex, label, token).ConfigureAwait(false);
            if (result == null || result.AddressLabelResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressLabelResponse.Result;
        }

        public async Task<AccountResult> GetAccountsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAccountsAsync(token).ConfigureAwait(false);
            if (result == null || result.AccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountResponse.Result;
        }

        public async Task<AccountResult> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAccountsAsync(tag, token).ConfigureAwait(false);
            if (result == null || result.AccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountResponse.Result;
        }

        public async Task<CreateAccount> CreateAccountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAccountAsync(token).ConfigureAwait(false);
            if (result == null || result.CreateAccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CreateAccountResponse.Result;
        }

        public async Task<CreateAccount> CreateAccountAsync(string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAccountAsync(label, token).ConfigureAwait(false);
            if (result == null || result.CreateAccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CreateAccountResponse.Result;
        }

        public async Task<AccountLabel> LabelAccountAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.LabelAccountAsync(accountIndex, label, token).ConfigureAwait(false);
            if (result == null || result.AccountLabelResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountLabelResponse.Result;
        }

        public async Task<AccountTags> GetAccountTagsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAccountTagsAsync(token).ConfigureAwait(false);
            if (result == null || result.AccountTagsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountTagsResponse.Result;
        }

        public async Task<TagAccounts> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TagAccountsAsync(tag, accounts, token).ConfigureAwait(false);
            if (result == null || result.TagAccountsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.TagAccountsResponse.Result;
        }

        public async Task<UntagAccounts> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.UntagAccountsAsync(accounts, token).ConfigureAwait(false);
            if (result == null || result.UntagAccountsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.UntagAccountsResponse.Result;
        }

        public async Task<AccountTagAndDescription> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SetAccountTagDescriptionAsync(tag, description, token).ConfigureAwait(false);
            if (result == null || result.SetAccountTagAndDescriptionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SetAccountTagAndDescriptionResponse.Result;
        }

        public async Task<BlockchainHeight> GetHeightAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetHeightAsync(token).ConfigureAwait(false);
            if (result == null || result.BlockchainHeightResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockchainHeightResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, getTxKey, getTxHex, unlockTime, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, ringSize, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, ringSize, accountIndex, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, newAlgorithm, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, getTxKey, getTxHex, newAlgorithm, unlockTime, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, ringSize, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, ringSize, accountIndex, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SignTransferResult> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SignTransferAsync(unsignedTxSet, exportRaw, token).ConfigureAwait(false);
            if (result == null || result.SignTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SignTransferResponse.Result;
        }

        public async Task<SubmitTransfer> SubmitTransferAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SubmitTransferAsync(txDataHex, token).ConfigureAwait(false);
            if (result == null || result.SubmitTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SubmitTransferResponse.Result;
        }

        public async Task<SweepDust> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SweepDustAsync(getTxKey, getTxHex, getTxMetadata, doNotRelay, token).ConfigureAwait(false);
            if (result == null || result.SweepDustResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SweepDustResponse.Result;
        }

        public async Task<SplitFundTransfer> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SweepAllAsync(address, accountIndex, transactionPriority, ringSize, unlockTime, belowAmount, getTxKeys, getTxHex, getTxMetadata, token).ConfigureAwait(false);
            if (result == null || result.SweepAllResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SweepAllResponse.Result;
        }

        public async Task<SaveWallet> SaveWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SaveWalletAsync(token).ConfigureAwait(false);
            if (result == null || result.SaveWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SaveWalletResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetIncomingTransfersAsync(transferType, returnKeyImage, token).ConfigureAwait(false);
            if (result == null || result.IncomingTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetIncomingTransfersAsync(transferType, accountIndex, returnKeyImage, token).ConfigureAwait(false);
            if (result == null || result.IncomingTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetIncomingTransfersAsync(transferType, accountIndex, subaddrIndices, returnKeyImage, token).ConfigureAwait(false);
            if (result == null || result.IncomingTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<QueryKey> GetPrivateKey(KeyType keyType, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetPrivateKey(keyType, token).ConfigureAwait(false);
            if (result == null || result.QueryKeyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.QueryKeyResponse.Result;
        }

        public async Task<StopWalletResult> StopWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.StopWalletAsync(token).ConfigureAwait(false);
            if (result == null || result.StopWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.StopWalletResponse.Result;
        }

        public async Task<SetTransactionNotes> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SetTransactionNotesAsync(txids, notes, token).ConfigureAwait(false);
            if (result == null || result.SetTransactionNotesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SetTransactionNotesResponse.Result;
        }

        public async Task<GetTransactionNotes> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransactionNotesAsync(txids, token).ConfigureAwait(false);
            if (result == null || result.GetTransactionNotesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetTransactionNotesResponse.Result;
        }

        public async Task<GetTransactionKey> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransactionKeyAsync(txid, token).ConfigureAwait(false);
            if (result == null || result.GetTransactionKeyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetTransactionKeyResponse.Result;
        }

        public async Task<CheckTransactionKey> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CheckTransactionKeyAsync(txid, txKey, address, token).ConfigureAwait(false);
            if (result == null || result.CheckTransactionKeyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CheckTransactionKeyResponse.Result;
        }

        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransfersAsync(@in, @out, pending, failed, pool, token).ConfigureAwait(false);
            if (result == null || result.ShowTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ShowTransfersResponse.Result;
        }

        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint minHeight, uint maxHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransfersAsync(@in, @out, pending, failed, pool, minHeight, maxHeight, token).ConfigureAwait(false);
            if (result == null || result.ShowTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ShowTransfersResponse.Result;
        }

        public async Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransferByTxidAsync(txid, token).ConfigureAwait(false);
            if (result == null || result.TransferByTxidResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.TransferByTxidResponse.Result;
        }

        public async Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransferByTxidAsync(txid, accountIndex, token).ConfigureAwait(false);
            if (result == null || result.TransferByTxidResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.TransferByTxidResponse.Result;
        }

        public async Task<Signature> SignAsync(string data, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SignAsync(data, token).ConfigureAwait(false);
            if (result == null || result.SignResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SignResponse.Result;
        }

        public async Task<VerifyResult> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.VerifyAsync(data, address, signature, token).ConfigureAwait(false);
            if (result == null || result.VerifyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.VerifyResponse.Result;
        }

        public async Task<ExportOutputs> ExportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ExportOutputsAsync(token).ConfigureAwait(false);
            if (result == null || result.ExportOutputsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ExportOutputsResponse.Result;
        }

        public async Task<ImportOutputsResult> ImportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ImportOutputsAsync(token).ConfigureAwait(false);
            if (result == null || result.ImportOutputsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ImportOutputsResponse.Result;
        }

        public async Task<ExportKeyImages> ExportKeyImagesAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ExportKeyImagesAsync(token).ConfigureAwait(false);
            if (result == null || result.ExportKeyImagesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ExportKeyImagesResponse.Result;
        }

        public async Task<ImportKeyImages> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ImportKeyImagesAsync(signedKeyImages, token).ConfigureAwait(false);
            if (result == null || result.ImportKeyImagesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ImportKeyImagesResponse.Result;
        }

        public async Task<MakeUri> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.MakeUriAsync(address, amount, recipientName, txDescription, paymentId, token).ConfigureAwait(false);
            if (result == null || result.MakeUriResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.MakeUriResponse.Result;
        }

        public async Task<ParseUri> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ParseUriAsync(uri, token).ConfigureAwait(false);
            if (result == null || result.ParseUriResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ParseUriResponse.Result;
        }

        public async Task<GetAddressBook> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressBookAsync(entries, token).ConfigureAwait(false);
            if (result == null || result.GetAddressBookResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetAddressBookResponse.Result;
        }

        public async Task<AddAddressBook> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.AddAddressBookAsync(address, description, paymentId, token).ConfigureAwait(false);
            if (result == null || result.AddAddressBookResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddAddressBookResponse.Result;
        }

        public async Task<DeleteAddressBook> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.DeleteAddressBookAsync(index, token).ConfigureAwait(false);
            if (result == null || result.DeleteAddressBookResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.DeleteAddressBookResponse.Result;
        }

        public async Task<RefreshWallet> RefreshWalletAsync(uint startHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.RefreshWalletAsync(startHeight, token).ConfigureAwait(false);
            if (result == null || result.RefreshWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.RefreshWalletResponse.Result;
        }

        public async Task<RescanSpent> RescanSpentAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.RescanSpentAsync(token).ConfigureAwait(false);
            if (result == null || result.RescanSpentResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.RescanSpentResponse.Result;
        }

        public async Task<CreateWallet> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateWalletAsync(filename, language, password, token).ConfigureAwait(false);
            if (result == null || result.CreateWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CreateWalletResponse.Result;
        }

        public async Task<OpenWallet> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.OpenWalletAsync(filename, password, token).ConfigureAwait(false);
            if (result == null || result.OpenWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.OpenWalletResponse.result;
        }

        public async Task<CloseWallet> CloseWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CloseWalletAsync(token).ConfigureAwait(false);
            if (result == null || result.CloseWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CloseWalletResponse.Result;
        }

        public async Task<ChangeWalletPassword> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ChangeWalletPasswordAsync(oldPassword, newPassword, token).ConfigureAwait(false);
            if (result == null || result.ChangeWalletPasswordResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ChangeWalletPasswordResponse.Result;
        }

        public async Task<GetVersion> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetVersionAsync(token).ConfigureAwait(false);
            if (result == null || result.GetRpcVersionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetRpcVersionResponse.Result;
        }

        public async Task<IsMultiSigInformation> IsMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.IsMultiSigAsync(token).ConfigureAwait(false);
            if (result == null || result.IsMultiSigInformationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IsMultiSigInformationResponse.Result;
        }

        public async Task<PrepareMultiSig> PrepareMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.PrepareMultiSigAsync(token).ConfigureAwait(false);
            if (result == null || result.PrepareMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.PrepareMultiSigResponse.Result;
        }

        public async Task<MakeMultiSig> MakeMultiSigAsync(IEnumerable<string> multiSigInfo, uint threshold, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.MakeMultiSigAsync(multiSigInfo, threshold, password, token).ConfigureAwait(false);
            if (result == null || result.MakeMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.MakeMultiSigResponse.Result;
        }

        public async Task<ExportMultiSigInformation> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ExportMultiSigInfoAsync(token).ConfigureAwait(false);
            if (result == null || result.ExportMultiSigInfoResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ExportMultiSigInfoResponse.Result;
        }

        public async Task<ImportMultiSigInformation> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ImportMultiSigInfoAsync(info, token).ConfigureAwait(false);
            if (result == null || result.ImportMultiSigInfoResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ImportMultiSigInfoResponse.Result;
        }

        public async Task<FinalizeMultiSig> FinalizeMultiSigAsync(IEnumerable<string> multiSigInfo, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.FinalizeMultiSigAsync(multiSigInfo, password, token).ConfigureAwait(false);
            if (result == null || result.FinalizeMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FinalizeMultiSigResponse.Result;
        }

        public async Task<SignMultiSigTransactionResult> SignMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SignMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            if (result == null || result.SignMultiSigTransactionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SignMultiSigTransactionResponse.Result;
        }

        public async Task<SubmitMultiSig> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SubmitMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            if (result == null || result.SubmitMultiSigTransactionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SubmitMultiSigTransactionResponse.Result;
        }
    }
}