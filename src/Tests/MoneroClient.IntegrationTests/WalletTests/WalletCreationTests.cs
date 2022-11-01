using Monero.Client.Daemon;
using Monero.Client.Network;
using Monero.Client.Wallet;
using Monero.Client.Wallet.POD;
using MoneroClient.IntegrationTests.Constants;
using MoneroClient.IntegrationTests.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MoneroClient.IntegrationTests.WalletTests
{
    public class WalletCreationTests
    {
        [Fact]
        public async Task MoneroWalletClient_CreateNewWalletAndGetMnemonicPhrase_ReturnsCorrectMnemonic()
        {
            BackgroundProcessUtility.Start();

            var guid = "test_" + Guid.NewGuid().ToString();
            var walletPassword = "password123";
            await MoneroWalletClient.CreateNewWalletAsync(
                    TestingConstants.DefaultHost,
                    TestingConstants.DefaultPort,
                    guid,
                    walletPassword,
                    TestingConstants.DefaultLanguage);
            var walletClient = await MoneroWalletClient.CreateAsync(
                    TestingConstants.DefaultHost,
                    TestingConstants.DefaultPort,
                    guid,
                    walletPassword);
            var mnemonicPhrase = await walletClient.GetPrivateKey(KeyType.Mnemonic);
            var words = mnemonicPhrase.Split(' ');

            BackgroundProcessUtility.Stop();

            Assert.Equal(25, words.Length);
        }
    }
}
