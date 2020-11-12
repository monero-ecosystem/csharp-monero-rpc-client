using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Network;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Requests;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    internal class MoneroWalletRpcCommunicator : IMoneroWalletRpcCommunicator
    {
        private readonly HttpClient _httpClient;
        private readonly MoneroWalletClientRequestAdapter _requestAdapter;

        private MoneroWalletRpcCommunicator()
        {
            _httpClient = new HttpClient();
        }

        private MoneroWalletRpcCommunicator(HttpMessageHandler httpMessageHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler);
        }

        private MoneroWalletRpcCommunicator(HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler, disposeHandler);
        }

        public MoneroWalletRpcCommunicator(Uri uri, HttpMessageHandler httpMessageHandler) : this(httpMessageHandler)
        {
            _requestAdapter = new MoneroWalletClientRequestAdapter(uri);
        }

        public MoneroWalletRpcCommunicator(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler) : this(httpMessageHandler, disposeHandler)
        {
            _requestAdapter = new MoneroWalletClientRequestAdapter(uri);
        }

        public MoneroWalletRpcCommunicator(Uri uri) : this()
        {
            _requestAdapter = new MoneroWalletClientRequestAdapter(uri);
        }

        public MoneroWalletRpcCommunicator(MoneroNetwork networkType) : this()
        {
            Uri uri;
            switch (networkType)
            {
                case MoneroNetwork.Mainnet:
                    uri = new Uri(MoneroNetworkDefaults.WalletMainnetUri);
                    break;
                case MoneroNetwork.Stagenet:
                    uri = new Uri(MoneroNetworkDefaults.WalletStagenetUri);
                    break;
                case MoneroNetwork.Testnet:
                    uri = new Uri(MoneroNetworkDefaults.WalletTestnetUri);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown MoneroNetwork ({networkType})");
            }
            _requestAdapter = new MoneroWalletClientRequestAdapter(uri);
        }

        public async Task<MoneroWalletCommunicatorResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                account_index = account_index,
                address_indices = address_indices,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Balance, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BalanceResponse responseObject = await JsonSerializer.DeserializeAsync<BalanceResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Balance,
                BalanceResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetBalanceAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Balance, new WalletRequestParameters() { account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BalanceResponse responseObject = await JsonSerializer.DeserializeAsync<BalanceResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Balance,
                BalanceResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAddressAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default)
        {
            var walletParameters = new WalletRequestParameters()
            {
                account_index = account_index,
                address_indices = address_indices,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Address, walletParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddressResponse responseObject = await JsonSerializer.DeserializeAsync<AddressResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Address,
                AddressResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAddressAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Address, new WalletRequestParameters() { account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddressResponse responseObject = await JsonSerializer.DeserializeAsync<AddressResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Address,
                AddressResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidOperationException("Address cannot be null of whitespace");
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AddressIndex, new WalletRequestParameters() { address = address, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddressIndexResponse responseObject = await JsonSerializer.DeserializeAsync<AddressIndexResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AddressIndex,
                AddressIndexResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CreateAddressAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AddressCreation, new WalletRequestParameters() { account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddressCreationResponse responseObject = await JsonSerializer.DeserializeAsync<AddressCreationResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Address,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AddressCreation,
                AddressCreationResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CreateAddressAsync(uint account_index, string label, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new InvalidOperationException("Label cannot be null of whitespace");
            var walletRequestParameters = new WalletRequestParameters()
            {
                account_index = account_index,
                label = label,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AddressCreation, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddressCreationResponse responseObject = await JsonSerializer.DeserializeAsync<AddressCreationResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Address,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AddressCreation,
                AddressCreationResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> LabelAddressAsync(uint major_index, uint minor_index, string label, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new InvalidOperationException("Label cannot be null of whitespace");
            var walletRequestParameters = new WalletRequestParameters()
            {
                index = new AddressIndexParameter()
                {
                    major = major_index,
                    minor = minor_index,
                },
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AddressLabeling, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddressLabelResponse responseObject = await JsonSerializer.DeserializeAsync<AddressLabelResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Address,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AddressCreation,
                AddressLabelResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new InvalidOperationException("Tag used to filter accounts on cannot be null of whitespace");
            var walletRequestParameters = new WalletRequestParameters()
            {
                label = tag,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Account, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AccountResponse responseObject = await JsonSerializer.DeserializeAsync<AccountResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Account,
                AccountResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAccountsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Account, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AccountResponse responseObject = await JsonSerializer.DeserializeAsync<AccountResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Account,
                AccountResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CreateAccountAsync(string label, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new InvalidOperationException("Label cannot be null of whitespace");
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountCreation, new WalletRequestParameters() { label = label, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            CreateAccountResponse responseObject = await JsonSerializer.DeserializeAsync<CreateAccountResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountCreation,
                CreateAccountResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CreateAccountAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountCreation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            CreateAccountResponse responseObject = await JsonSerializer.DeserializeAsync<CreateAccountResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountCreation,
                CreateAccountResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> LabelAccountAsync(uint account_index, string label, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new InvalidOperationException("Label cannot be null of whitespace");
            var walletRequestParameters = new WalletRequestParameters()
            {
                label = label,
                account_index = account_index,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountLabeling, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AccountLabelResponse responseObject = await JsonSerializer.DeserializeAsync<AccountLabelResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountLabeling,
                AccountLabelResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAccountTagsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountTags, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AccountTagsResponse responseObject = await JsonSerializer.DeserializeAsync<AccountTagsResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountTags,
                AccountTagsResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new InvalidOperationException("Tag cannot be null of whitespace");
            if (accounts == null || !accounts.Any())
                throw new InvalidOperationException("Accounts is either null or empty");
            var walletRequestParameters = new WalletRequestParameters()
            {
                tag = tag,
                accounts = accounts,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountTagging, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            TagAccountsResponse responseObject = await JsonSerializer.DeserializeAsync<TagAccountsResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountTagging,
                TagAccountsResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            if (accounts == null || !accounts.Any())
                throw new InvalidOperationException("Accounts is either null or empty");
            var walletRequestParameters = new WalletRequestParameters()
            {
                accounts = accounts,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountUntagging, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            UntagAccountsResponse responseObject = await JsonSerializer.DeserializeAsync<UntagAccountsResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountUntagging,
                UntagAccountsResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new InvalidOperationException("Tag cannot be null of whitespace");
            if (string.IsNullOrWhiteSpace(description))
                throw new InvalidOperationException("Description cannot be null of whitespace");
            var walletRequestParameters = new WalletRequestParameters()
            {
                tag = tag,
                description = description,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AccountTagAndDescriptionSetting, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AccountTagAndDescriptionResponse responseObject = await JsonSerializer.DeserializeAsync<AccountTagAndDescriptionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountTagAndDescriptionSetting,
                SetAccountTagAndDescriptionResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetHeightAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Height, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            BlockchainHeightResponse responseObject = await JsonSerializer.DeserializeAsync<BlockchainHeightResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Account,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AccountTagAndDescriptionSetting,
                BlockchainHeightResponse = responseObject,
            };
        }

        private static List<FundTransferParameter> TransactionToFundTransferParameter(IEnumerable<(string address, ulong amount)> transactions)
        {
            List<FundTransferParameter> fundTransferParameters = new List<FundTransferParameter>();
            foreach (var da in transactions)
                fundTransferParameters.Add(new FundTransferParameter()
                {
                    address = da.address,
                    amount = da.amount,
                });
            return fundTransferParameters;
        }

        private async Task<MoneroWalletCommunicatorResponse> TransferFundsAsync(WalletRequestParameters walletRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.FundTransfer, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            FundTransferResponse responseObject = await JsonSerializer.DeserializeAsync<FundTransferResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.FundTransfer,
                FundTransferResponse = responseObject,
            };
        }

        private async Task<MoneroWalletCommunicatorResponse> TransferSplitFundsAsync(WalletRequestParameters walletRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.FundTransferSplit, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SplitFundTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SplitFundTransferResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.FundTransferSplit,
                FundTransferSplitResponse = responseObject,
            };
        }

        public Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
            };
            return TransferFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, uint unlock_time = 0, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                unlock_time = unlock_time,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
            };
            return TransferFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
                throw new InvalidOperationException($"ring_size must be at least 2");
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                unlock_time = unlock_time,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                ring_size = ring_size,
                mixin = ring_size - 1,
            };
            return TransferFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
                throw new InvalidOperationException($"ring_size must be at least 2");
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                unlock_time = unlock_time,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                ring_size = ring_size,
                mixin = ring_size - 1,
                account_index = account_index,
            };
            return TransferFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool new_algorithm = true, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
            };
            return TransferSplitFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, bool new_algorithm = true, uint unlock_time = 0, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                new_algorithm = new_algorithm,
                unlock_time = unlock_time,
            };
            return TransferSplitFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                ring_size = ring_size,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                new_algorithm = new_algorithm,
                unlock_time = unlock_time,
            };
            return TransferSplitFundsAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
                throw new InvalidOperationException($"ring_size must be at least 2");
            var walletRequestParameters = new WalletRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                ring_size = ring_size,
                mixin = ring_size - 1,
                account_index = account_index,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                new_algorithm = new_algorithm,
                unlock_time = unlock_time,
            };
            return TransferSplitFundsAsync(walletRequestParameters, token);
        }

        public async Task<MoneroWalletCommunicatorResponse> SignTransferAsync(string unsigned_txset, bool export_raw = false, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                unsigned_txset = unsigned_txset,
                export_raw = export_raw,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SignTransfer, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SignTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SignTransferResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SignTransfer,
                SignTransferResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SubmitTransferAsync(string tx_data_hex, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                tx_data_hex = tx_data_hex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SubmitTransfer, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SubmitTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitTransferResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SubmitTransfer,
                SubmitTransferResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SweepDustAsync(bool get_tx_keys, bool get_tx_hex, bool get_tx_metadata, bool do_not_relay = false, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                get_tx_keys = get_tx_keys,
                get_tx_hex = get_tx_hex,
                get_tx_metadata = get_tx_metadata,
                do_not_relay = do_not_relay,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SweepDust, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SweepDustResponse responseObject = await JsonSerializer.DeserializeAsync<SweepDustResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SweepDust,
                SweepDustResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SweepAllAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, ulong below_amount = ulong.MaxValue, bool get_tx_keys = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                address = address,
                account_index = account_index,
                priority = (uint)transaction_priority,
                ring_size = ring_size,
                mixin = ring_size - 1,
                unlock_time = unlock_time,
                get_tx_keys = get_tx_keys,
                get_tx_hex = get_tx_hex,
                get_tx_metadata = get_tx_metadata,
                below_amount = below_amount,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SweepAll, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SweepAllResponse responseObject = await JsonSerializer.DeserializeAsync<SweepAllResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SweepAll,
                SweepAllResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SaveWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SaveWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SaveWalletResponse responseObject = await JsonSerializer.DeserializeAsync<SaveWalletResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SaveWallet,
                SaveWalletResponse = responseObject,
            };
        }

        public Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, bool return_key_image = false, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                transfer_type = transfer_type.ToString().ToLowerInvariant(),
                verbose = return_key_image,
            };
            return GetIncomingTransfersAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, bool return_key_image = false, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                transfer_type = transfer_type.ToString().ToLowerInvariant(),
                verbose = return_key_image,
                account_index = account_index,
            };
            return GetIncomingTransfersAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, IEnumerable<uint> subaddr_indices, bool return_key_image = false, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                transfer_type = transfer_type.ToString().ToLowerInvariant(),
                verbose = return_key_image,
                account_index = account_index,
                subaddr_indices = subaddr_indices,
            };
            return GetIncomingTransfersAsync(walletRequestParameters, token);
        }

        private async Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(WalletRequestParameters walletRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.IncomingTransfers, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            IncomingTransfersResponse responseObject = await JsonSerializer.DeserializeAsync<IncomingTransfersResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.IncomingTransfers,
                IncomingTransfersResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetPrivateKey(KeyType key_type, CancellationToken token = default)
        {
            static string KeyTypeToString(KeyType keyType)
            {
                return keyType switch
                {
                    KeyType.Mnemonic => "mnemonic",
                    KeyType.ViewKey => "view_key",
                    _ => throw new InvalidOperationException($"Unknown KeyType ({keyType})"),
                };
            }

            var walletRequestParameters = new WalletRequestParameters()
            {
                key_type = KeyTypeToString(key_type),
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.QueryPrivateKey, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            QueryKeyResponse responseObject = await JsonSerializer.DeserializeAsync<QueryKeyResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.QueryPrivateKey,
                QueryKeyResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> StopWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.StopWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            StopWalletResponse responseObject = await JsonSerializer.DeserializeAsync<StopWalletResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.StopWallet,
                StopWalletResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            if (txids == null || !txids.Any())
                throw new InvalidOperationException("txids is either null or empty");
            if (notes == null || !notes.Any())
                throw new InvalidOperationException("notes is either null or empty");
            var walletRequestParameters = new WalletRequestParameters()
            {
                txids = txids,
                notes = notes,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SetTransactionNotes, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SetTransactionNotesResponse responseObject = await JsonSerializer.DeserializeAsync<SetTransactionNotesResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SetTransactionNotes,
                SetTransactionNotesResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            if (txids == null || !txids.Any())
                throw new InvalidOperationException("txids is either null or empty");
            var walletRequestParameters = new WalletRequestParameters()
            {
                txids = txids,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.GetTransactionNotes, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            GetTransactionNotesResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransactionNotesResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.GetTransactionNotes,
                GetTransactionNotesResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(txid))
                throw new InvalidOperationException("txid cannot be null or whitespace");
            var walletRequestParameters = new WalletRequestParameters()
            {
                txid = txid,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.GetTransactionKey, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            GetTransactionKeyResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransactionKeyResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.GetTransactionKey,
                GetTransactionKeyResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CheckTransactionKeyAsync(string txid, string tx_key, string address, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(txid))
                throw new InvalidOperationException("txid cannot be null or whitespace");
            if (string.IsNullOrWhiteSpace(tx_key))
                throw new InvalidOperationException("tx_key cannot be null or whitespace");
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidOperationException("address cannot be null or whitespace");
            var walletRequestParameters = new
            {
                txid = txid,
                tx_key = tx_key,
                address = address,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.CheckTransactionKey, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            CheckTransactionKeyResponse responseObject = await JsonSerializer.DeserializeAsync<CheckTransactionKeyResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.CheckTransactionKey,
                CheckTransactionKeyResponse = responseObject,
            };
        }

        public Task<MoneroWalletCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            bool isValidRequest = false;
            isValidRequest |= @in |= @out |= pending |= failed |= pool;
            if (!isValidRequest)
                throw new InvalidOperationException("Not requesting to view any form of transfer");
            var walletRequestParameters = new WalletRequestParameters()
            {
                @in = @in,
                @out = @out,
                pending = pending,
                failed = failed,
                pool = pool,
            };
            return GetTransfersAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint min_height, uint max_height, CancellationToken token = default)
        {
            bool isValidRequest = false;
            isValidRequest |= @in |= @out |= pending |= failed |= pool;
            if (!isValidRequest)
                throw new InvalidOperationException("Not requesting to view any form of transfer");
            if (max_height < min_height)
                throw new InvalidOperationException($"max_height ({max_height}) cannot be less than min_height({min_height})");
            var walletRequestParameters = new WalletRequestParameters()
            {
                @in = @in,
                @out = @out,
                pending = pending,
                failed = failed,
                pool = pool,
                min_height = min_height,
                max_height = max_height,
                filter_by_height = true,
            };
            return GetTransfersAsync(walletRequestParameters, token);
        }

        private async Task<MoneroWalletCommunicatorResponse> GetTransfersAsync(WalletRequestParameters walletRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Transfers, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ShowTransfersResponse responseObject = await JsonSerializer.DeserializeAsync<ShowTransfersResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Transfers,
                ShowTransfersResponse = responseObject,
            };
        }

        public Task<MoneroWalletCommunicatorResponse> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                txid = txid,
            };
            return GetTransferByTxidAsync(walletRequestParameters, token);
        }

        public Task<MoneroWalletCommunicatorResponse> GetTransferByTxidAsync(string txid, uint account_index, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                txid = txid,
                account_index = account_index,
            };
            return GetTransferByTxidAsync(walletRequestParameters, token);
        }

        private async Task<MoneroWalletCommunicatorResponse> GetTransferByTxidAsync(WalletRequestParameters walletRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.TransferByTxid, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            GetTransferByTxidResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransferByTxidResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.TransferByTxid,
                TransferByTxidResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SignAsync(string data, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                data = data,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Sign, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SignResponse responseObject = await JsonSerializer.DeserializeAsync<SignResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Miscellaneous,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Sign,
                SignResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                data = data,
                address = address,
                signature = signature,

            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Verify, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            VerifyResponse responseObject = await JsonSerializer.DeserializeAsync<VerifyResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Miscellaneous,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Verify,
                VerifyResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ExportOutputsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ExportOutputs, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ExportOutputsResponse responseObject = await JsonSerializer.DeserializeAsync<ExportOutputsResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ExportOutputs,
                ExportOutputsResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ImportOutputsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ImportOutputs, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ImportOutputsResponse responseObject = await JsonSerializer.DeserializeAsync<ImportOutputsResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ImportOutputs,
                ImportOutputsResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ExportKeyImagesAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ExportKeyImages, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ExportKeyImagesResponse responseObject = await JsonSerializer.DeserializeAsync<ExportKeyImagesResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ExportKeyImages,
                ExportKeyImagesResponse = responseObject,
            };
        }

        private static List<SignedKeyImage> KeyImageAndSignatureToSignedKeyImages(IEnumerable<(string key_image, string signature)> signed_key_images)
        {
            var signedKeyImages = new List<SignedKeyImage>();
            foreach (var keyImagePair in signed_key_images)
            {
                signedKeyImages.Add(new SignedKeyImage()
                {
                    Image = keyImagePair.key_image,
                    Signature = keyImagePair.signature,
                });
            }
            return signedKeyImages;
        }

        public async Task<MoneroWalletCommunicatorResponse> ImportKeyImagesAsync(IEnumerable<(string key_image, string signature)> signed_key_images, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                signed_key_images = KeyImageAndSignatureToSignedKeyImages(signed_key_images),
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ImportKeyImages, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ImportKeyImagesResponse responseObject = await JsonSerializer.DeserializeAsync<ImportKeyImagesResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ImportKeyImages,
                ImportKeyImagesResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> MakeUriAsync(string address, ulong amount, string recipient_name, string tx_description = null, string payment_id = null, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                address = address,
                amount = amount,
                recipient_name = recipient_name,
                tx_description = tx_description,
                payment_id = payment_id,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.MakeUri, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            MakeUriResponse responseObject = await JsonSerializer.DeserializeAsync<MakeUriResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.MakeUri,
                MakeUriResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                uri = uri,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ParseUri, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ParseUriResponse responseObject = await JsonSerializer.DeserializeAsync<ParseUriResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Transaction,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ParseUri,
                ParseUriResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                entries = entries,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.GetAddressBook, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            GetAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<GetAddressBookResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.GetAddressBook,
                GetAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> AddAddressBookAsync(string address, string description = null, string payment_id = null, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                address = address,
                description = description,
                payment_id = payment_id,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.AddAddressBook, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            AddAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<AddAddressBookResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.AddAddressBook,
                AddAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var walletRequestParameters = new
            {
                index = index,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.DeleteAddressBook, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            DeleteAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<DeleteAddressBookResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.DeleteAddressBook,
                DeleteAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> RefreshWalletAsync(uint start_height, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                start_height = start_height,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.Refresh, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            RefreshWalletResponse responseObject = await JsonSerializer.DeserializeAsync<RefreshWalletResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.Refresh,
                RefreshWalletResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> RescanSpentAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.RescanSpent, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            RescanSpentResponse responseObject = await JsonSerializer.DeserializeAsync<RescanSpentResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.RescanSpent,
                RescanSpentResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                filename = filename,
                language = language,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.CreateWallet, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            CreateWalletResponse responseObject = await JsonSerializer.DeserializeAsync<CreateWalletResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.CreateWallet,
                CreateWalletResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                filename = filename,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.OpenWallet, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            OpenWalletResponse responseObject = await JsonSerializer.DeserializeAsync<OpenWalletResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.OpenWallet,
                OpenWalletResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> CloseWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.CloseWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            CloseWalletResponse responseObject = await JsonSerializer.DeserializeAsync<CloseWalletResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.CloseWallet,
                CloseWalletResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                old_password = oldPassword,
                new_password = newPassword,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ChangeWalletPassword, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ChangeWalletPasswordResponse responseObject = await JsonSerializer.DeserializeAsync<ChangeWalletPasswordResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ChangeWalletPassword,
                ChangeWalletPasswordResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> GetVersionAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.RpcVersion, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            GetRpcVersionResponse responseObject = await JsonSerializer.DeserializeAsync<GetRpcVersionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Miscellaneous,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.RpcVersion,
                GetRpcVersionResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> IsMultiSigAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.IsMultiSig, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            IsMultiSigInformationResponse responseObject = await JsonSerializer.DeserializeAsync<IsMultiSigInformationResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.IsMultiSig,
                IsMultiSigInformationResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> PrepareMultiSigAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.PrepareMultiSig, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            PrepareMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<PrepareMultiSigResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.PrepareMultiSig,
                PrepareMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> MakeMultiSigAsync(IEnumerable<string> multisig_info, uint threshold, string password, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                multisig_info = multisig_info,
                threshold = threshold,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.MakeMultiSig, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            MakeMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<MakeMultiSigResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.MakeMultiSig,
                MakeMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ExportMultiSigInfo, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ExportMultiSigInfoResponse responseObject = await JsonSerializer.DeserializeAsync<ExportMultiSigInfoResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ExportMultiSigInfo,
                ExportMultiSigInfoResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.ImportMultiSigInfo, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            ImportMultiSigInfoResponse responseObject = await JsonSerializer.DeserializeAsync<ImportMultiSigInfoResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.ImportMultiSigInfo,
                ImportMultiSigInfoResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> FinalizeMultiSigAsync(IEnumerable<string> multisigInfo, string password, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                multisig_info = multisigInfo,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.FinalizeMultiSig, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            FinalizeMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<FinalizeMultiSigResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.FinalizeMultiSig,
                FinalizeMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SignMultiSigAsync(string tx_data_hex, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                tx_data_hex = tx_data_hex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SignMultiSigTransaction, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SignMultiSigTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SignMultiSigTransactionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SignMultiSigTransaction,
                SignMultiSigTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroWalletCommunicatorResponse> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var walletRequestParameters = new WalletRequestParameters()
            {
                tx_data_hex = txDataHex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroWalletResponseSubType.SubmitMultiSigTransaction, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SubmitMultiSigTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitMultiSigTransactionResponse>(ms, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            return new MoneroWalletCommunicatorResponse()
            {
                MoneroWalletResponseType = MoneroWalletResponseType.Wallet,
                MoneroWalletResponseSubType = MoneroWalletResponseSubType.SubmitMultiSigTransaction,
                SubmitMultiSigTransactionResponse = responseObject,
            };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}