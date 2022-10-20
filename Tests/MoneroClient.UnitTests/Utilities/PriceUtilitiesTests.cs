using Monero.Client.Utilities;
using System;
using Xunit;

namespace MoneroClient.UnitTests.Utilities
{
    public class PriceUtilitiesTests
    {
        [Fact]
        public void MoneroToPiconero_NegativeMonero_Throws()
        {
            decimal monero = -1;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<InvalidOperationException>(() => GetPiconero());
        }

        [Fact]
        public void MoneroToPiconero_MoreThan12DecimalPlaces_Throws()
        {
            decimal monero = 1.000000000000123M;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<InvalidOperationException>(GetPiconero);
        }

        [Theory]
        [InlineData(0, 0ul)]
        [InlineData(0.000000000001, 1)]
        [InlineData(1, 1000000000000)]
        [InlineData(0.5, 500000000000)]
        [InlineData(1.5, 1500000000000)]
        public void MoneroToPiconero_ValidPiconeroAmounts_ReturnCorrectPiconero(decimal monero, ulong expectedPiconero)
        {
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(expectedPiconero, piconero);
        }

        [Fact]
        public void MoneroToPiconero_OneThirteenthAMonero_Throws()
        {
            decimal monero = 0.0000000000001M;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<InvalidOperationException>(GetPiconero);
        }

        [Fact]
        public void MoneroToPiconero_MaxMonero_Throws()
        {
            decimal monero = decimal.MaxValue;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<OverflowException>(GetPiconero);
        }

        [Theory]
        [InlineData(10, 0.000000000010)]
        [InlineData(1, 0.000000000001)]
        public void PiconeroToMonero_ValidMoneroAmounts_ReturnCorrectMonero(ulong piconero, decimal expectedMonero)
        {
            var monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.Equal(expectedMonero, monero);
        }

        [Fact]
        public void PiconeroToMonero_MaxPiconero_DoesNotThrow()
        {
            ulong piconero = ulong.MaxValue;
            _ = PriceUtilities.PiconeroToMonero(piconero);
            // Does not throw.
        }

        [Fact]
        public void PiconeroToMonero_ZeroPiconero()
        {
            ulong piconero = 0;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.Equal(0M, monero);
        }
    }
}