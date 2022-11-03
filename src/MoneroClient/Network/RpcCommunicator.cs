using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Daemon.POD;
using Monero.Client.Daemon.POD.Requests;
using Monero.Client.Daemon.POD.Responses;
using Monero.Client.Enums;
using Monero.Client.Network;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Requests;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Utilities
{
    internal class RpcCommunicator
    {
        private readonly HttpClient httpClient;
        private readonly MoneroRequestAdapter requestAdapter;
        private readonly JsonSerializerOptions defaultSerializationOptions = new JsonSerializerOptions()
        { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        /// <param name="host">A string representation of the host you'd like to connect to (e.g. "127.0.0.1").</param>
        /// <param name="port">An integer representation of the host's port you'd like to communicate on (e.g. 18081).</param>
        public RpcCommunicator(string host, uint port) : this()
        {
            this.requestAdapter = new MoneroRequestAdapter(host, port);
        }

        public RpcCommunicator(MoneroNetwork networkType, ConnectionType connectionType) : this()
        {
            this.requestAdapter = (connectionType, networkType) switch
            {
                (ConnectionType.Daemon, MoneroNetwork.Mainnet) => new MoneroRequestAdapter(MoneroNetworkDefaults.DaemonMainnetHost, MoneroNetworkDefaults.DaemonMainnetPort),
                (ConnectionType.Daemon, MoneroNetwork.Stagenet) => new MoneroRequestAdapter(MoneroNetworkDefaults.DaemonStagenetHost, MoneroNetworkDefaults.DaemonStagenetPort),
                (ConnectionType.Daemon, MoneroNetwork.Testnet) => new MoneroRequestAdapter(MoneroNetworkDefaults.DaemonTestnetHost, MoneroNetworkDefaults.DaemonTestnetPort),
                (ConnectionType.Wallet, MoneroNetwork.Mainnet) => new MoneroRequestAdapter(MoneroNetworkDefaults.WalletMainnetHost, MoneroNetworkDefaults.WalletMainnetPort),
                (ConnectionType.Wallet, MoneroNetwork.Stagenet) => new MoneroRequestAdapter(MoneroNetworkDefaults.WalletStagenetHost, MoneroNetworkDefaults.WalletStagenetPort),
                (ConnectionType.Wallet, MoneroNetwork.Testnet) => new MoneroRequestAdapter(MoneroNetworkDefaults.WalletTestnetHost, MoneroNetworkDefaults.WalletTestnetPort),
                (_, _) => throw new InvalidOperationException($"Unknown MoneroNetwork ({networkType}) and ConnectionType ({connectionType}) combination"),
            };
        }

        private RpcCommunicator()
        {
            this.httpClient = new HttpClient();
        }

        public Task<MoneroCommunicatorResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, bool all_accounts, bool strict, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Account_index = account_index,
                Address_indices = address_indices,
                All_accounts = all_accounts,
                Strict = strict,
            };
            return this.GetBalanceAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Account_index = account_index,
                Address_indices = address_indices,
            };
            return this.GetBalanceAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetBalanceAsync(uint account_index, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Account_index = account_index,
            };
            return this.GetBalanceAsync(genericRequestParameters, token);
        }

        public async Task<MoneroCommunicatorResponse> GetAddressAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default)
        {
            var walletParameters = new GenericRequestParameters()
            {
                Account_index = account_index,
                Address_indices = address_indices,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Address, walletParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressResponse responseObject = await JsonSerializer.DeserializeAsync<AddressResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Address,
                AddressResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAddressAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Address, new GenericRequestParameters() { Account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressResponse responseObject = await JsonSerializer.DeserializeAsync<AddressResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressIndex, new GenericRequestParameters() { Address = address, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressIndexResponse responseObject = await JsonSerializer.DeserializeAsync<AddressIndexResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AddressIndex,
                AddressIndexResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateAddressAsync(uint account_index, CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressCreation, new GenericRequestParameters() { Account_index = account_index, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressCreationResponse responseObject = await JsonSerializer.DeserializeAsync<AddressCreationResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new GenericRequestParameters()
            {
                Account_index = account_index,
                Label = label,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressCreation, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressCreationResponse responseObject = await JsonSerializer.DeserializeAsync<AddressCreationResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new GenericRequestParameters()
            {
                Index = new AddressIndexParameter()
                {
                    Major = major_index,
                    Minor = minor_index,
                },
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AddressLabeling, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddressLabelResponse responseObject = await JsonSerializer.DeserializeAsync<AddressLabelResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new GenericRequestParameters()
            {
                Label = tag,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Account, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountResponse responseObject = await JsonSerializer.DeserializeAsync<AccountResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Account,
                AccountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAccountsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Account, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountResponse responseObject = await JsonSerializer.DeserializeAsync<AccountResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountCreation, new GenericRequestParameters() { Label = label, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CreateAccountResponse responseObject = await JsonSerializer.DeserializeAsync<CreateAccountResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountCreation,
                CreateAccountResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateAccountAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountCreation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CreateAccountResponse responseObject = await JsonSerializer.DeserializeAsync<CreateAccountResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new GenericRequestParameters()
            {
                Label = label,
                Account_index = account_index,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountLabeling, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountLabelResponse responseObject = await JsonSerializer.DeserializeAsync<AccountLabelResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountLabeling,
                AccountLabelResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAccountTagsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountTags, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountTagsResponse responseObject = await JsonSerializer.DeserializeAsync<AccountTagsResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            {
                throw new InvalidOperationException("Accounts is either null or empty");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Tag = tag,
                Accounts = accounts,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountTagging, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            TagAccountsResponse responseObject = await JsonSerializer.DeserializeAsync<TagAccountsResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            {
                throw new InvalidOperationException($"{nameof(accounts)} is either null or empty");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Accounts = accounts,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountUntagging, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            UntagAccountsResponse responseObject = await JsonSerializer.DeserializeAsync<UntagAccountsResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new GenericRequestParameters()
            {
                Tag = tag,
                Description = description,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AccountTagAndDescriptionSetting, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AccountTagAndDescriptionResponse responseObject = await JsonSerializer.DeserializeAsync<AccountTagAndDescriptionResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountTagAndDescriptionSetting,
                SetAccountTagAndDescriptionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetHeightAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Height, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockchainHeightResponse responseObject = await JsonSerializer.DeserializeAsync<BlockchainHeightResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.AccountTagAndDescriptionSetting,
                BlockchainHeightResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
            };
            return this.TransferFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, ulong unlock_time = 0, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
                Unlock_time = unlock_time,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
            };
            return this.TransferFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, ulong unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
            {
                throw new InvalidOperationException($"ring_size must be at least 2");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
                Unlock_time = unlock_time,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
                Ring_size = ring_size,
                Mixin = ring_size - 1,
            };
            return this.TransferFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, ulong unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
            {
                throw new InvalidOperationException($"ring_size must be at least 2");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
                Unlock_time = unlock_time,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
                Ring_size = ring_size,
                Mixin = ring_size - 1,
                Account_index = account_index,
            };
            return this.TransferFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool new_algorithm = true, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
            };
            return this.TransferSplitFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, bool new_algorithm = true, ulong unlock_time = 0, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
                New_algorithm = new_algorithm,
                Unlock_time = unlock_time,
            };
            return this.TransferSplitFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, bool new_algorithm = true, ulong unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
                Ring_size = ring_size,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
                New_algorithm = new_algorithm,
                Unlock_time = unlock_time,
            };
            return this.TransferSplitFundsAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, bool new_algorithm = true, ulong unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default)
        {
            if (ring_size <= 1)
            {
                throw new InvalidOperationException($"ring_size must be at least 2");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Destinations = TransactionToFundTransferParameter(transactions),
                Priority = (uint)transfer_priority,
                Ring_size = ring_size,
                Mixin = ring_size - 1,
                Account_index = account_index,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
                New_algorithm = new_algorithm,
                Unlock_time = unlock_time,
            };
            return this.TransferSplitFundsAsync(genericRequestParameters, token);
        }

        public async Task<MoneroCommunicatorResponse> SignTransferAsync(string unsigned_txset, bool export_raw = false, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Unsigned_txset = unsigned_txset,
                Export_raw = export_raw,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SignTransfer, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SignTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SignTransferResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SignTransfer,
                SignTransferResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SubmitTransferAsync(string tx_data_hex, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Tx_data_hex = tx_data_hex,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SubmitTransfer, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SubmitTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitTransferResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SubmitTransfer,
                SubmitTransferResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SweepDustAsync(bool get_tx_keys, bool get_tx_hex, bool get_tx_metadata, bool do_not_relay = false, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Get_tx_keys = get_tx_keys,
                Get_tx_hex = get_tx_hex,
                Get_tx_metadata = get_tx_metadata,
                Do_not_relay = do_not_relay,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SweepDust, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SweepDustResponse responseObject = await JsonSerializer.DeserializeAsync<SweepDustResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SweepDust,
                SweepDustResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SweepAllAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, ulong below_amount = ulong.MaxValue, bool get_tx_keys = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Address = address,
                Account_index = account_index,
                Priority = (uint)transaction_priority,
                Ring_size = ring_size,
                Mixin = ring_size - 1,
                Unlock_time = unlock_time,
                Get_tx_keys = get_tx_keys,
                Get_tx_hex = get_tx_hex,
                Get_tx_metadata = get_tx_metadata,
                Below_amount = below_amount,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SweepAll, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using Stream ms = new MemoryStream(responseBody);
            SweepAllResponse responseObject = await JsonSerializer.DeserializeAsync<SweepAllResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SweepAll,
                SweepAllResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SaveWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SaveWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SaveWalletResponse responseObject = await JsonSerializer.DeserializeAsync<SaveWalletResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SaveWallet,
                SaveWalletResponse = responseObject,
            };
        }

        public Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, bool return_key_image = false, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Transfer_type = transfer_type.ToString().ToLowerInvariant(),
                Verbose = return_key_image,
            };
            return this.GetIncomingTransfersAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, bool return_key_image = false, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Transfer_type = transfer_type.ToString().ToLowerInvariant(),
                Verbose = return_key_image,
                Account_index = account_index,
            };
            return this.GetIncomingTransfersAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, IEnumerable<uint> subaddr_indices, bool return_key_image = false, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Transfer_type = transfer_type.ToString().ToLowerInvariant(),
                Verbose = return_key_image,
                Account_index = account_index,
                Subaddr_indices = subaddr_indices,
            };
            return this.GetIncomingTransfersAsync(genericRequestParameters, token);
        }

        public async Task<MoneroCommunicatorResponse> GetPrivateKey(KeyType key_type, CancellationToken token = default)
        {
            static string KeyTypeToString(KeyType keyType)
            {
                return keyType switch
                {
                    KeyType.Mnemonic => "mnemonic",
                    KeyType.ViewKey => "view_key",
                    KeyType.SpendKey => "spend_key",
                    _ => throw new InvalidOperationException($"Unknown KeyType ({keyType})"),
                };
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Key_type = KeyTypeToString(key_type),
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.QueryPrivateKey, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            QueryKeyResponse responseObject = await JsonSerializer.DeserializeAsync<QueryKeyResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.QueryPrivateKey,
                QueryKeyResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> StopWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.StopWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            StopWalletResponse responseObject = await JsonSerializer.DeserializeAsync<StopWalletResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            {
                throw new InvalidOperationException("txids is either null or empty");
            }

            if (notes == null || !notes.Any())
            {
                throw new InvalidOperationException("notes is either null or empty");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Txids = txids,
                Notes = notes,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SetTransactionNotes, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SetTransactionNotesResponse responseObject = await JsonSerializer.DeserializeAsync<SetTransactionNotesResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            {
                throw new InvalidOperationException("txids is either null or empty");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                Txids = txids,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetTransactionNotes, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetTransactionNotesResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransactionNotesResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new GenericRequestParameters()
            {
                Txid = txid,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetTransactionKey, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetTransactionKeyResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransactionKeyResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            var genericRequestParameters = new
            {
                txid = txid,
                tx_key = tx_key,
                address = address,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.CheckTransactionKey, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CheckTransactionKeyResponse responseObject = await JsonSerializer.DeserializeAsync<CheckTransactionKeyResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            {
                throw new InvalidOperationException("Not requesting to view any form of transfer");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                In = @in,
                Out = @out,
                Pending = pending,
                Failed = failed,
                Pool = pool,
            };
            return this.GetTransfersAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, ulong min_height, ulong max_height, CancellationToken token = default)
        {
            bool isValidRequest = false;
            isValidRequest = @in | @out | pending | failed | pool;
            if (!isValidRequest)
            {
                throw new InvalidOperationException("Not requesting to view any form of transfer");
            }

            if (max_height < min_height)
            {
                throw new InvalidOperationException($"max_height ({max_height}) cannot be less than min_height({min_height})");
            }

            var genericRequestParameters = new GenericRequestParameters()
            {
                In = @in,
                Out = @out,
                Pending = pending,
                Failed = failed,
                Pool = pool,
                Min_height = min_height,
                Max_height = max_height,
                Filter_by_height = true,
            };
            return this.GetTransfersAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Txid = txid,
            };
            return this.GetTransferByTxidAsync(genericRequestParameters, token);
        }

        public Task<MoneroCommunicatorResponse> GetTransferByTxidAsync(string txid, uint account_index, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Txid = txid,
                Account_index = account_index,
            };
            return this.GetTransferByTxidAsync(genericRequestParameters, token);
        }

        public async Task<MoneroCommunicatorResponse> SignAsync(string data, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Data = data,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Sign, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SignResponse responseObject = await JsonSerializer.DeserializeAsync<SignResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Miscellaneous,
                MoneroResponseSubType = MoneroResponseSubType.Sign,
                SignResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Data = data,
                Address = address,
                Signature = signature,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Verify, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            VerifyResponse responseObject = await JsonSerializer.DeserializeAsync<VerifyResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Miscellaneous,
                MoneroResponseSubType = MoneroResponseSubType.Verify,
                VerifyResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ExportOutputsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ExportOutputs, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ExportOutputsResponse responseObject = await JsonSerializer.DeserializeAsync<ExportOutputsResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ExportOutputs,
                ExportOutputsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ImportOutputsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ImportOutputs, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ImportOutputsResponse responseObject = await JsonSerializer.DeserializeAsync<ImportOutputsResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ImportOutputs,
                ImportOutputsResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ExportKeyImagesAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ExportKeyImages, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ExportKeyImagesResponse responseObject = await JsonSerializer.DeserializeAsync<ExportKeyImagesResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ExportKeyImages,
                ExportKeyImagesResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ImportKeyImagesAsync(IEnumerable<(string key_image, string signature)> signed_key_images, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Signed_key_images = KeyImageAndSignatureToSignedKeyImages(signed_key_images),
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ImportKeyImages, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ImportKeyImagesResponse responseObject = await JsonSerializer.DeserializeAsync<ImportKeyImagesResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ImportKeyImages,
                ImportKeyImagesResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> MakeUriAsync(string address, ulong amount, string recipient_name, string tx_description = null, string payment_id = null, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Address = address,
                Amount = amount,
                Recipient_name = recipient_name,
                Tx_description = tx_description,
                Payment_id = payment_id,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.MakeUri, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            MakeUriResponse responseObject = await JsonSerializer.DeserializeAsync<MakeUriResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.MakeUri,
                MakeUriResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Uri = uri,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ParseUri, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ParseUriResponse responseObject = await JsonSerializer.DeserializeAsync<ParseUriResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.ParseUri,
                ParseUriResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Entries = entries,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetAddressBook, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<GetAddressBookResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.GetAddressBook,
                GetAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> AddAddressBookAsync(string address, string description = null, string payment_id = null, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Address = address,
                Description = description,
                Payment_id = payment_id,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AddAddressBook, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AddAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<AddAddressBookResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.AddAddressBook,
                AddAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var genericRequestParameters = new
            {
                index = index,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.DeleteAddressBook, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            DeleteAddressBookResponse responseObject = await JsonSerializer.DeserializeAsync<DeleteAddressBookResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.DeleteAddressBook,
                DeleteAddressBookResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> RefreshWalletAsync(uint start_height, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Start_height = start_height,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Refresh, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            RefreshWalletResponse responseObject = await JsonSerializer.DeserializeAsync<RefreshWalletResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.Refresh,
                RefreshWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> RescanSpentAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.RescanSpent, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            RescanSpentResponse responseObject = await JsonSerializer.DeserializeAsync<RescanSpentResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.RescanSpent,
                RescanSpentResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Filename = filename,
                Language = language,
                Password = password,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.CreateWallet, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CreateWalletResponse responseObject = await JsonSerializer.DeserializeAsync<CreateWalletResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.CreateWallet,
                CreateWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Filename = filename,
                Password = password,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.OpenWallet, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            OpenWalletResponse responseObject = await JsonSerializer.DeserializeAsync<OpenWalletResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.OpenWallet,
                OpenWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> CloseWalletAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.CloseWallet, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CloseWalletResponse responseObject = await JsonSerializer.DeserializeAsync<CloseWalletResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.CloseWallet,
                CloseWalletResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Old_password = oldPassword,
                New_password = newPassword,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ChangeWalletPassword, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ChangeWalletPasswordResponse responseObject = await JsonSerializer.DeserializeAsync<ChangeWalletPasswordResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ChangeWalletPassword,
                ChangeWalletPasswordResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetRpcVersionAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.RpcVersion, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetRpcVersionResponse responseObject = await JsonSerializer.DeserializeAsync<GetRpcVersionResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.RpcVersion,
                GetRpcVersionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> IsMultiSigAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.IsMultiSig, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            MultiSigInformationResponse responseObject = await JsonSerializer.DeserializeAsync<MultiSigInformationResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.IsMultiSig,
                MultiSigInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> PrepareMultiSigAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.PrepareMultiSig, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            PrepareMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<PrepareMultiSigResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.PrepareMultiSig,
                PrepareMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> MakeMultiSigAsync(IEnumerable<string> multisig_info, uint threshold, string password, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Multisig_info = multisig_info,
                Threshold = threshold,
                Password = password,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.MakeMultiSig, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            MakeMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<MakeMultiSigResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.MakeMultiSig,
                MakeMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ExportMultiSigInfo, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ExportMultiSigInfoResponse responseObject = await JsonSerializer.DeserializeAsync<ExportMultiSigInfoResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ExportMultiSigInfo,
                ExportMultiSigInfoResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ImportMultiSigInfo, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ImportMultiSigInfoResponse responseObject = await JsonSerializer.DeserializeAsync<ImportMultiSigInfoResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ImportMultiSigInfo,
                ImportMultiSigInfoResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> FinalizeMultiSigAsync(IEnumerable<string> multisigInfo, string password, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Multisig_info = multisigInfo,
                Password = password,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.FinalizeMultiSig, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FinalizeMultiSigResponse responseObject = await JsonSerializer.DeserializeAsync<FinalizeMultiSigResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.FinalizeMultiSig,
                FinalizeMultiSigResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SignMultiSigAsync(string tx_data_hex, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Tx_data_hex = tx_data_hex,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SignMultiSigTransaction, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SignMultiSigTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SignMultiSigTransactionResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SignMultiSigTransaction,
                SignMultiSigTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var genericRequestParameters = new GenericRequestParameters()
            {
                Tx_data_hex = txDataHex,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SubmitMultiSigTransaction, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SubmitMultiSigTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitMultiSigTransactionResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SubmitMultiSigTransaction,
                SubmitMultiSigTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockCountAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockCount, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockCountResponse responseObject = await JsonSerializer.DeserializeAsync<BlockCountResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByHash, new GenericRequestParameters() { Hash = hash, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByHash,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockHeaderByHeightAsync(ulong height, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByHeight, new GenericRequestParameters() { Height = height, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, this.defaultSerializationOptions, token);
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
            {
                throw new InvalidOperationException($"startHeight ({startHeight}) cannot be greater than endHeight ({endHeight})");
            }

            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByRange, new GenericRequestParameters() { Start_height = startHeight, End_height = endHeight, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderRangeResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderRangeResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByRange,
                BlockHeaderRangeResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetConnectionsAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AllConnections, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ConnectionResponse responseObject = await JsonSerializer.DeserializeAsync<ConnectionResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Connection,
                MoneroResponseSubType = MoneroResponseSubType.AllConnections,
                ConnectionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetDaemonInformationAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.NodeInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            DaemonInformationResponse responseObject = await JsonSerializer.DeserializeAsync<DaemonInformationResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.NodeInformation,
                DaemonInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetHardforkInformationAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.HardforkInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            HardforkInformationResponse responseObject = await JsonSerializer.DeserializeAsync<HardforkInformationResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.NodeInformation,
                HardforkInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBansAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.BanInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetBansResponse responseObject = await JsonSerializer.DeserializeAsync<GetBansResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.NodeInformation,
                GetBansResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetLastBlockHeaderAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.BlockHeaderByRecency, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockHeaderResponse responseObject = await JsonSerializer.DeserializeAsync<BlockHeaderResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.BlockHeader,
                MoneroResponseSubType = MoneroResponseSubType.BlockHeaderByRecency,
                BlockHeaderResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> FlushTransactionPoolAsync(IEnumerable<string> txids, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.FlushTransactionPool, new GenericRequestParameters() { Txids = txids, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FlushTransactionPoolResponse responseObject = await JsonSerializer.DeserializeAsync<FlushTransactionPoolResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.TransactionPool,
                MoneroResponseSubType = MoneroResponseSubType.FlushTransactionPool,
                FlushTransactionPoolResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetOutputHistogramAsync(IEnumerable<ulong> amounts, ulong from_height, ulong to_height, bool cumulative, bool binary, bool compress, CancellationToken token)
        {
            if (from_height > to_height)
            {
                throw new InvalidOperationException($"from_height ({from_height}) cannot be greater than to_height ({to_height})");
            }

            var requestParameters = new GenericRequestParameters()
            {
                Amounts = amounts,
                From_height = from_height,
                To_height = to_height,
                Cumulative = cumulative,
                Binary = binary,
                Compress = compress,
            };

            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.OutputHistogram, requestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            OutputHistogramResponse responseObject = await JsonSerializer.DeserializeAsync<OutputHistogramResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.OutputHistogram,
                OutputHistogramResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetCoinbaseTransactionSumAsync(ulong height, uint count, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.CoinbaseTransactionSum, new GenericRequestParameters() { Height = height, Count = count, }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            CoinbaseTransactionSumResponse responseObject = await JsonSerializer.DeserializeAsync<CoinbaseTransactionSumResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Coinbase,
                MoneroResponseSubType = MoneroResponseSubType.CoinbaseTransactionSum,
                CoinbaseTransactionSumReponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetDaemonVersionAsync(CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.DaemonVersion, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            VersionResponse responseObject = await JsonSerializer.DeserializeAsync<VersionResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Daemon,
                MoneroResponseSubType = MoneroResponseSubType.DaemonVersion,
                VersionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetFeeEstimateAsync(uint grace_blocks, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.FeeEstimate, new GenericRequestParameters() { Grace_blocks = grace_blocks }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FeeEstimateResponse responseObject = await JsonSerializer.DeserializeAsync<FeeEstimateResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.FeeEstimate,
                FeeEstimateResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAlternateChainsAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AlternateChain, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            AlternateChainResponse responseObject = await JsonSerializer.DeserializeAsync<AlternateChainResponse>(ms, this.defaultSerializationOptions, token);
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
                Hex = hex,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.AlternateChain, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            RelayTransactionResponse responseObject = await JsonSerializer.DeserializeAsync<RelayTransactionResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.RelayTransaction,
                RelayTransactionResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SyncInformationAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SyncInformation, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SyncronizeInformationResponse responseObject = await JsonSerializer.DeserializeAsync<SyncronizeInformationResponse>(ms, this.defaultSerializationOptions, token);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Information,
                MoneroResponseSubType = MoneroResponseSubType.SyncInformation,
                SyncronizeInformationResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockAsync(uint height, CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Block, new GenericRequestParameters() { Height = height }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockResponse responseObject = await JsonSerializer.DeserializeAsync<BlockResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Block,
                MoneroResponseSubType = MoneroResponseSubType.Block,
                BlockResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockAsync(string hash, CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Block, new GenericRequestParameters() { Hash = hash }, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BlockResponse responseObject = await JsonSerializer.DeserializeAsync<BlockResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Block,
                MoneroResponseSubType = MoneroResponseSubType.Block,
                BlockResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SetBansAsync(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Bans = BanInformationToBans(bans)
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SetBans, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SetBansResponse responseObject = await JsonSerializer.DeserializeAsync<SetBansResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
                Address = address,
                Account_index = account_index,
                Priority = (uint)transaction_priority,
                Ring_size = ring_size,
                Unlock_time = unlock_time,
                Get_tx_key = get_tx_key,
                Get_tx_hex = get_tx_hex,
                Get_tx_metadata = get_tx_metadata,
            };

            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SweepSingle, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SweepSingleResponse responseObject = await JsonSerializer.DeserializeAsync<SweepSingleResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.SweepSingle,
                SweepSingleResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> DescribeTransferAsync(string txSet, bool isMultiSig, CancellationToken token = default)
        {
            var walletRequestParameters = new GenericRequestParameters();
            if (isMultiSig)
            {
                walletRequestParameters.Multisig_txset = txSet;
            }
            else
            {
                walletRequestParameters.Unsigned_txset = txSet;
            }

            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.DescribeTransfer, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            DescribeTransferResponse responseObject = await JsonSerializer.DeserializeAsync<DescribeTransferResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
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
                Payment_id = payment_id,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetPaymentDetail, walletRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            PaymentDetailResponse responseObject = await JsonSerializer.DeserializeAsync<PaymentDetailResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.GetPaymentDetail,
                PaymentDetailResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SubmitBlocksAsync(IEnumerable<string> blockBlobs, CancellationToken token = default)
        {
            var daemonRequestParameters = new
            {
                request = blockBlobs,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SubmitBlock, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SubmitBlockResponse responseObject = await JsonSerializer.DeserializeAsync<SubmitBlockResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Block,
                MoneroResponseSubType = MoneroResponseSubType.SubmitBlock,
                SubmitBlockResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBlockTemplateAsync(ulong reserve_size, string wallet_address, string prev_block, string extra_nonce, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Reserve_size = reserve_size,
                Wallet_address = wallet_address,
                Prev_block = prev_block,
                Extra_nonce = extra_nonce,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetBlockTemplate, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetBlockTemplateResponse responseObject = await JsonSerializer.DeserializeAsync<GetBlockTemplateResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Block,
                MoneroResponseSubType = MoneroResponseSubType.GetBlockTemplate,
                GetBlockTemplateResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetBanStatusAsync(string address, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Address = address,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetBanStatus, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetBanStatusResponse responseObject = await JsonSerializer.DeserializeAsync<GetBanStatusResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Connection,
                MoneroResponseSubType = MoneroResponseSubType.GetBanStatus,
                GetBanStatusResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> PruneBlockchainAsync(bool check, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Check = check,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.PruneBlockchain, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            PruneBlockchainResponse responseObject = await JsonSerializer.DeserializeAsync<PruneBlockchainResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Blockchain,
                MoneroResponseSubType = MoneroResponseSubType.PruneBlockchain,
                PruneBlockchainResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetTransactionPoolBacklogAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.TransactionPoolBacklog, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            TransactionPoolBacklogResponse responseObject = await JsonSerializer.DeserializeAsync<TransactionPoolBacklogResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.TransactionPool,
                MoneroResponseSubType = MoneroResponseSubType.TransactionPoolBacklog,
                TransactionPoolBacklogResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> SetAttributeAsync(string key, string value, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Key = key,
                Value = value,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.SetAttribute, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SetAttributeResponse responseObject = await JsonSerializer.DeserializeAsync<SetAttributeResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.SetAttribute,
                SetAttributeResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetAttributeAsync(string key, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Key = key,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.GetAttribute, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetAttributeResponse responseObject = await JsonSerializer.DeserializeAsync<GetAttributeResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.GetAttribute,
                GetAttributeResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> ValidateAddressAsync(string address, bool any_net_type = false, bool allow_openalias = false, CancellationToken token = default)
        {
            var daemonRequestParameters = new GenericRequestParameters()
            {
                Address = address,
                AnyNetType = any_net_type,
                AllowOpenAlias = allow_openalias
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.ValidateAddress, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ValidateAddressResponse responseObject = await JsonSerializer.DeserializeAsync<ValidateAddressResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.ValidateAddress,
                ValidateAddressResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetTransactionPoolAsync(CancellationToken token = default)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.TransactionPoolTransactions, null, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            TransactionPool responseObject = await JsonSerializer.DeserializeAsync<TransactionPool>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.TransactionPool,
                MoneroResponseSubType = MoneroResponseSubType.TransactionPoolTransactions,
                TransactionPoolResponse = responseObject,
            };
        }

        public async Task<MoneroCommunicatorResponse> GetTransactionsAsync(IEnumerable<string> txHashes, CancellationToken token = default)
        {
            var daemonRequestParameters = new CustomRequestParameters()
            {
                Txs_hashes = txHashes,
            };
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Transactions, daemonRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            TransactionSet responseObject = await JsonSerializer.DeserializeAsync<TransactionSet>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.Transactions,
                TransactionsResponse = responseObject,
            };
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }

        private static async Task<Stream> ByteArrayToMemoryStream(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return new MemoryStream(responseBody);
        }

        private static async Task<string> ByteArrayToString(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseBody;
        }

        private static List<FundTransferParameter> TransactionToFundTransferParameter(IEnumerable<(string address, ulong amount)> transactions)
        {
            List<FundTransferParameter> fundTransferParameters = new List<FundTransferParameter>();
            foreach (var da in transactions)
            {
                fundTransferParameters.Add(new FundTransferParameter()
                {
                    Address = da.address,
                    Amount = da.amount,
                });
            }

            return fundTransferParameters;
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

        private static List<NodeBan> BanInformationToBans(IEnumerable<(string host, ulong ip, bool ban, uint seconds)> bans)
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

        private async Task<MoneroCommunicatorResponse> GetBalanceAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Balance, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            BalanceResponse responseObject = await JsonSerializer.DeserializeAsync<BalanceResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Account,
                MoneroResponseSubType = MoneroResponseSubType.Balance,
                BalanceResponse = responseObject,
            };
        }

        private async Task<MoneroCommunicatorResponse> GetIncomingTransfersAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.IncomingTransfers, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            IncomingTransfersResponse responseObject = await JsonSerializer.DeserializeAsync<IncomingTransfersResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.IncomingTransfers,
                IncomingTransfersResponse = responseObject,
            };
        }

        private async Task<MoneroCommunicatorResponse> GetTransferByTxidAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.TransferByTxid, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            GetTransferByTxidResponse responseObject = await JsonSerializer.DeserializeAsync<GetTransferByTxidResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.TransferByTxid,
                TransferByTxidResponse = responseObject,
            };
        }

        private async Task<MoneroCommunicatorResponse> TransferFundsAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.FundTransfer, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            FundTransferResponse responseObject = await JsonSerializer.DeserializeAsync<FundTransferResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.FundTransfer,
                FundTransferResponse = responseObject,
            };
        }

        private async Task<MoneroCommunicatorResponse> TransferSplitFundsAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.FundTransferSplit, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            SplitFundTransferResponse responseObject = await JsonSerializer.DeserializeAsync<SplitFundTransferResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Transaction,
                MoneroResponseSubType = MoneroResponseSubType.FundTransferSplit,
                FundTransferSplitResponse = responseObject,
            };
        }

        private async Task<MoneroCommunicatorResponse> GetTransfersAsync(GenericRequestParameters genericRequestParameters, CancellationToken token)
        {
            HttpRequestMessage request = await this.requestAdapter.GetRequestMessage(MoneroResponseSubType.Transfers, genericRequestParameters, token).ConfigureAwait(false);
            HttpResponseMessage response = await this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using Stream ms = await ByteArrayToMemoryStream(response).ConfigureAwait(false);
            ShowTransfersResponse responseObject = await JsonSerializer.DeserializeAsync<ShowTransfersResponse>(ms, this.defaultSerializationOptions, token).ConfigureAwait(false);
            return new MoneroCommunicatorResponse()
            {
                MoneroResponseType = MoneroResponseType.Wallet,
                MoneroResponseSubType = MoneroResponseSubType.Transfers,
                ShowTransfersResponse = responseObject,
            };
        }
    }
}
