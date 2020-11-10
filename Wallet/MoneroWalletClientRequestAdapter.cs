using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Network;
using Monero.Client.Wallet.POD.Requests;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    internal class MoneroWalletClientRequestAdapter
    {
        private readonly Uri _uri;

        public MoneroWalletClientRequestAdapter(Uri uri)
        {
            _uri = uri;
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroWalletResponseSubType subType, WalletRequestParameters requestParams, CancellationToken token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _uri);
            WalletRequest request = GetRequest(subType, requestParams);
            return SerializeRequest(httpRequestMessage, request, token);
        }

        public Task<HttpRequestMessage> GetRequestMessage(MoneroWalletResponseSubType subType, dynamic requestParams, CancellationToken token)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _uri);
            AnonymousWalletRequest request = GetRequest(subType, requestParams);
            return SerializeRequest(httpRequestMessage, request, token);
        }

        private static AnonymousWalletRequest GetRequest(MoneroWalletResponseSubType subType, dynamic requestParams)
        {
            return subType switch
            {
                MoneroWalletResponseSubType.DeleteAddressBook => new AnonymousWalletRequest
                {
                    jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                    id = FieldAndHeaderDefaults.Id,
                    method = "delete_address_book",
                    @params = requestParams,
                },
                _ => throw new InvalidOperationException($"Unknown MoneroWalletResponseSubType ({subType})"),
            };
        }

        private static WalletRequest GetRequest(MoneroWalletResponseSubType subType, WalletRequestParameters requestParams)
        {
             return subType switch
             {
                 MoneroWalletResponseSubType.Balance => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_balance",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Address => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_address",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AddressIndex => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_address_index",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AddressCreation => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "create_address",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AddressLabeling => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "label_address",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Account => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_accounts",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AccountCreation => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "create_account",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AccountLabeling => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "label_account",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AccountTags => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_account_tags",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AccountTagging => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "tag_accounts",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AccountUntagging => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "untag_accounts",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AccountTagAndDescriptionSetting => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "set_account_tag_description",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Height => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_height",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.FundTransfer => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "transfer",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.FundTransferSplit => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "transfer_split",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SignTransfer => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "sign_transfer",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SweepDust => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "sweep_dust",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SweepAll => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "sweep_dust",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SaveWallet => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "store",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.StopWallet => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "stop_wallet",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.IncomingTransfers => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "incoming_transfers",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.QueryPrivateKey => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "query_key",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SetTransactionNotes => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "set_tx_notes",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.GetTransactionNotes => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_tx_notes",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.GetTransactionKey => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_tx_key",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.CheckTransactionKey => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "check_tx_key",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Transfers => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_transfers",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.TransferByTxid => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_transfer_by_txid",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Sign => new WalletRequest 
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "sign",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ExportOutputs => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "export_outputs",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ImportOutputs => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "import_outputs",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ExportKeyImages => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "export_key_images",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ImportKeyImages => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "import_key_images",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.MakeUri => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "make_uri",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ParseUri => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "parse_uri",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.GetAddressBook => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_address_book",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.AddAddressBook => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "add_address_book",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.DeleteAddressBook => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "delete_address_book",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Refresh => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "refresh",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.RescanSpent => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "rescan_spent",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.Languages => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_languages",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.CreateWallet => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "create_wallet",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.OpenWallet => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "open_wallet",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.CloseWallet => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "close_wallet",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ChangeWalletPassword => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "change_wallet_password",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.RpcVersion => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "get_version",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.IsMultiSig => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "is_multisig",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.PrepareMultiSig => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "prepare_multisig",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.MakeMultiSig => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "make_multisig",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ExportMultiSigInfo => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "export_multisig_info",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.ImportMultiSigInfo => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "import_multisig_info",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.FinalizeMultiSig => new WalletRequest 
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "finalize_multisig",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SignMultiSigTransaction => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "sign_multisig",
                     @params = requestParams,
                 },
                 MoneroWalletResponseSubType.SubmitMultiSigTransaction => new WalletRequest
                 {
                     jsonrpc = FieldAndHeaderDefaults.ApplicationJson,
                     id = FieldAndHeaderDefaults.Id,
                     method = "submit_multisig",
                     @params = requestParams,
                 },
                 _ => throw new InvalidOperationException($"Unknown MoneroWalletResponseSubType ({subType})"),
             };
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, AnonymousWalletRequest anonymousRequest, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<AnonymousWalletRequest>(ms, anonymousRequest, new JsonSerializerOptions() { IgnoreNullValues = true, }).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            return httpRequestMessage;
        }

        private static async Task<HttpRequestMessage> SerializeRequest(HttpRequestMessage httpRequestMessage, WalletRequest request, CancellationToken token)
        {
            using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync<WalletRequest>(ms, request, new JsonSerializerOptions() { IgnoreNullValues = true, }).ConfigureAwait(false);
            var messageContent = ms.ToArray();
            httpRequestMessage.Content = new ByteArrayContent(messageContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(FieldAndHeaderDefaults.ApplicationJson);
            httpRequestMessage.Content.Headers.ContentType.CharSet = FieldAndHeaderDefaults.CharsetUtf16;
            return httpRequestMessage;
        }
    }
}