using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Constants;
using Monero.Client.Enums;

namespace Monero.Client.Network
{
    internal class MoneroRequestAdapter
    {
        private static readonly JsonSerializerOptions DefaultSerializationOptions = new JsonSerializerOptions()
        { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        private readonly string host;
        private readonly uint port;

        public MoneroRequestAdapter(string host, uint port)
        {
            this.host = host;
            this.port = port;
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, CustomRequestParameters requestParams, CancellationToken token)
        {
            if (requestParams == null)
            {
                return this.GetRequestMessage(subType, new GenericRequestParameters(), token);
            }

            var request = GetRequest(subType, requestParams);
            IUriBuilder uriBuilder = new UriBuilderDirector(new UriBuilder(this.host, this.port, RequestEndpointExtensionRetriever.FetchEndpoint(request)));
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Build());
            return SerializeRequest(httpRequestMessage, request, token);
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, GenericRequestParameters requestParams, CancellationToken token)
        {
            var request = GetRequest(subType, requestParams);
            IUriBuilder uriBuilder = new UriBuilderDirector(new UriBuilder(this.host, this.port, RequestEndpointExtensionRetriever.FetchEndpoint(request)));
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Build());
            return SerializeRequest(httpRequestMessage, request, token);
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, dynamic requestParams, CancellationToken token)
        {
            AnonymousRequest request = GetRequest(subType, requestParams);
            IUriBuilder uriBuilder = new UriBuilderDirector(new UriBuilder(this.host, this.port, RequestEndpointExtensionRetriever.FetchEndpoint(request)));
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Build());
            return SerializeRequest(httpRequestMessage, request, token);
        }

        private static AnonymousRequest GetRequest(MoneroResponseSubType subType, dynamic requestParams)
        {
            return subType switch
            {
                MoneroResponseSubType.DeleteAddressBook => new AnonymousRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "delete_address_book",
                    Params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new AnonymousRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "check_tx_key",
                    Params = requestParams,
                },
                MoneroResponseSubType.SubmitBlock => new AnonymousRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "submit_block",
                    Params = requestParams,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroWalletResponseSubType ({subType})"),
            };
        }

        private static CustomRequest GetRequest(MoneroResponseSubType subType, CustomRequestParameters customRequest)
        {
            return subType switch
            {
                MoneroResponseSubType.Transactions => new CustomRequest
                {
                    Endpoint = RequestEndpoint.Transactions,
                    Method = null,
                    Params = null,
                    Txs_hashes = customRequest.Txs_hashes,
                    Id = null,
                    Jsonrpc = null,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroDaemonResponseSubType ({subType})"),
            };
        }

        private static BaseRequest GetRequest(MoneroResponseSubType subType, GenericRequestParameters requestParams)
        {
            return subType switch
            {
                MoneroResponseSubType.BlockCount => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_block_count",
                    Params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHash => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_block_header_by_hash",
                    Params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHeight => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_block_header_by_height",
                    Params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRange => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_block_headers_range",
                    Params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRecency => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_last_block_header",
                    Params = requestParams,
                },
                MoneroResponseSubType.AllConnections => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_connections",
                    Params = requestParams,
                },
                MoneroResponseSubType.NodeInformation => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_info",
                    Params = requestParams,
                },
                MoneroResponseSubType.HardforkInformation => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "hard_fork_info",
                    Params = requestParams,
                },
                MoneroResponseSubType.BanInformation => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_bans",
                    Params = requestParams,
                },
                MoneroResponseSubType.FlushTransactionPool => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "flush_txpool",
                    Params = requestParams,
                },
                MoneroResponseSubType.OutputHistogram => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_output_histogram",
                    Params = requestParams,
                },
                MoneroResponseSubType.CoinbaseTransactionSum => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_coinbase_tx_sum",
                    Params = requestParams,
                },
                MoneroResponseSubType.FeeEstimate => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_fee_estimate",
                    Params = requestParams,
                },
                MoneroResponseSubType.AlternateChain => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_alternate_chains",
                    Params = requestParams,
                },
                MoneroResponseSubType.RelayTransaction => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "relay_tx",
                    Params = requestParams,
                },
                MoneroResponseSubType.SyncInformation => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sync_info",
                    Params = requestParams,
                },
                MoneroResponseSubType.Block => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_block",
                    Params = requestParams,
                },
                MoneroResponseSubType.SetBans => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "set_bans",
                    Params = requestParams,
                },
                MoneroResponseSubType.DaemonVersion => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_version",
                    Params = requestParams,
                },
                MoneroResponseSubType.Balance => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_balance",
                    Params = requestParams,
                },
                MoneroResponseSubType.Address => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_address",
                    Params = requestParams,
                },
                MoneroResponseSubType.AddressIndex => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_address_index",
                    Params = requestParams,
                },
                MoneroResponseSubType.AddressCreation => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "create_address",
                    Params = requestParams,
                },
                MoneroResponseSubType.AddressLabeling => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "label_address",
                    Params = requestParams,
                },
                MoneroResponseSubType.Account => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_accounts",
                    Params = requestParams,
                },
                MoneroResponseSubType.AccountCreation => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "create_account",
                    Params = requestParams,
                },
                MoneroResponseSubType.AccountLabeling => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "label_account",
                    Params = requestParams,
                },
                MoneroResponseSubType.AccountTags => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_account_tags",
                    Params = requestParams,
                },
                MoneroResponseSubType.AccountTagging => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "tag_accounts",
                    Params = requestParams,
                },
                MoneroResponseSubType.AccountUntagging => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "untag_accounts",
                    Params = requestParams,
                },
                MoneroResponseSubType.AccountTagAndDescriptionSetting => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "set_account_tag_description",
                    Params = requestParams,
                },
                MoneroResponseSubType.Height => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_height",
                    Params = requestParams,
                },
                MoneroResponseSubType.FundTransfer => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "transfer",
                    Params = requestParams,
                },
                MoneroResponseSubType.FundTransferSplit => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "transfer_split",
                    Params = requestParams,
                },
                MoneroResponseSubType.SignTransfer => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sign_transfer",
                    Params = requestParams,
                },
                MoneroResponseSubType.SweepDust => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sweep_dust",
                    Params = requestParams,
                },
                MoneroResponseSubType.SweepAll => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sweep_all",
                    Params = requestParams,
                },
                MoneroResponseSubType.SaveWallet => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "store",
                    Params = requestParams,
                },
                MoneroResponseSubType.StopWallet => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "stop_wallet",
                    Params = requestParams,
                },
                MoneroResponseSubType.IncomingTransfers => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "incoming_transfers",
                    Params = requestParams,
                },
                MoneroResponseSubType.QueryPrivateKey => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "query_key",
                    Params = requestParams,
                },
                MoneroResponseSubType.SetTransactionNotes => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "set_tx_notes",
                    Params = requestParams,
                },
                MoneroResponseSubType.GetTransactionNotes => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_tx_notes",
                    Params = requestParams,
                },
                MoneroResponseSubType.GetTransactionKey => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_tx_key",
                    Params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "check_tx_key",
                    Params = requestParams,
                },
                MoneroResponseSubType.Transfers => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_transfers",
                    Params = requestParams,
                },
                MoneroResponseSubType.TransferByTxid => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_transfer_by_txid",
                    Params = requestParams,
                },
                MoneroResponseSubType.Sign => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sign",
                    Params = requestParams,
                },
                MoneroResponseSubType.ExportOutputs => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "export_outputs",
                    Params = requestParams,
                },
                MoneroResponseSubType.ImportOutputs => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "import_outputs",
                    Params = requestParams,
                },
                MoneroResponseSubType.ExportKeyImages => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "export_key_images",
                    Params = requestParams,
                },
                MoneroResponseSubType.ImportKeyImages => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "import_key_images",
                    Params = requestParams,
                },
                MoneroResponseSubType.MakeUri => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "make_uri",
                    Params = requestParams,
                },
                MoneroResponseSubType.ParseUri => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "parse_uri",
                    Params = requestParams,
                },
                MoneroResponseSubType.GetAddressBook => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_address_book",
                    Params = requestParams,
                },
                MoneroResponseSubType.AddAddressBook => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "add_address_book",
                    Params = requestParams,
                },
                MoneroResponseSubType.DeleteAddressBook => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "delete_address_book",
                    Params = requestParams,
                },
                MoneroResponseSubType.Refresh => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "refresh",
                    Params = requestParams,
                },
                MoneroResponseSubType.RescanSpent => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "rescan_spent",
                    Params = requestParams,
                },
                MoneroResponseSubType.Languages => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_languages",
                    Params = requestParams,
                },
                MoneroResponseSubType.CreateWallet => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "create_wallet",
                    Params = requestParams,
                },
                MoneroResponseSubType.OpenWallet => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "open_wallet",
                    Params = requestParams,
                },
                MoneroResponseSubType.CloseWallet => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "close_wallet",
                    Params = requestParams,
                },
                MoneroResponseSubType.ChangeWalletPassword => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "change_wallet_password",
                    Params = requestParams,
                },
                MoneroResponseSubType.RpcVersion => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_version",
                    Params = requestParams,
                },
                MoneroResponseSubType.IsMultiSig => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "is_multisig",
                    Params = requestParams,
                },
                MoneroResponseSubType.PrepareMultiSig => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "prepare_multisig",
                    Params = requestParams,
                },
                MoneroResponseSubType.MakeMultiSig => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "make_multisig",
                    Params = requestParams,
                },
                MoneroResponseSubType.ExportMultiSigInfo => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "export_multisig_info",
                    Params = requestParams,
                },
                MoneroResponseSubType.ImportMultiSigInfo => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "import_multisig_info",
                    Params = requestParams,
                },
                MoneroResponseSubType.FinalizeMultiSig => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "finalize_multisig",
                    Params = requestParams,
                },
                MoneroResponseSubType.SignMultiSigTransaction => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sign_multisig",
                    Params = requestParams,
                },
                MoneroResponseSubType.SubmitMultiSigTransaction => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "submit_multisig",
                    Params = requestParams,
                },
                MoneroResponseSubType.DescribeTransfer => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "describe_transfer",
                    Params = requestParams,
                },
                MoneroResponseSubType.SweepSingle => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "sweep_single",
                    Params = requestParams,
                },
                MoneroResponseSubType.GetBlockTemplate => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_block_template",
                    Params = requestParams,
                },
                MoneroResponseSubType.GetBanStatus => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "banned",
                    Params = requestParams,
                },
                MoneroResponseSubType.PruneBlockchain => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "prune_blockchain",
                    Params = requestParams,
                },
                MoneroResponseSubType.GetAttribute => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "get_attribute",
                    Params = requestParams,
                },
                MoneroResponseSubType.ValidateAddress => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "validate_address",
                    Params = requestParams,
                },
                MoneroResponseSubType.SetAttribute => new BaseRequest
                {
                    Endpoint = RequestEndpoint.JsonRpc,
                    Method = "set_attribute",
                    Params = requestParams,
                },
                MoneroResponseSubType.TransactionPoolTransactions => new BaseRequest
                {
                    Endpoint = RequestEndpoint.TransactionPool,
                    Method = null,
                    Params = null,
                    Id = null,
                    Jsonrpc = null,
                },
                MoneroResponseSubType.Verify => throw new NotImplementedException("The Verify RPC Command is not implemented"),
                _ => throw new InvalidOperationException($"Unknown MoneroDaemonResponseSubType ({subType})"),
            };
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, CustomRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<CustomRequest>(ms, request, DefaultSerializationOptions, token).ConfigureAwait(false);
            httpRequestMessage.Content = new ByteArrayContent(ms.ToArray());
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, BaseRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<BaseRequest>(ms, request, DefaultSerializationOptions, token).ConfigureAwait(false);
            httpRequestMessage.Content = new ByteArrayContent(ms.ToArray());
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, AnonymousRequest anonymousRequest, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<AnonymousRequest>(ms, anonymousRequest, DefaultSerializationOptions, token).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }
    }
}