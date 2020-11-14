using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Daemon.POD.Requests;

namespace Monero.Client.Network
{
    internal class MoneroRequestAdapter
    {
        private readonly Uri _uri;

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
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "delete_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new AnonymousRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "check_tx_key",
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
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_count",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHash => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_header_by_hash",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByHeight => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_header_by_height",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRange => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block_headers_range",
                    @params = requestParams,
                },
                MoneroResponseSubType.BlockHeaderByRecency => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_last_block_header",
                    @params = requestParams,
                },
                MoneroResponseSubType.AllConnections => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_connections",
                    @params = requestParams,
                },
                MoneroResponseSubType.NodeInformation => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.HardforkInformation => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "hard_fork_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.BanInformation => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_bans",
                    @params = requestParams,
                },
                MoneroResponseSubType.FlushTransactionPool => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "flush_txpool",
                    @params = requestParams,
                },
                MoneroResponseSubType.OutputHistogram => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_output_histogram",
                    @params = requestParams,
                },
                MoneroResponseSubType.CoinbaseTransactionSum => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_coinbase_tx_sum",
                    @params = requestParams,
                },
                MoneroResponseSubType.FeeEstimate => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_fee_estimate",
                    @params = requestParams,
                },
                MoneroResponseSubType.AlternateChain => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_alternate_chains",
                    @params = requestParams,
                },
                MoneroResponseSubType.RelayTransaction => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "relay_tx",
                    @params = requestParams,
                },
                MoneroResponseSubType.SyncInformation => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sync_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.Block => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_block",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetBans => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "set_bans",
                    @params = requestParams,
                },
                MoneroResponseSubType.DaemonVersion => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.JsonRpc,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_version",
                    @params = requestParams,
                },
                MoneroResponseSubType.Balance => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_balance",
                    @params = requestParams,
                },
                MoneroResponseSubType.Address => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressIndex => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_address_index",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressCreation => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "create_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddressLabeling => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "label_address",
                    @params = requestParams,
                },
                MoneroResponseSubType.Account => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountCreation => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "create_account",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountLabeling => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "label_account",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTags => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_account_tags",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTagging => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "tag_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountUntagging => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "untag_accounts",
                    @params = requestParams,
                },
                MoneroResponseSubType.AccountTagAndDescriptionSetting => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "set_account_tag_description",
                    @params = requestParams,
                },
                MoneroResponseSubType.Height => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_height",
                    @params = requestParams,
                },
                MoneroResponseSubType.FundTransfer => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.FundTransferSplit => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "transfer_split",
                    @params = requestParams,
                },
                MoneroResponseSubType.SignTransfer => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sign_transfer",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepDust => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sweep_dust",
                    @params = requestParams,
                },
                MoneroResponseSubType.SweepAll => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sweep_dust",
                    @params = requestParams,
                },
                MoneroResponseSubType.SaveWallet => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "store",
                    @params = requestParams,
                },
                MoneroResponseSubType.StopWallet => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "stop_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.IncomingTransfers => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "incoming_transfers",
                    @params = requestParams,
                },
                MoneroResponseSubType.QueryPrivateKey => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "query_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.SetTransactionNotes => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "set_tx_notes",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetTransactionNotes => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_tx_notes",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetTransactionKey => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.CheckTransactionKey => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "check_tx_key",
                    @params = requestParams,
                },
                MoneroResponseSubType.Transfers => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_transfers",
                    @params = requestParams,
                },
                MoneroResponseSubType.TransferByTxid => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_transfer_by_txid",
                    @params = requestParams,
                },
                MoneroResponseSubType.Sign => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sign",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportOutputs => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "export_outputs",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportOutputs => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "import_outputs",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportKeyImages => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "export_key_images",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportKeyImages => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "import_key_images",
                    @params = requestParams,
                },
                MoneroResponseSubType.MakeUri => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "make_uri",
                    @params = requestParams,
                },
                MoneroResponseSubType.ParseUri => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "parse_uri",
                    @params = requestParams,
                },
                MoneroResponseSubType.GetAddressBook => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.AddAddressBook => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "add_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.DeleteAddressBook => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "delete_address_book",
                    @params = requestParams,
                },
                MoneroResponseSubType.Refresh => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "refresh",
                    @params = requestParams,
                },
                MoneroResponseSubType.RescanSpent => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "rescan_spent",
                    @params = requestParams,
                },
                MoneroResponseSubType.Languages => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_languages",
                    @params = requestParams,
                },
                MoneroResponseSubType.CreateWallet => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "create_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.OpenWallet => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "open_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.CloseWallet => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "close_wallet",
                    @params = requestParams,
                },
                MoneroResponseSubType.ChangeWalletPassword => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "change_wallet_password",
                    @params = requestParams,
                },
                MoneroResponseSubType.RpcVersion => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "get_version",
                    @params = requestParams,
                },
                MoneroResponseSubType.IsMultiSig => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "is_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.PrepareMultiSig => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "prepare_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.MakeMultiSig => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "make_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.ExportMultiSigInfo => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "export_multisig_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.ImportMultiSigInfo => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "import_multisig_info",
                    @params = requestParams,
                },
                MoneroResponseSubType.FinalizeMultiSig => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "finalize_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.SignMultiSigTransaction => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "sign_multisig",
                    @params = requestParams,
                },
                MoneroResponseSubType.SubmitMultiSigTransaction => new GenericRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "submit_multisig",
                    @params = requestParams,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroDaemonResponseSubType ({subType})"),
            };
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, GenericRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<GenericRequest>(ms, request, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, AnonymousRequest anonymousRequest, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<AnonymousRequest>(ms, anonymousRequest, new JsonSerializerOptions() { IgnoreNullValues = true, }, token).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }
    }
}