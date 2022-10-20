using Monero.Client.Utilities;
using System;
using Xunit;

namespace PriceUtilitiesTests
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

        [Fact]
        public void MoneroToPiconero_ZeroMonero()
        {
            decimal monero = 0;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(0ul, piconero);
        }

        [Fact]
        public void PiconeroToMonero_ZeroPiconero()
        {
            ulong piconero = 0;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.Equal(0M, monero);
        }

        [Fact]
        public void PiconeroToMonero_TenPiconero()
        {
            ulong piconero = 10;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.Equal(0.000000000010M, monero);
        }

        [Fact]
        public void PiconeroToMonero_MaxPiconero_DoesNotThrow()
        {
            ulong piconero = ulong.MaxValue;
            _ = PriceUtilities.PiconeroToMonero(piconero);
            // Does not throw.
        }

        [Fact]
        public void PiconeroToMonero_OnePiconero()
        {
            ulong piconero = 1;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.Equal(0.000000000001M, monero);
        }

        [Fact]
        public void MoneroToPiconero_OnePiconero()
        {
            decimal monero = 0.000000000001M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(1ul, piconero);
        }

        [Fact]
        public void MoneroToPiconero_OneMonero()
        {
            decimal monero = 1M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(1000000000000ul, piconero);
        }

        [Fact]
        public void MoneroToPiconero_HalfMonero()
        {
            decimal monero = 0.5M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(500000000000ul, piconero);
        }

        [Fact]
        public void MoneroToPiconero_OneAndAHalfMonero()
        {
            decimal monero = 1.5M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.Equal(1500000000000ul, piconero);
        }

        [Fact]
        public void MoneroToPiconero_OneThirteenthAMonero()
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
    }
}
