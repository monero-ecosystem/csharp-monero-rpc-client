using Monero.Client.Daemon.POD.Requests;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Network;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Requests;
using Monero.Client.Wallet.POD.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Monero.Client.Utilities
{
    internal enum ConnectionType
    {
        Wallet,
        Daemon,
    }

    internal class RpcCommunicator
    {
        private readonly HttpClient _httpClient;
        private readonly MoneroRequestAdapter _requestAdapter;
        private readonly JsonSerializerOptions _defaultSerializationOptions = new JsonSerializerOptions() { IgnoreNullValues = true, };

        private RpcCommunicator()
        {
            _httpClient = new HttpClient();
        }

        private RpcCommunicator(HttpMessageHandler httpMessageHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler);
        }

        private RpcCommunicator(HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler, disposeHandler);
        }

        public RpcCommunicator(Uri uri) : this()
        {
            _requestAdapter = new MoneroRequestAdapter(uri);
        }

        public RpcCommunicator(Uri uri, HttpMessageHandler httpMessageHandler) : this(httpMessageHandler)
        {
            _requestAdapter = new MoneroRequestAdapter(uri);
        }

        public RpcCommunicator(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler) : this(httpMessageHandler, disposeHandler)
        {
            _requestAdapter = new MoneroRequestAdapter(uri);
        }

        public RpcCommunicator(MoneroNetwork networkType, ConnectionType connectionType) : this()
        {
            Uri uri = (connectionType, networkType) switch 
            {
                (ConnectionType.Daemon, MoneroNetwork.Mainnet) => new Uri(MoneroNetworkDefaults.DaemonMainnetUri),
                (ConnectionType.Daemon, MoneroNetwork.Stagenet) => new Uri(MoneroNetworkDefaults.DaemonStagenetUri),
                (ConnectionType.Daemon, MoneroNetwork.Testnet) => new Uri(MoneroNetworkDefaults.DaemonTestnetUri),
                (ConnectionType.Wallet, MoneroNetwork.Mainnet) => new Uri(MoneroNetworkDefaults.WalletMainnetUri),
                (ConnectionType.Wallet, MoneroNetwork.Stagenet) => new Uri(MoneroNetworkDefaults.WalletStagenetUri),
                (ConnectionType.Wallet, MoneroNetwork.Testnet) => new Uri(MoneroNetworkDefaults.WalletTestnetUri),
                (_, _) => throw new InvalidOperationException($"Unknown MoneroNetwork ({networkType}) and ConnectionType ({connectionType}) combination"),
            };
            _requestAdapter = new MoneroRequestAdapter(uri);
        }

        private static async Task<Stream> ByteArrayToMemoryStream(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return new MemoryStream(responseBody);
        }

        private async Task<MoneroCommunicatorResponse> GetBalanceAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Balance, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BalanceResponse responseObject = await JsonSerializer.DeserializeAsync<BalanceResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Balance,
                BalanceResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, bool all_accounts, bool strict, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                account_index = account_index,
                address_indices = address_indices,
                all_accounts = all_accounts,
                strict = strict,
            };
            return GetBalanceAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                account_index = account_index,
                address_indices = address_indices,
            };
            return GetBalanceAsync(GenericRequestParameters, token);
        }


        public Task<MoneroCommunicatorResponse> GetBalanceAsync(uint account_index, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                account_index = account_index,
            };
            return GetBalanceAsync(GenericRequestParameters, token);
        }

        public async Task<MoneroCommunicatorResponse> GetAddressAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default)
        {
            var walletParameters = new GenericRequestParameters()
            {
                account_index = account_index,
                address_indices = address_indices,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Address, walletParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressResponse responseObject = await JsonSerializer.DeserializeAsync<AddressResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Address,
                AddressResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAddressAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Address, new GenericRequestParameters() { account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressResponse responseObject = await JsonSerializer.DeserializeAsync<AddressResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Address,
                AddressResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(address, nameof(address));
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressIndex, new GenericRequestParameters() { address = address, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressIndexResponse responseObject = await JsonSerializer.DeserializeAsync<AddressIndexResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AddressIndex,
                AddressIndexResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateAddressAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressCreation, new GenericRequestParameters() { account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressCreationResponse responseObject = await JsonSerializer.DeserializeAsync<AddressCreationResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Address,
                MoneroResponseSubType = MoneroResponseSubType.AddressCreation,
                AddressCreationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateAddressAsync(uint account_index, string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(label, nameof(label));
            var GenericRequestParameters = new GenericRequestParameters()
            {
                account_index = account_index,
                label = label,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressCreation, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressCreationResponse responseObject = await JsonSerializer.DeserializeAsync<AddressCreationResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Address,
                MoneroResponseSubType = MoneroResponseSubType.AddressCreation,
                AddressCreationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> LabelAddressAsync(uint major_index, uint minor_index, string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(label, nameof(label));
            var GenericRequestParameters = new GenericRequestParameters()
            {
                index = new AddressIndexParameter()
                {
                    major = major_index,
                    minor = minor_index,
                },
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressLabeling, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressLabelResponse responseObject = await JsonSerializer.DeserializeAsync<AddressLabelResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Address,
                MoneroResponseSubType = MoneroResponseSubType.AddressCreation,
                AddressLabelResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(tag, nameof(tag));
            var GenericRequestParameters = new GenericRequestParameters()
            {
                label = tag,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Account, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountResponse responseObject = await JsonSerializer.DeserializeAsync<AccountResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Account,
                AccountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAccountsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Account, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountResponse responseObject = await JsonSerializer.DeserializeAsync<AccountResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Account,
                AccountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateAccountAsync(string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(label, nameof(label));
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountCreation, new GenericRequestParameters() { label = label, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CreateAccountResponse responseObject = await JsonSerializer.DeserializeAsync<CreateAccountResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountCreation,
                CreateAccountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateAccountAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountCreation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CreateAccountResponse responseObject = await JsonSerializer.DeserializeAsync<CreateAccountResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountCreation,
                CreateAccountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> LabelAccountAsync(uint account_index, string label, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(label, nameof(label));
            var GenericRequestParameters = new GenericRequestParameters()
            {
                label = label,
                account_index = account_index,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountLabeling, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountLabelResponse responseObject = await JsonSerializer.DeserializeAsync<AccountLabelResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountLabeling,
                AccountLabelResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAccountTagsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountTags, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountTagsResponse responseObject = await JsonSerializer.DeserializeAsync<AccountTagsResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountTags,
                AccountTagsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(tag, nameof(tag));
            if (accounts == null || !accounts.Any())
                throw new InvalidOperationException("Accounts is either null or empty");
            var GenericRequestParameters = new GenericRequestParameters()
            {
                tag = tag,
                accounts = accounts,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountTagging, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            TagAccountsResponse responseObject = await JsonSerializer.DeserializeAsync<TagAccountsResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountTagging,
                TagAccountsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            if (accounts == null || !accounts.Any())
                throw new InvalidOperationException("Accounts is either null or empty");
            var GenericRequestParameters = new GenericRequestParameters()
            {
                accounts = accounts,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountUntagging, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            UntagAccountsResponse responseObject = await JsonSerializer.DeserializeAsync<UntagAccountsResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountUntagging,
                UntagAccountsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(tag, nameof(tag));
            ErrorGuard.ThrowIfNullOrWhiteSpace(description, nameof(description));
            var GenericRequestParameters = new GenericRequestParameters()
            {
                tag = tag,
                description = description,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountTagAndDescriptionSetting, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountTagAndDescriptionResponse responseObject = await JsonSerializer.DeserializeAsync<AccountTagAndDescriptionResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountTagAndDescriptionSetting,
                SetAccountTagAndDescriptionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetHeightAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Height, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockchainHeightResponse responseObject = await JsonSerializer.DeserializeAsync<BlockchainHeightResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountTagAndDescriptionSetting,
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

        private async Task<MoneroCommunicatorResponse> TransferFundsAsync(GenericRequestParameters GenericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.FundTransfer, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FundTransferResponse responseObject = await JsonSerializer.DeserializeAsync<FundTransferResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.FundTransfer,
                FundTransferResponse = responseObject,
            };
        }

        private async Task<MoneroCommunicatorResponse> TransferSplitFundsAsync(GenericRequestParameters GenericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.FundTransferSplit, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SplitFundTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SplitFundTransferResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.FundTransferSplit,
                FundTransferSplitResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
            };
            return TransferFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, uint unlock_time = 0, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                unlock_time = unlock_time,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
            };
            return TransferFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
                throw new InvalidOperationException($"ring_size must be at least 2");
            var GenericRequestParameters = new GenericRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                unlock_time = unlock_time,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                ring_size = ring_size,
                mixin = ring_size - 1,
            };
            return TransferFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
                throw new InvalidOperationException($"ring_size must be at least 2");
            var GenericRequestParameters = new GenericRequestParameters()
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
            return TransferFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool new_algorithm = true, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
            };
            return TransferSplitFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, bool new_algorithm = true, uint unlock_time = 0, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                new_algorithm = new_algorithm,
                unlock_time = unlock_time,
            };
            return TransferSplitFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                destinations = TransactionToFundTransferParameter(transactions),
                priority = (uint)transfer_priority,
                ring_size = ring_size,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                new_algorithm = new_algorithm,
                unlock_time = unlock_time,
            };
            return TransferSplitFundsAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
                throw new InvalidOperationException($"ring_size must be at least 2");
            var GenericRequestParameters = new GenericRequestParameters()
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
            return TransferSplitFundsAsync(GenericRequestParameters, token);
        }

        public async Task<MoneroCommunicatorResponse> SignTransferAsync(string unsigned_txset, bool export_raw = false, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                unsigned_txset = unsigned_txset,
                export_raw = export_raw,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SignTransfer, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SignTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SignTransferResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SignTransfer,
                SignTransferResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SubmitTransferAsync(string tx_data_hex, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                tx_data_hex = tx_data_hex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SubmitTransfer, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SubmitTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitTransferResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SubmitTransfer,
                SubmitTransferResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SweepDustAsync(bool get_tx_keys, bool get_tx_hex, bool get_tx_metadata, bool do_not_relay = false, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                get_tx_keys = get_tx_keys,
                get_tx_hex = get_tx_hex,
                get_tx_metadata = get_tx_metadata,
                do_not_relay = do_not_relay,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SweepDust, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SweepDustResponse responseObject = await JsonSerializer.DeserializeAsync<SweepDustResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SweepDust,
                SweepDustResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SweepAllAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, ulong below_amount = ulong.MaxValue, bool get_tx_keys = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
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
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SweepAll, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SweepAllResponse responseObject = await JsonSerializer.DeserializeAsync<SweepAllResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SweepAll,
                SweepAllResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SaveWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SaveWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SaveWalletResponse responseObject = await JsonSerializer.DeserializeAsync<SaveWalletResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SaveWallet,
                SaveWalletResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, bool return_key_image = false, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                transfer_type = transfer_type.ToString().ToLowerInvariant(),
                verbose = return_key_image,
            };
            return GetIncomingTransfersAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, bool return_key_image = false, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                transfer_type = transfer_type.ToString().ToLowerInvariant(),
                verbose = return_key_image,
                account_index = account_index,
            };
            return GetIncomingTransfersAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, IEnumerable<uint> subaddr_indices, bool return_key_image = false, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                transfer_type = transfer_type.ToString().ToLowerInvariant(),
                verbose = return_key_image,
                account_index = account_index,
                subaddr_indices = subaddr_indices,
            };
            return GetIncomingTransfersAsync(GenericRequestParameters, token);
        }

        private async Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(GenericRequestParameters GenericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.IncomingTransfers, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            IncomingTransfersResponse responseObject = await JsonSerializer.DeserializeAsync<IncomingTransfersResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.IncomingTransfers,
                IncomingTransfersResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetPrivateKey(KeyType key_type, CancellationToken token = default)
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

            var GenericRequestParameters = new GenericRequestParameters()
            {
                key_type = KeyTypeToString(key_type),
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.QueryPrivateKey, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            QueryKeyResponse responseObject = await JsonSerializer.DeserializeAsync<QueryKeyResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.QueryPrivateKey,
                QueryKeyResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> StopWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.StopWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            StopWalletResponse responseObject = await JsonSerializer.DeserializeAsync<StopWalletResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.StopWallet,
                StopWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            if (txids == null || !txids.Any())
                throw new InvalidOperationException("txids is either null or empty");
            if (notes == null || !notes.Any())
                throw new InvalidOperationException("notes is either null or empty");
            var GenericRequestParameters = new GenericRequestParameters()
            {
                txids = txids,
                notes = notes,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SetTransactionNotes, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SetTransactionNotesResponse responseObject = await JsonSerializer.DeserializeAsync<SetTransactionNotesResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SetTransactionNotes,
                SetTransactionNotesResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            if (txids == null || !txids.Any())
                throw new InvalidOperationException("txids is either null or empty");
            var GenericRequestParameters = new GenericRequestParameters()
            {
                txids = txids,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.GetTransactionNotes, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetTransactionNotesResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransactionNotesResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.GetTransactionNotes,
                GetTransactionNotesResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(txid, nameof(txid));
            var GenericRequestParameters = new GenericRequestParameters()
            {
                txid = txid,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.GetTransactionKey, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetTransactionKeyResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransactionKeyResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.GetTransactionKey,
                GetTransactionKeyResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CheckTransactionKeyAsync(string txid, string tx_key, string address, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(txid, nameof(txid));
            ErrorGuard.ThrowIfNullOrWhiteSpace(tx_key, nameof(tx_key));
            ErrorGuard.ThrowIfNullOrWhiteSpace(address, nameof(address));
            var GenericRequestParameters = new
            {
                txid = txid,
                tx_key = tx_key,
                address = address,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.CheckTransactionKey, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CheckTransactionKeyResponse responseObject = await JsonSerializer.DeserializeAsync<CheckTransactionKeyResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.CheckTransactionKey,
                CheckTransactionKeyResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            bool isValidRequest = false;
            isValidRequest = @in | @out | pending | failed | pool;
            if (!isValidRequest)
                throw new InvalidOperationException("Not requesting to view any form of transfer");
            var GenericRequestParameters = new GenericRequestParameters()
            {
                @in = @in,
                @out = @out,
                pending = pending,
                failed = failed,
                pool = pool,
            };
            return GetTransfersAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint min_height, uint max_height, CancellationToken token = default)
        {
            bool isValidRequest = false;
            isValidRequest = @in | @out | pending | failed | pool;
            if (!isValidRequest)
                throw new InvalidOperationException("Not requesting to view any form of transfer");
            if (max_height < min_height)
                throw new InvalidOperationException($"max_height ({max_height}) cannot be less than min_height({min_height})");
            var GenericRequestParameters = new GenericRequestParameters()
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
            return GetTransfersAsync(GenericRequestParameters, token);
        }

        private async Task<MoneroCommunicatorResponse> GetTransfersAsync(GenericRequestParameters GenericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Transfers, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ShowTransfersResponse responseObject = await JsonSerializer.DeserializeAsync<ShowTransfersResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.Transfers,
                ShowTransfersResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                txid = txid,
            };
            return GetTransferByTxidAsync(GenericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetTransferByTxidAsync(string txid, uint account_index, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                txid = txid,
                account_index = account_index,
            };
            return GetTransferByTxidAsync(GenericRequestParameters, token);
        }

        private async Task<MoneroCommunicatorResponse> GetTransferByTxidAsync(GenericRequestParameters GenericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.TransferByTxid, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetTransferByTxidResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransferByTxidResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.TransferByTxid,
                TransferByTxidResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SignAsync(string data, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                data = data,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Sign, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SignResponse responseObject = await JsonSerializer.DeserializeAsync<SignResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Miscellaneous,
                MoneroResponseSubType = MoneroResponseSubType.Sign,
                SignResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                data = data,
                address = address,
                signature = signature,

            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Verify, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            VerifyResponse responseObject = await JsonSerializer.DeserializeAsync<VerifyResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Miscellaneous,
                MoneroResponseSubType = MoneroResponseSubType.Verify,
                VerifyResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ExportOutputsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ExportOutputs, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ExportOutputsResponse responseObject = await JsonSerializer.DeserializeAsync<ExportOutputsResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ExportOutputs,
                ExportOutputsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ImportOutputsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ImportOutputs, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ImportOutputsResponse responseObject = await JsonSerializer.DeserializeAsync<ImportOutputsResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ImportOutputs,
                ImportOutputsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ExportKeyImagesAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ExportKeyImages, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ExportKeyImagesResponse responseObject = await JsonSerializer.DeserializeAsync<ExportKeyImagesResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ExportKeyImages,
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

        public async Task<MoneroCommunicatorResponse> ImportKeyImagesAsync(IEnumerable<(string key_image, string signature)> signed_key_images, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                signed_key_images = KeyImageAndSignatureToSignedKeyImages(signed_key_images),
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ImportKeyImages, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ImportKeyImagesResponse responseObject = await JsonSerializer.DeserializeAsync<ImportKeyImagesResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ImportKeyImages,
                ImportKeyImagesResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> MakeUriAsync(string address, ulong amount, string recipient_name, string tx_description = null, string payment_id = null, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                address = address,
                amount = amount,
                recipient_name = recipient_name,
                tx_description = tx_description,
                payment_id = payment_id,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.MakeUri, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            MakeUriResponse responseObject = await JsonSerializer.DeserializeAsync<MakeUriResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.MakeUri,
                MakeUriResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                uri = uri,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ParseUri, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ParseUriResponse responseObject = await JsonSerializer.DeserializeAsync<ParseUriResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.ParseUri,
                ParseUriResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                entries = entries,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.GetAddressBook, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<GetAddressBookResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.GetAddressBook,
                GetAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> AddAddressBookAsync(string address, string description = null, string payment_id = null, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                address = address,
                description = description,
                payment_id = payment_id,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AddAddressBook, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<AddAddressBookResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.AddAddressBook,
                AddAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var GenericRequestParameters = new
            {
                index = index,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.DeleteAddressBook, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            DeleteAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<DeleteAddressBookResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.DeleteAddressBook,
                DeleteAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> RefreshWalletAsync(uint start_height, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                start_height = start_height,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Refresh, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            RefreshWalletResponse responseObject = await JsonSerializer.DeserializeAsync<RefreshWalletResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.Refresh,
                RefreshWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> RescanSpentAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.RescanSpent, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            RescanSpentResponse responseObject = await JsonSerializer.DeserializeAsync<RescanSpentResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.RescanSpent,
                RescanSpentResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                filename = filename,
                language = language,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.CreateWallet, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CreateWalletResponse responseObject = await JsonSerializer.DeserializeAsync<CreateWalletResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.CreateWallet,
                CreateWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                filename = filename,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.OpenWallet, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            OpenWalletResponse responseObject = await JsonSerializer.DeserializeAsync<OpenWalletResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.OpenWallet,
                OpenWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CloseWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.CloseWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CloseWalletResponse responseObject = await JsonSerializer.DeserializeAsync<CloseWalletResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.CloseWallet,
                CloseWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                old_password = oldPassword,
                new_password = newPassword,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ChangeWalletPassword, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ChangeWalletPasswordResponse responseObject = await JsonSerializer.DeserializeAsync<ChangeWalletPasswordResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ChangeWalletPassword,
                ChangeWalletPasswordResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetRpcVersionAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.RpcVersion, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetRpcVersionResponse responseObject = await JsonSerializer.DeserializeAsync<GetRpcVersionResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.RpcVersion,
                GetRpcVersionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> IsMultiSigAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.IsMultiSig, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            IsMultiSigInformationResponse responseObject = await JsonSerializer.DeserializeAsync<IsMultiSigInformationResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.IsMultiSig,
                IsMultiSigInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> PrepareMultiSigAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.PrepareMultiSig, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            PrepareMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<PrepareMultiSigResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.PrepareMultiSig,
                PrepareMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> MakeMultiSigAsync(IEnumerable<string> multisig_info, uint threshold, string password, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                multisig_info = multisig_info,
                threshold = threshold,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.MakeMultiSig, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            MakeMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<MakeMultiSigResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.MakeMultiSig,
                MakeMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ExportMultiSigInfo, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ExportMultiSigInfoResponse responseObject = await JsonSerializer.DeserializeAsync<ExportMultiSigInfoResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ExportMultiSigInfo,
                ExportMultiSigInfoResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.ImportMultiSigInfo, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ImportMultiSigInfoResponse responseObject = await JsonSerializer.DeserializeAsync<ImportMultiSigInfoResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ImportMultiSigInfo,
                ImportMultiSigInfoResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> FinalizeMultiSigAsync(IEnumerable<string> multisigInfo, string password, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                multisig_info = multisigInfo,
                password = password,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.FinalizeMultiSig, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FinalizeMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<FinalizeMultiSigResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.FinalizeMultiSig,
                FinalizeMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SignMultiSigAsync(string tx_data_hex, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                tx_data_hex = tx_data_hex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SignMultiSigTransaction, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SignMultiSigTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SignMultiSigTransactionResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SignMultiSigTransaction,
                SignMultiSigTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var GenericRequestParameters = new GenericRequestParameters()
            {
                tx_data_hex = txDataHex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SubmitMultiSigTransaction, GenericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SubmitMultiSigTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitMultiSigTransactionResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SubmitMultiSigTransaction,
                SubmitMultiSigTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockCountAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockCount, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockCountResponse responseObject = await JsonSerializer.DeserializeAsync<BlockCountResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockCount,
                MoneroResponseSubType = MoneroResponseSubType.BlockCount,
                BlockCountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockHeaderByHashAsync(string hash, CancellationToken token)
        {
            ErrorGuard.ThrowIfNullOrWhiteSpace(hash, nameof(hash));
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByHash, new GenericRequestParameters() { hash = hash, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByHash,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockHeaderByHeightAsync(uint height, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByHeight, new GenericRequestParameters() { height = height, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByHeight,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockHeaderRangeAsync(uint startHeight, uint endHeight, CancellationToken token)
        {
            if (endHeight < startHeight)
                throw new InvalidOperationException($"startHeight ({startHeight}) cannot be greater than endHeight ({endHeight})");
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByRange, new GenericRequestParameters() { start_height = startHeight, end_height = endHeight, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderRangeResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderRangeResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByRange,
                BlockHeaderRangeResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetConnectionsAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AllConnections, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ConnectionResponse responseObject = await JsonSerializer.DeserializeAsync<ConnectionResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Connection,
                MoneroResponseSubType = MoneroResponseSubType.AllConnections,
                ConnectionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetDaemonInformationAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.NodeInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            DaemonInformationResponse responseObject = await JsonSerializer.DeserializeAsync<DaemonInformationResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.NodeInformation,
                DaemonInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetHardforkInformationAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.HardforkInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            HardforkInformationResponse responseObject = await JsonSerializer.DeserializeAsync<HardforkInformationResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.NodeInformation,
                HardforkInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBansAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.BanInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetBansResponse responseObject = await JsonSerializer.DeserializeAsync<GetBansResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.NodeInformation,
                GetBansResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetLastBlockHeaderAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByRecency, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByRecency,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.FlushTransactionPool, new GenericRequestParameters() { txids = txids, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FlushTransactionPoolResponse responseObject = await JsonSerializer.DeserializeAsync<FlushTransactionPoolResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.TransactionPool,
                MoneroResponseSubType = MoneroResponseSubType.FlushTransactionPool,
                FlushTransactionPoolResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetOutputHistogramAsync(IEnumerable<ulong> amounts, uint min_count, uint max_count, bool unlocked, uint recent_cutoff, CancellationToken token)
        {
            if (min_count > max_count)
                throw new InvalidOperationException($"min_count ({min_count}) cannot be greater than max_count ({max_count})");
            var requestParameters = new GenericRequestParameters()
            {
                amounts = amounts,
                min_count = min_count,
                max_count = max_count,
                unlocked = unlocked,
                recent_cutoff = recent_cutoff,
            };

            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.OutputHistogram, requestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            OutputHistogramResponse responseObject = await JsonSerializer.DeserializeAsync<OutputHistogramResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.OutputHistogram,
                OutputHistogramResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetCoinbaseTransactionSumAsync(uint height, uint count, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.CoinbaseTransactionSum, new GenericRequestParameters() { height = height, count = count, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CoinbaseTransactionSumResponse responseObject = await JsonSerializer.DeserializeAsync<CoinbaseTransactionSumResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Coinbase,
                MoneroResponseSubType = MoneroResponseSubType.CoinbaseTransactionSum,
                CoinbaseTransactionSumReponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetDaemonVersionAsync(CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.DaemonVersion, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            VersionResponse responseObject = await JsonSerializer.DeserializeAsync<VersionResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Daemon,
                MoneroResponseSubType = MoneroResponseSubType.DaemonVersion,
                VersionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.FeeEstimate, new GenericRequestParameters() { grace_blocks = grace_blocks }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FeeEstimateResponse responseObject = await JsonSerializer.DeserializeAsync<FeeEstimateResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.FeeEstimate,
                FeeEstimateResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAlternateChainsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AlternateChain, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AlternateChainResponse responseObject = await JsonSerializer.DeserializeAsync<AlternateChainResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Blockchain,
                MoneroResponseSubType = MoneroResponseSubType.AlternateChain,
                AlternateChainResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> RelayTransactionAsync(string hex, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                hex = hex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.AlternateChain, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            RelayTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<RelayTransactionResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.RelayTransaction,
                RelayTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SyncInformationAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SyncInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SyncronizeInformationResponse responseObject = await JsonSerializer.DeserializeAsync<SyncronizeInformationResponse>(ms, _defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.SyncInformation,
                SyncronizeInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockAsync(uint height, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Block, new GenericRequestParameters() { height = height }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockResponse responseObject = await JsonSerializer.DeserializeAsync<BlockResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Block,
                MoneroResponseSubType = MoneroResponseSubType.Block,
                BlockResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockAsync(string hash, CancellationToken token = default)
        {
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.Block, new GenericRequestParameters() { hash = hash }, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockResponse responseObject = await JsonSerializer.DeserializeAsync<BlockResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Block,
                MoneroResponseSubType = MoneroResponseSubType.Block,
                BlockResponse = responseObject,
            };
        }

        private List<NodeBan> BanInformationToBans(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans)
        {
            var requestBans = new List<NodeBan>();
            foreach (var ban in bans)
            {
                requestBans.Add(new NodeBan()
                {
                    Host = ban.host,
                    IP = ban.ip,
                    IsBanned = ban.ban,
                    Seconds = ban.seconds,
                });
            }
            return requestBans;
        }

        public async Task<MoneroCommunicatorResponse> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                bans = BanInformationToBans(bans)
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SetBans, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SetBansResponse responseObject = await JsonSerializer.DeserializeAsync<SetBansResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Connection,
                MoneroResponseSubType = MoneroResponseSubType.SetBans,
                SetBansResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SweepSingleAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time, bool get_tx_key, bool get_tx_hex, bool get_tx_metadata, CancellationToken token = default)
        {
            var walletRequestParameters = new GenericRequestParameters()
            {
                address = address,
                account_index = account_index,
                priority = (uint)transaction_priority,
                ring_size = ring_size,
                unlock_time = unlock_time,
                get_tx_key = get_tx_key,
                get_tx_hex = get_tx_hex,
                get_tx_metadata = get_tx_metadata,
            };

            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SweepSingle, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SweepSingleResponse responseObject = await JsonSerializer.DeserializeAsync<SweepSingleResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SweepSingle,
                SweepSingleResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> DescribeTransferAsync(string tx_set, bool is_multisig, CancellationToken token = default)
        {
            var walletRequestParameters = new GenericRequestParameters();
            if (is_multisig)
                walletRequestParameters.multisig_txset = tx_set;
            else
                walletRequestParameters.unsigned_txset = tx_set;

            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.DescribeTransfer, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            DescribeTransferResponse responseObject = await JsonSerializer.DeserializeAsync<DescribeTransferResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.DescribeTransfer,
                DescribeTransferResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetPaymentDetailAsync(string payment_id, CancellationToken token = default)
        {
            var walletRequestParameters = new GenericRequestParameters()
            {
                payment_id = payment_id,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.GetPaymentDetail, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            PaymentDetailResponse responseObject = await JsonSerializer.DeserializeAsync<PaymentDetailResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.GetPaymentDetail,
                PaymentDetailResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SendRawTransactionAsync(string tx_as_hex, bool do_not_relay, bool do_sanity_checks, CancellationToken token = default)
        {
            var walletRequestParameters = new GenericRequestParameters()
            {
                do_not_relay = do_not_relay,
                do_sanity_checks = do_sanity_checks,
                tx_as_hex = tx_as_hex,
            };
            HttpRequestMessage request = await _requestAdapter.GetRequestMessage(MoneroResponseSubType.SendRawTransaction, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SendRawTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SendRawTransactionResponse>(ms, _defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SendRawTransaction,
                SendRawTransactionResponse = responseObject,
            };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
