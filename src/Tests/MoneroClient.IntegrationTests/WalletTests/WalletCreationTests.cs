using Monero.Client.Enums;
using Monero.Client.Network;
using Monero.Client.Wallet;
using MoneroClient.IntegrationTests.BaseClasses;
using MoneroClient.IntegrationTests.Constants;
using MoneroClient.IntegrationTests.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MoneroClient.IntegrationTests.WalletTests
{
    public class MoneroWalletClientTests : MoneroBackgroundProcesses
    {
        [Fact]
        public async Task CreateNewWalletAsync_ValidWalletInfo_ReturnsCorrectMnemonic()
        {
            var fileName = "test_" + Guid.NewGuid().ToString();
            var walletPassword = "password123";
            await MoneroWalletClient.CreateNewWalletAsync(
                    TestingConstants.DefaultHost,
                    TestingConstants.DefaultTestNetPort,
                    fileName,
                    walletPassword,
                    TestingConstants.DefaultLanguage);
            var walletClient = await MoneroWalletClient.CreateAsync(
                    TestingConstants.DefaultHost,
                    TestingConstants.DefaultTestNetPort,
                    fileName,
                    walletPassword);
            var mnemonicPhrase = await walletClient.GetPrivateKey(KeyType.Mnemonic);
            var words = mnemonicPhrase.Split(' ');

            Assert.Equal(25, words.Length);
        }

        [Fact]
        public async Task GetAccountsAsync_WalletWithFunds_HasBalanceGreaterThanZero()
        {
            var fileName = "test_0b395a87-583b-41bb-a7dc-1eed1ba47ef3";
            var walletPassword = "password123";
            var walletClient = await MoneroWalletClient.CreateAsync(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort,
                fileName,
                walletPassword);
 
            var accounts = await walletClient.GetAccountsAsync();

            Assert.True(accounts.TotalBalance > 0);
        }

        [Fact]
        public async Task TransferAsync_WalletWithFundsTriesToSend_HasReducedBalanceAfterSending()
        {
            var fileName = "test_0b395a87-583b-41bb-a7dc-1eed1ba47ef3";
            var walletPassword = "password123";
            var walletClient = await MoneroWalletClient.CreateAsync(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort,
                fileName,
                walletPassword);

            var addressWithAmount = new List<(string address, ulong amount)>()
            {
                ("9vTA787Xnw8Uxa4LoNwAR4MZmhtDymQCU6EZ3q1g6yCRBv62UKuN9PG2CX4sotUehjBfAY487fL2eJQoaPzStnF5SxEzcYi",
                10000000),
            };

            var balanceBeforeSending = await walletClient.GetBalanceAsync(0);

            await walletClient.TransferAsync(
                addressWithAmount,
                TransferPriority.Unimportant).ConfigureAwait(false);

            var balanceAfterSending = await walletClient.GetBalanceAsync(0);

            Assert.True(balanceBeforeSending.TotalBalance > balanceAfterSending.TotalBalance);
        }

        [Fact]
        public async Task MoneroWalletClient_ValidTestNetAddres_ReturnsValid()
        {
            var fileName = "test_0b395a87-583b-41bb-a7dc-1eed1ba47ef3";
            var walletPassword = "password123";
            var walletClient = await MoneroWalletClient.CreateAsync(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort,
                fileName,
                walletPassword);

            var validationResult = await walletClient.ValidateAddressAsync("9vTA787Xnw8Uxa4LoNwAR4MZmhtDymQCU6EZ3q1g6yCRBv62UKuN9PG2CX4sotUehjBfAY487fL2eJQoaPzStnF5SxEzcYi");
            
            Assert.True(validationResult.Valid);
        }

        
        [Fact(Skip = "not done")]
        public async Task ValidateAddressAsync_ValidTestNetAddres_ReturnsValid_skip()
        {
            BackgroundProcessUtility.Start();

            var fileName = "test_e05d308b-cf8f-4570-a6ed-c48e3b036b02";// "test_" + Guid.NewGuid().ToString();
            var walletPassword = "password123";
            //await MoneroWalletClient.CreateNewWalletAsync(
            //        TestingConstants.DefaultHost,
            //        18082,
            //        fileName,
            //        walletPassword,
            //        TestingConstants.DefaultLanguage);

            var walletClient = await MoneroWalletClient.CreateAsync(MoneroNetwork.Mainnet, fileName, walletPassword);
            var validationResult = await walletClient.ValidateAddressAsync("42go2d3XqA9Mx4HjZoqr93BHspcMxwAUBivs3yJKV1FyTycEcbgjNyEaGNEcgnUE9DDDAXNanzB16YgMt88Sa8cFSm2QcHK");


            BackgroundProcessUtility.Stop();

            Assert.True(validationResult.Valid);
        }
    }
}
