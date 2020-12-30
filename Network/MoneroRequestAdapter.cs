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
        private readonly Uri _uri;
        private static readonly JsonSerializerOptions _defaultSerializationOptions = new JsonSerializerOptions() { IgnoreNullValues = true, };

        public MoneroRequestAdapter(Uri uri)
        {
            _uri = uri;
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, GenericRequestParameters requestParams, CancellationToken token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _uri);
            var request = GetRequest(subType, requestParams);
            return SerializeRequest(httpRequestMessage, request, token);
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroResponseSubType subType, dynamic requestParams, CancellationToken token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _uri);
            AnonymousRequest request = GetRequest(subType, requestParams);
            return SerializeRequest(httpRequestMessage, request, token);
        }

        private static AnonymousRequest GetRequest(MoneroResponseSubType subType, dynamic requestParams)
        {
            return subType switch
            {
                MoneroResponseSubType.DeleteAddressBook => new AnonymousRequest
                {
                    method = "delete_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new AnonymousRequest
                {
                    method = "check_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.SubmitBlock => new AnonymousRequest
                {
                    method = "submit_block",
                    @params = requestParams,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroWalletResponseSubType ({subType})"),
            };
        }

        private static GenericRequest GetRequest(MoneroResponseSubType subType, GenericRequestParameters requestParams)
        {
            return subType switch
            {
                MoneroResponseSubType.BlockCount => new GenericRequest
                {
                    method = "get_block_count",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHash => new GenericRequest
                {
                    method = "get_block_header_by_hash",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHeight => new GenericRequest
                {
                    method = "get_block_header_by_height",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRange => new GenericRequest
                {
                    method = "get_block_headers_range",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRecency => new GenericRequest
                {
                    method = "get_last_block_header",
                    @params = requestParams,
                },
                MoneroResponseSubType.AllConnections => new GenericRequest
                {
                    method = "get_connections",
                    @params = requestParams,
                },
                MoneroResponseSubType.NodeInformation => new GenericRequest
                {
                    method = "get_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.HardforkInformation => new GenericRequest
                {
                    method = "hard_fork_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.BanInformation => new GenericRequest
                {
                    method = "get_bans",
                    @params = requestParams,
                },
                MoneroResponseSubType.FlushTransactionPool => new GenericRequest
                {
                    method = "flush_txpool",
                    @params = requestParams,
                },
                MoneroResponseSubType.OutputHistogram => new GenericRequest
                {
                    method = "get_output_histogram",
                    @params = requestParams,
                },
                MoneroResponseSubType.CoinbaseTransactionSum => new GenericRequest
                {
                    method = "get_coinbase_tx_sum",
                    @params = requestParams,
                },
                MoneroResponseSubType.FeeEstimate => new GenericRequest
                {
                    method = "get_fee_estimate",
                    @params = requestParams,
                },
                MoneroResponseSubType.AlternateChain => new GenericRequest
                {
                    method = "get_alternate_chains",
                    @params = requestParams,
                },
                MoneroResponseSubType.RelayTransaction => new GenericRequest
                {
                    method = "relay_tx",
                    @params = requestParams,
                },
                MoneroResponseSubType.SyncInformation => new GenericRequest
                {
                    method = "sync_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.Block => new GenericRequest
                {
                    method = "get_block",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetBans => new GenericRequest
                {
                    method = "set_bans",
                    @params = requestParams,
                },
                MoneroResponseSubType.DaemonVersion => new GenericRequest
                {
                    method = "get_version",
                    @params = requestParams,
                },
                MoneroResponseSubType.Balance => new GenericRequest
                {
                    method = "get_balance",
                    @params = requestParams,
                },
                MoneroResponseSubType.Address => new GenericRequest
                {
                    method = "get_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressIndex => new GenericRequest
                {
                    method = "get_address_index",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressCreation => new GenericRequest
                {
                    method = "create_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressLabeling => new GenericRequest
                {
                    method = "label_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.Account => new GenericRequest
                {
                    method = "get_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountCreation => new GenericRequest
                {
                    method = "create_account",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountLabeling => new GenericRequest
                {
                    method = "label_account",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTags => new GenericRequest
                {
                    method = "get_account_tags",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTagging => new GenericRequest
                {
                    method = "tag_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountUntagging => new GenericRequest
                {
                    method = "untag_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTagAndDescriptionSetting => new GenericRequest
                {
                    method = "set_account_tag_description",
                    @params = requestParams,
                },
                MoneroResponseSubType.Height => new GenericRequest
                {
                    method = "get_height",
                    @params = requestParams,
                },
                MoneroResponseSubType.FundTransfer => new GenericRequest
                {
                    method = "transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.FundTransferSplit => new GenericRequest
                {
                    method = "transfer_split",
                    @params = requestParams,
                },
                MoneroResponseSubType.SignTransfer => new GenericRequest
                {
                    method = "sign_transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepDust => new GenericRequest
                {
                    method = "sweep_dust",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepAll => new GenericRequest
                {
                    method = "sweep_dust",
                    @params = requestParams,
                },
                MoneroResponseSubType.SaveWallet => new GenericRequest
                {
                    method = "store",
                    @params = requestParams,
                },
                MoneroResponseSubType.StopWallet => new GenericRequest
                {
                    method = "stop_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.IncomingTransfers => new GenericRequest
                {
                    method = "incoming_transfers",
                    @params = requestParams,
                },
                MoneroResponseSubType.QueryPrivateKey => new GenericRequest
                {
                    method = "query_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetTransactionNotes => new GenericRequest
                {
                    method = "set_tx_notes",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetTransactionNotes => new GenericRequest
                {
                    method = "get_tx_notes",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetTransactionKey => new GenericRequest
                {
                    method = "get_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new GenericRequest
                {
                    method = "check_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.Transfers => new GenericRequest
                {
                    method = "get_transfers",
                    @params = requestParams,
                },
                MoneroResponseSubType.TransferByTxid => new GenericRequest
                {
                    method = "get_transfer_by_txid",
                    @params = requestParams,
                },
                MoneroResponseSubType.Sign => new GenericRequest
                {
                    method = "sign",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportOutputs => new GenericRequest
                {
                    method = "export_outputs",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportOutputs => new GenericRequest
                {
                    method = "import_outputs",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportKeyImages => new GenericRequest
                {
                    method = "export_key_images",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportKeyImages => new GenericRequest
                {
                    method = "import_key_images",
                    @params = requestParams,
                },
                MoneroResponseSubType.MakeUri => new GenericRequest
                {
                    method = "make_uri",
                    @params = requestParams,
                },
                MoneroResponseSubType.ParseUri => new GenericRequest
                {
                    method = "parse_uri",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetAddressBook => new GenericRequest
                {
                    method = "get_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddAddressBook => new GenericRequest
                {
                    method = "add_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.DeleteAddressBook => new GenericRequest
                {
                    method = "delete_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.Refresh => new GenericRequest
                {
                    method = "refresh",
                    @params = requestParams,
                },
                MoneroResponseSubType.RescanSpent => new GenericRequest
                {
                    method = "rescan_spent",
                    @params = requestParams,
                },
                MoneroResponseSubType.Languages => new GenericRequest
                {
                    method = "get_languages",
                    @params = requestParams,
                },
                MoneroResponseSubType.CreateWallet => new GenericRequest
                {
                    method = "create_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.OpenWallet => new GenericRequest
                {
                    method = "open_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.CloseWallet => new GenericRequest
                {
                    method = "close_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.ChangeWalletPassword => new GenericRequest
                {
                    method = "change_wallet_password",
                    @params = requestParams,
                },
                MoneroResponseSubType.RpcVersion => new GenericRequest
                {
                    method = "get_version",
                    @params = requestParams,
                },
                MoneroResponseSubType.IsMultiSig => new GenericRequest
                {
                    method = "is_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.PrepareMultiSig => new GenericRequest
                {
                    method = "prepare_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.MakeMultiSig => new GenericRequest
                {
                    method = "make_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportMultiSigInfo => new GenericRequest
                {
                    method = "export_multisig_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportMultiSigInfo => new GenericRequest
                {
                    method = "import_multisig_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.FinalizeMultiSig => new GenericRequest
                {
                    method = "finalize_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.SignMultiSigTransaction => new GenericRequest
                {
                    method = "sign_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.SubmitMultiSigTransaction => new GenericRequest
                {
                    method = "submit_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.DescribeTransfer => new GenericRequest
                {
                    method = "describe_transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepSingle => new GenericRequest
                {
                    method = "sweep_single",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetBlockTemplate => new GenericRequest
                {
                    method = "get_block_template",
                    @params = requestParams,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroDaemonResponseSubType ({subType})"),
            };
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, GenericRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<GenericRequest>(ms, request, _defaultSerializationOptions, token).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
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