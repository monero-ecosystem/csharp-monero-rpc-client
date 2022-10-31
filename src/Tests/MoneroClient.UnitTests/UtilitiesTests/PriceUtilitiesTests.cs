using System;
using Monero.Client.Utilities;
using Xunit;

namespace MoneroClient.UnitTests.UtilitiesTests
{
    public class PriceUtilitiesTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0.000000000001, 1)]
        [InlineData(1, 1000000000000)]
        [InlineData(0.5, 500000000000)]
        [InlineData(1.5, 1500000000000)]
        public void MoneroToPiconero_ValidMoneroAmounts_ReturnsCorrectPiconero(decimal monero, ulong expectedPiconero)
        {
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(expectedPiconero, piconero);
        }

        [Fact]
        public void MoneroToPiconero_OneThirteenthAMonero_Throws()
        {
            decimal monero = 0.0000000000001M;
            Action getPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<InvalidOperationException>(getPiconero);
        }

        [Fact]
        public void MoneroToPiconero_MaxMonero_Throws()
        {
            decimal monero = decimal.MaxValue;
            Action getPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<OverflowException>(getPiconero);
        }

        [Fact]
        public void MoneroToPiconero_NegativeMonero_Throws()
        {
            decimal monero = -1;
            Action getPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<InvalidOperationException>(() => getPiconero());
        }

        [Fact]
        public void MoneroToPiconero_MoreThan12DecimalPlaces_Throws()
        {
            decimal monero = 1.000000000000123M;
            Action getPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.Throws<InvalidOperationException>(getPiconero);
        }

        [Theory]
        [InlineData(10, 0.000000000010)]
        [InlineData(1, 0.000000000001)]
        [InlineData(0, 0)]
        public void PiconeroToMonero_ValidPiconeroAmounts_ReturnsCorrectMonero(ulong piconero, decimal expectedMonero)
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
    }
}