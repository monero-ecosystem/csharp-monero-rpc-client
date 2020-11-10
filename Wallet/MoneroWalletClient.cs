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

        public async Task<BalanceResult> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetBalanceAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            if (result == null || result.BalanceResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BalanceResponse.result;
        }

        public async Task<BalanceResult> GetBalanceAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetBalanceAsync(accountIndex, token).ConfigureAwait(false);
            if (result == null || result.BalanceResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BalanceResponse.result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressAsync(accountIndex, token).ConfigureAwait(false);
            if (result == null || result.AddressResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressResponse.result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            if (result == null || result.AddressResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressResponse.result;
        }

        public async Task<AddressIndexResult> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressIndexAsync(address, token).ConfigureAwait(false);
            if (result == null || result.AddressIndexResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressIndexResponse.result;
        }

        public async Task<AddressCreationResult> CreateAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAddressAsync(accountIndex, token).ConfigureAwait(false);
            if (result == null || result.AddressCreationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressCreationResponse.result;
        }

        public async Task<AddressCreationResult> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAddressAsync(accountIndex, label, token).ConfigureAwait(false);
            if (result == null || result.AddressCreationResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressCreationResponse.result;
        }

        public async Task<AddressLabelResult> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.LabelAddressAsync(majorIndex, minorIndex, label, token).ConfigureAwait(false);
            if (result == null || result.AddressLabelResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddressLabelResponse.result;
        }

        public async Task<AccountResult> GetAccountsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAccountsAsync(token).ConfigureAwait(false);
            if (result == null || result.AccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountResponse.result;
        }

        public async Task<AccountResult> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAccountsAsync(tag, token).ConfigureAwait(false);
            if (result == null || result.AccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountResponse.result;
        }

        public async Task<CreateAccountResult> CreateAccountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAccountAsync(token).ConfigureAwait(false);
            if (result == null || result.CreateAccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CreateAccountResponse.result;
        }

        public async Task<CreateAccountResult> CreateAccountAsync(string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateAccountAsync(label, token).ConfigureAwait(false);
            if (result == null || result.CreateAccountResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CreateAccountResponse.result;
        }

        public async Task<AccountLabelResult> LabelAccountAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.LabelAccountAsync(accountIndex, label, token).ConfigureAwait(false);
            if (result == null || result.AccountLabelResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountLabelResponse.result;
        }

        public async Task<AccountTagsResult> GetAccountTagsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAccountTagsAsync(token).ConfigureAwait(false);
            if (result == null || result.AccountTagsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AccountTagsResponse.result;
        }

        public async Task<TagAccountsResult> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TagAccountsAsync(tag, accounts, token).ConfigureAwait(false);
            if (result == null || result.TagAccountsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.TagAccountsResponse.result;
        }

        public async Task<UntagAccountsResult> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.UntagAccountsAsync(accounts, token).ConfigureAwait(false);
            if (result == null || result.UntagAccountsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.UntagAccountsResponse.result;
        }

        public async Task<AccountTagAndDescriptionResult> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SetAccountTagDescriptionAsync(tag, description, token).ConfigureAwait(false);
            if (result == null || result.SetAccountTagAndDescriptionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SetAccountTagAndDescriptionResponse.result;
        }

        public async Task<BlockchainHeightResult> GetHeightAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetHeightAsync(token).ConfigureAwait(false);
            if (result == null || result.BlockchainHeightResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.BlockchainHeightResponse.result;
        }

        public async Task<FundTransferResult> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.result;
        }

        public async Task<FundTransferResult> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, getTxKey, getTxHex, unlockTime, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.result;
        }

        public async Task<FundTransferResult> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, ringSize, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.result;
        }

        public async Task<FundTransferResult> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferAsync(transactions, transferPriority, ringSize, accountIndex, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferResponse.result;
        }

        public async Task<FundTransferSplitResult> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, newAlgorithm, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.result;
        }

        public async Task<FundTransferSplitResult> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, getTxKey, getTxHex, newAlgorithm, unlockTime, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.result;
        }

        public async Task<FundTransferSplitResult> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, ringSize, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.result;
        }

        public async Task<FundTransferSplitResult> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.TransferSplitAsync(transactions, transferPriority, ringSize, accountIndex, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            if (result == null || result.FundTransferSplitResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FundTransferSplitResponse.result;
        }

        public async Task<SignTransferResult> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SignTransferAsync(unsignedTxSet, exportRaw, token).ConfigureAwait(false);
            if (result == null || result.SignTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SignTransferResponse.result;
        }

        public async Task<SubmitTransferResult> SubmitTransferAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SubmitTransferAsync(txDataHex, token).ConfigureAwait(false);
            if (result == null || result.SubmitTransferResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SubmitTransferResponse.result;
        }

        public async Task<SweepDustResult> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SweepDustAsync(getTxKey, getTxHex, getTxMetadata, doNotRelay, token).ConfigureAwait(false);
            if (result == null || result.SweepDustResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SweepDustResponse.result;
        }

        public async Task<FundTransferSplitResult> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SweepAllAsync(address, accountIndex, transactionPriority, ringSize, unlockTime, belowAmount, getTxKeys, getTxHex, getTxMetadata, token).ConfigureAwait(false);
            if (result == null || result.SweepAllResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SweepAllResponse.result;
        }

        public async Task<SaveWalletResult> SaveWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SaveWalletAsync(token).ConfigureAwait(false);
            if (result == null || result.SaveWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SaveWalletResponse.result;
        }

        public async Task<IncomingTransferResult> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetIncomingTransfersAsync(transferType, returnKeyImage, token).ConfigureAwait(false);
            if (result == null || result.IncomingTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IncomingTransfersResponse.result;
        }

        public async Task<IncomingTransferResult> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetIncomingTransfersAsync(transferType, accountIndex, returnKeyImage, token).ConfigureAwait(false);
            if (result == null || result.IncomingTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IncomingTransfersResponse.result;
        }

        public async Task<IncomingTransferResult> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetIncomingTransfersAsync(transferType, accountIndex, subaddrIndices, returnKeyImage, token).ConfigureAwait(false);
            if (result == null || result.IncomingTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IncomingTransfersResponse.result;
        }

        public async Task<QueryKeyResult> GetPrivateKey(KeyType keyType, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetPrivateKey(keyType, token).ConfigureAwait(false);
            if (result == null || result.QueryKeyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.QueryKeyResponse.result;
        }

        public async Task<StopWalletResult> StopWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.StopWalletAsync(token).ConfigureAwait(false);
            if (result == null || result.StopWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.StopWalletResponse.result;
        }

        public async Task<SetTransactionNotesResult> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SetTransactionNotesAsync(txids, notes, token).ConfigureAwait(false);
            if (result == null || result.SetTransactionNotesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SetTransactionNotesResponse.result;
        }

        public async Task<GetTransactionNotesResult> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransactionNotesAsync(txids, token).ConfigureAwait(false);
            if (result == null || result.GetTransactionNotesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetTransactionNotesResponse.result;
        }

        public async Task<GetTransactionKeyResult> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransactionKeyAsync(txid, token).ConfigureAwait(false);
            if (result == null || result.GetTransactionKeyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetTransactionKeyResponse.result;
        }

        public async Task<CheckTransactionKeyResult> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CheckTransactionKeyAsync(txid, txKey, address, token).ConfigureAwait(false);
            if (result == null || result.CheckTransactionKeyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CheckTransactionKeyResponse.result;
        }

        public async Task<ShowTransfersResult> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransfersAsync(@in, @out, pending, failed, pool, token).ConfigureAwait(false);
            if (result == null || result.ShowTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ShowTransfersResponse.result;
        }

        public async Task<ShowTransfersResult> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint minHeight, uint maxHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransfersAsync(@in, @out, pending, failed, pool, minHeight, maxHeight, token).ConfigureAwait(false);
            if (result == null || result.ShowTransfersResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ShowTransfersResponse.result;
        }

        public async Task<ShowTransferByTxidResult> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransferByTxidAsync(txid, token).ConfigureAwait(false);
            if (result == null || result.TransferByTxidResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.TransferByTxidResponse.result;
        }

        public async Task<ShowTransferByTxidResult> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetTransferByTxidAsync(txid, accountIndex, token).ConfigureAwait(false);
            if (result == null || result.TransferByTxidResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.TransferByTxidResponse.result;
        }

        public async Task<SignResult> SignAsync(string data, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SignAsync(data, token).ConfigureAwait(false);
            if (result == null || result.SignResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SignResponse.result;
        }

        public async Task<VerifyResult> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.VerifyAsync(data, address, signature, token).ConfigureAwait(false);
            if (result == null || result.VerifyResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.VerifyResponse.result;
        }

        public async Task<ExportOutputsResult> ExportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ExportOutputsAsync(token).ConfigureAwait(false);
            if (result == null || result.ExportOutputsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ExportOutputsResponse.result;
        }

        public async Task<ImportOutputsResult> ImportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ImportOutputsAsync(token).ConfigureAwait(false);
            if (result == null || result.ImportOutputsResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ImportOutputsResponse.result;
        }

        public async Task<ExportKeyImagesResult> ExportKeyImagesAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ExportKeyImagesAsync(token).ConfigureAwait(false);
            if (result == null || result.ExportKeyImagesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ExportKeyImagesResponse.result;
        }

        public async Task<ImportKeyImagesResult> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ImportKeyImagesAsync(signedKeyImages, token).ConfigureAwait(false);
            if (result == null || result.ImportKeyImagesResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ImportKeyImagesResponse.result;
        }

        public async Task<MakeUriResult> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.MakeUriAsync(address, amount, recipientName, txDescription, paymentId, token).ConfigureAwait(false);
            if (result == null || result.MakeUriResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.MakeUriResponse.result;
        }

        public async Task<ParseUriResult> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ParseUriAsync(uri, token).ConfigureAwait(false);
            if (result == null || result.ParseUriResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ParseUriResponse.result;
        }

        public async Task<GetAddressBookResult> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetAddressBookAsync(entries, token).ConfigureAwait(false);
            if (result == null || result.GetAddressBookResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetAddressBookResponse.result;
        }

        public async Task<AddAddressBookResult> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.AddAddressBookAsync(address, description, paymentId, token).ConfigureAwait(false);
            if (result == null || result.AddAddressBookResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.AddAddressBookResponse.result;
        }

        public async Task<DeleteAddressBookResult> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.DeleteAddressBookAsync(index, token).ConfigureAwait(false);
            if (result == null || result.DeleteAddressBookResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.DeleteAddressBookResponse.result;
        }

        public async Task<RefreshWalletResult> RefreshWalletAsync(uint startHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.RefreshWalletAsync(startHeight, token).ConfigureAwait(false);
            if (result == null || result.RefreshWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.RefreshWalletResponse.result;
        }

        public async Task<RescanSpentResult> RescanSpentAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.RescanSpentAsync(token).ConfigureAwait(false);
            if (result == null || result.RescanSpentResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.RescanSpentResponse.result;
        }

        public async Task<CreateWalletResult> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CreateWalletAsync(filename, language, password, token).ConfigureAwait(false);
            if (result == null || result.CreateWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CreateWalletResponse.result;
        }

        public async Task<OpenWalletResult> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.OpenWalletAsync(filename, password, token).ConfigureAwait(false);
            if (result == null || result.OpenWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.OpenWalletResponse.result;
        }

        public async Task<CloseWalletResult> CloseWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.CloseWalletAsync(token).ConfigureAwait(false);
            if (result == null || result.CloseWalletResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.CloseWalletResponse.result;
        }

        public async Task<ChangeWalletPasswordResult> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ChangeWalletPasswordAsync(oldPassword, newPassword, token).ConfigureAwait(false);
            if (result == null || result.ChangeWalletPasswordResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ChangeWalletPasswordResponse.result;
        }

        public async Task<GetVersionResult> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.GetVersionAsync(token).ConfigureAwait(false);
            if (result == null || result.GetRpcVersionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.GetRpcVersionResponse.result;
        }

        public async Task<IsMultiSigResult> IsMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.IsMultiSigAsync(token).ConfigureAwait(false);
            if (result == null || result.IsMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.IsMultiSigResponse.result;
        }

        public async Task<PrepareMultiSigResult> PrepareMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.PrepareMultiSigAsync(token).ConfigureAwait(false);
            if (result == null || result.PrepareMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.PrepareMultiSigResponse.result;
        }

        public async Task<MakeMultiSigResult> MakeMultiSigAsync(IEnumerable<string> multiSigInfo, uint threshold, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.MakeMultiSigAsync(multiSigInfo, threshold, password, token).ConfigureAwait(false);
            if (result == null || result.MakeMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.MakeMultiSigResponse.result;
        }

        public async Task<ExportMultiSigInfoResult> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ExportMultiSigInfoAsync(token).ConfigureAwait(false);
            if (result == null || result.ExportMultiSigInfoResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ExportMultiSigInfoResponse.result;
        }

        public async Task<ImportMultiSigInfoResult> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.ImportMultiSigInfoAsync(info, token).ConfigureAwait(false);
            if (result == null || result.ImportMultiSigInfoResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.ImportMultiSigInfoResponse.result;
        }

        public async Task<FinalizeMultiSigResult> FinalizeMultiSigAsync(IEnumerable<string> multiSigInfo, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.FinalizeMultiSigAsync(multiSigInfo, password, token).ConfigureAwait(false);
            if (result == null || result.FinalizeMultiSigResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.FinalizeMultiSigResponse.result;
        }

        public async Task<SignMultiSigTransactionResult> SignMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SignMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            if (result == null || result.SignMultiSigTransactionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SignMultiSigTransactionResponse.result;
        }

        public async Task<SubmitMultiSigResult> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcWalletDataRetriever.SubmitMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            if (result == null || result.SubmitMultiSigTransactionResponse == null)
                throw new RpcResponseException("Error experienced when making RPC call");
            return result.SubmitMultiSigTransactionResponse.result;
        }
    }
}