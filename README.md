![](gif/resized.gif)

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="https://camo.githubusercontent.com/83d3746e5881c1867665223424263d8e604df233d0a11aae0813e0414d433943/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f6c6963656e73652d4d49542d626c75652e737667" alt="License" data-canonical-src="https://img.shields.io/badge/license-MIT-blue.svg" style="max-width:100%;"> [![Build Status](https://travis-ci.com/Agorist-Action/csharp-monero-rpc-client.svg?branch=master)](https://travis-ci.com/Agorist-Action/csharp-monero-rpc-client) <img src="https://camo.githubusercontent.com/7e7bdf5c529c8bc594e26038dbb1a3d360e9ede891fbdcef50b403ab5f88fc14/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f636f6e747269627574696f6e732d77656c636f6d652d6f72616e67652e737667" alt="Contributions welcome" data-canonical-src="https://img.shields.io/badge/contributions-welcome-orange.svg" style="max-width:100%;"> [![HitCount](http://hits.dwyl.com/Agorist-Action/csharp-monero-rpc-client.svg)](http://hits.dwyl.com/Agorist-Action/csharp-monero-rpc-client)

This is the source code of a Monero JSON-RPC client (for both daemon and wallet) built on .netstandard2.1.

# Basic Overview
Both a daemon client and wallet client are available. The daemon client interacts with `monerod.exe`, and the wallet client interacts with `monero-wallet-rpc.exe`
## MoneroDaemonClient
**Initialize Client**
```
using Monero.Client.Daemon;
var daemonClient = new MoneroDaemonClient(new Uri("http://127.0.0.1:18082/json_rpc"));
```
**Get Connections**
```
var token = CancellationToken();
var response = await daemonClient.GetConnectionsAsync(token).ConfigureAwait(false);
```
For the entire MoneroDaemonClient interface, please click [here](https://github.com/Agorist-Action/csharp-monero-rpc-client/blob/master/Daemon/IMoneroDaemonClient.cs).
## MoneroWalletClient
**Initialize Client**
```
using Monero.Client.Network;
using Monero.Client.Wallet;
using Monero.Client.Wallet.POD;

var moneroWalletClient = new MoneroWalletClient(MoneroNetwork.Testnet);
```
**Open Wallet**
```
await moneroWalletClient.OpenWalletAsync("new_wallet3", "banana").ConfigureAwait(false);
```
**Transfer Funds**
```
var cts = new CancellationTokenSource();
var dA = new List<(string address, ulong amount)>() 
{ 
	("BfukYd1Dv5YDgkZDhffjmHb1SfzT7Wr1HNTYkyxEmfnXiGepCHgPiaWicRCLHpM2moVNWAxNEVKogU2w58fT", 1000ul),
	("SomeOtherMoneroAddress", 3233100ul),
};
var response = await moneroWalletClient.TransferAsync(dA, TransferPriority.Normal, cts.token).ConfigureAwait(false);
```
For the entire MoneroWalletClient interface, please click [here](https://github.com/Agorist-Action/csharp-monero-rpc-client/blob/master/Wallet/IMoneroWalletClient.cs).
**Note:** Unlike the Daemon Client, to perform any action with the Wallet Client, one must first either create a new wallet, or open an existing one (as shown above).
# Latest Stable Release
Available on Nuget [here](https://www.nuget.org/packages/Monero.Client/).
```
Install-Package Monero.Client -Version 1.0.1.0
```
# Latest Development Changes
```
git clone https://github.com/Agorist-Action/csharp-monero-rpc-client
```
# Contributing
All contributions are welcome. Please make sure your pull request include:

 - A detailed description of the issue.
 - A detailed description of your solution.
 - Make sure your commit messages are descriptive.

 # Donation
 If you found this library helpful, donations are appreciated.
89CkXKw3MQLXAinJz2eb8ohmGdDasGxun65ArenNuqXFfDSVbhiqpte4E2PQaxT4yPbsNSXkT4hR2QMFYQneZfBoCX19Wx2

![](gif/Donations.png)
