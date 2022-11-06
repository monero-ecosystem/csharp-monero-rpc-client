using Monero.Client.Enums;
using Monero.Client.Network;
using Monero.Client.Wallet;
using Monero.Client.Wallet.POD.Responses;
using MoneroClient.IntegrationTests.BaseClasses;
using MoneroClient.IntegrationTests.Constants;
using MoneroClient.IntegrationTests.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MoneroClient.IntegrationTests.WalletTests
{
    public class MoneroWalletClientTests : MoneroBackgroundProcesses
    {
        [Fact]
        public async Task CreateNewWalletAsync_ValidWalletInfo_ReturnsCorrectMnemonic()
        {
            // Arrange
            var fileName = "test_" + Guid.NewGuid().ToString();
            var walletPassword = "password123";

            // Act
            var walletClient = new MoneroWalletClient(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort);
            await walletClient.CreateWalletAsync(
                    fileName,
                    TestingConstants.DefaultLanguage,
                    walletPassword);
            var mnemonicPhrase = await walletClient.GetPrivateKey(KeyType.Mnemonic);
            var words = mnemonicPhrase.Split(' ');

            // Assert
            Assert.Equal(25, words.Length);
            Assert.True(true);
        }

        [Fact]
        public async Task GetAccountsAsync_WalletWithFunds_HasBalanceGreaterThanZero()
        {
            // Arrange
            var fileName = "test_0b395a87-583b-41bb-a7dc-1eed1ba47ef3";
            var walletPassword = "password123";

            // Act
            var walletClient = await MoneroWalletClient.CreateAsync(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort,
                fileName,
                walletPassword);
            var accounts = await walletClient.GetAccountsAsync();

            // Assert
            Assert.True(accounts.TotalBalance > 0);
        }

        [Fact]
        public async Task TransferAsync_WalletWithFundsTriesToSend_HasReducedBalanceAfterSending()
        {
            // Arrange
            var fileName = "test_0b395a87-583b-41bb-a7dc-1eed1ba47ef3";
            var walletPassword = "password123";
            var addressWithAmount = new List<(string address, ulong amount)>()
            {
                ("9vTA787Xnw8Uxa4LoNwAR4MZmhtDymQCU6EZ3q1g6yCRBv62UKuN9PG2CX4sotUehjBfAY487fL2eJQoaPzStnF5SxEzcYi",
                10000000),
            };

            // Act
            var walletClient = await MoneroWalletClient.CreateAsync(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort,
                fileName,
                walletPassword);
            var balanceBeforeSending = await walletClient.GetBalanceAsync(0);
            await walletClient.TransferAsync(
                addressWithAmount,
                TransferPriority.Unimportant).ConfigureAwait(false);
            var balanceAfterSending = await walletClient.GetBalanceAsync(0);

            // Assert
            Assert.True(balanceBeforeSending.TotalBalance > balanceAfterSending.TotalBalance);
        }

        [Fact]
        public async Task MoneroWalletClient_ValidTestNetAddres_ReturnsValid()
        {
            // Arrange
            var fileName = "test_0b395a87-583b-41bb-a7dc-1eed1ba47ef3";
            var walletPassword = "password123";
            
            // Act
            var walletClient = await MoneroWalletClient.CreateAsync(
                TestingConstants.DefaultHost,
                TestingConstants.DefaultTestNetPort,
                fileName,
                walletPassword);
            var validationResult = await walletClient.ValidateAddressAsync("9vTA787Xnw8Uxa4LoNwAR4MZmhtDymQCU6EZ3q1g6yCRBv62UKuN9PG2CX4sotUehjBfAY487fL2eJQoaPzStnF5SxEzcYi");

            // Assert
            Assert.True(validationResult.Valid);
        }
    }
}
