using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Monero.Client.Network
{
    internal class MoneroRequestAdapter
    {
        private readonly string _url;
        private readonly uint _port;
        private static readonly JsonSerializerOptions _defaultSerializationOptions = new JsonSerializerOptions() { IgnoreNullValues = true, };

        public MoneroRequestAdapter(string url, uint port)
        {
            _url = url;
            _port = port;
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, CustomRequestParameters requestParams, CancellationToken token)
        {
            if (requestParams == null)
                return GetRequestMessage(subType, new GenericRequestParameters(), token);
            var request = GetRequest(subType, requestParams);
            IUriBuilder uriBuilder = new UriBuilderDirector(new UriBuilder(_url, _port, RequestEndpointExtensionRetriever.FetchEndpoint(request)));
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Build());
            return SerializeRequest(httpRequestMessage, request, token);
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, GenericRequestParameters requestParams, CancellationToken token)
        {
            var request = GetRequest(subType, requestParams);
            IUriBuilder uriBuilder = new UriBuilderDirector(new UriBuilder(_url, _port, RequestEndpointExtensionRetriever.FetchEndpoint(request)));
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Build());
            return SerializeRequest(httpRequestMessage, request, token);
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, dynamic requestParams, CancellationToken token)
        {
            AnonymousRequest request = GetRequest(subType, requestParams);
            IUriBuilder uriBuilder = new UriBuilderDirector(new UriBuilder(_url, _port, RequestEndpointExtensionRetriever.FetchEndpoint(request)));
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Build());
            return SerializeRequest(httpRequestMessage, request, token);
        }

        private static AnonymousRequest GetRequest(MoneroResponseSubType subType, dynamic requestParams)
        {
            return subType switch
            {
                MoneroResponseSubType.DeleteAddressBook => new AnonymousRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "delete_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new AnonymousRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "check_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.SubmitBlock => new AnonymousRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "submit_block",
                    @params = requestParams,
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
                    endpoint = RequestEndpoint.Transactions,
                    method = null,
                    @params = null,
                    txs_hashes = customRequest.txs_hashes,
                    id = null,
                    jsonrpc = null,
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
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_block_count",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHash => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_block_header_by_hash",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHeight => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_block_header_by_height",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRange => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_block_headers_range",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRecency => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_last_block_header",
                    @params = requestParams,
                },
                MoneroResponseSubType.AllConnections => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_connections",
                    @params = requestParams,
                },
                MoneroResponseSubType.NodeInformation => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.HardforkInformation => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "hard_fork_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.BanInformation => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_bans",
                    @params = requestParams,
                },
                MoneroResponseSubType.FlushTransactionPool => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "flush_txpool",
                    @params = requestParams,
                },
                MoneroResponseSubType.OutputHistogram => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_output_histogram",
                    @params = requestParams,
                },
                MoneroResponseSubType.CoinbaseTransactionSum => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_coinbase_tx_sum",
                    @params = requestParams,
                },
                MoneroResponseSubType.FeeEstimate => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_fee_estimate",
                    @params = requestParams,
                },
                MoneroResponseSubType.AlternateChain => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_alternate_chains",
                    @params = requestParams,
                },
                MoneroResponseSubType.RelayTransaction => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "relay_tx",
                    @params = requestParams,
                },
                MoneroResponseSubType.SyncInformation => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sync_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.Block => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_block",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetBans => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "set_bans",
                    @params = requestParams,
                },
                MoneroResponseSubType.DaemonVersion => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_version",
                    @params = requestParams,
                },
                MoneroResponseSubType.Balance => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_balance",
                    @params = requestParams,
                },
                MoneroResponseSubType.Address => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressIndex => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_address_index",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressCreation => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "create_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressLabeling => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "label_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.Account => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountCreation => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "create_account",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountLabeling => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "label_account",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTags => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_account_tags",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTagging => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "tag_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountUntagging => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "untag_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTagAndDescriptionSetting => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "set_account_tag_description",
                    @params = requestParams,
                },
                MoneroResponseSubType.Height => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_height",
                    @params = requestParams,
                },
                MoneroResponseSubType.FundTransfer => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.FundTransferSplit => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "transfer_split",
                    @params = requestParams,
                },
                MoneroResponseSubType.SignTransfer => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sign_transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepDust => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sweep_dust",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepAll => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sweep_all",
                    @params = requestParams,
                },
                MoneroResponseSubType.SaveWallet => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "store",
                    @params = requestParams,
                },
                MoneroResponseSubType.StopWallet => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "stop_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.IncomingTransfers => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "incoming_transfers",
                    @params = requestParams,
                },
                MoneroResponseSubType.QueryPrivateKey => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "query_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetTransactionNotes => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "set_tx_notes",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetTransactionNotes => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_tx_notes",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetTransactionKey => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "check_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.Transfers => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_transfers",
                    @params = requestParams,
                },
                MoneroResponseSubType.TransferByTxid => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_transfer_by_txid",
                    @params = requestParams,
                },
                MoneroResponseSubType.Sign => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sign",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportOutputs => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "export_outputs",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportOutputs => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "import_outputs",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportKeyImages => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "export_key_images",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportKeyImages => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "import_key_images",
                    @params = requestParams,
                },
                MoneroResponseSubType.MakeUri => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "make_uri",
                    @params = requestParams,
                },
                MoneroResponseSubType.ParseUri => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "parse_uri",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetAddressBook => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddAddressBook => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "add_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.DeleteAddressBook => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "delete_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.Refresh => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "refresh",
                    @params = requestParams,
                },
                MoneroResponseSubType.RescanSpent => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "rescan_spent",
                    @params = requestParams,
                },
                MoneroResponseSubType.Languages => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_languages",
                    @params = requestParams,
                },
                MoneroResponseSubType.CreateWallet => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "create_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.OpenWallet => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "open_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.CloseWallet => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "close_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.ChangeWalletPassword => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "change_wallet_password",
                    @params = requestParams,
                },
                MoneroResponseSubType.RpcVersion => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_version",
                    @params = requestParams,
                },
                MoneroResponseSubType.IsMultiSig => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "is_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.PrepareMultiSig => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "prepare_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.MakeMultiSig => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "make_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportMultiSigInfo => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "export_multisig_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportMultiSigInfo => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "import_multisig_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.FinalizeMultiSig => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "finalize_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.SignMultiSigTransaction => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sign_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.SubmitMultiSigTransaction => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "submit_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.DescribeTransfer => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "describe_transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepSingle => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "sweep_single",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetBlockTemplate => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_block_template",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetBanStatus => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "banned",
                    @params = requestParams,
                },
                MoneroResponseSubType.PruneBlockchain => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "prune_blockchain",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetAttribute => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "get_attribute",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetAttribute => new BaseRequest
                {
                    endpoint = RequestEndpoint.JsonRpc,
                    method = "set_attribute",
                    @params = requestParams,
                },
                MoneroResponseSubType.TransactionPoolTransactions => new BaseRequest
                {
                    endpoint = RequestEndpoint.TransactionPool,
                    method = null,
                    @params = null,
                    id = null,
                    jsonrpc = null,
                },
                MoneroResponseSubType.Verify => throw new NotImplementedException("The Verify RPC Command is not implemented"),
                _ => throw new InvalidOperationException($"Unknown MoneroDaemonResponseSubType ({subType})"),
            };
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, CustomRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<CustomRequest>(ms, request, _defaultSerializationOptions, token).ConfigureAwait(false);
            httpRequestMessage.Content = new ByteArrayContent(ms.ToArray());
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, BaseRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<BaseRequest>(ms, request, _defaultSerializationOptions, token).ConfigureAwait(false);
            httpRequestMessage.Content = new ByteArrayContent(ms.ToArray());
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, AnonymousRequest anonymousRequest, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<AnonymousRequest>(ms, anonymousRequest, _defaultSerializationOptions, token).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }
    }
}