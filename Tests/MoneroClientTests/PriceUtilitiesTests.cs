using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monero.Client.Utilities;

namespace PriceUtilitiesTests
{
    [TestClass]
    public class PriceUtilitiesTests
    {

        [TestMethod]
        public void MoneroToPiconero_NegativeMonero_Throws()
        {
            decimal monero = -1;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.ThrowsException<InvalidOperationException>(GetPiconero);
        }

        [TestMethod]
        public void MoneroToPiconero_MoreThan12DecimalPlaces_Throws()
        {
            decimal monero = 1.000000000000123M;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.ThrowsException<InvalidOperationException>(GetPiconero);
        }

        [TestMethod]
        public void MoneroToPiconero_ZeroMonero()
        {
            decimal monero = 0;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.AreEqual(0ul, piconero);
        }

        [TestMethod]
        public void PiconeroToMonero_ZeroPiconero()
        {
            ulong piconero = 0;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.AreEqual(0M, monero);
        }

        [TestMethod]
        public void PiconeroToMonero_TenPiconero()
        {
            ulong piconero = 10;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.AreEqual(0.000000000010M, monero);
        }

        [TestMethod]
        public void PiconeroToMonero_MaxPiconero_DoesNotThrow()
        {
            ulong piconero = ulong.MaxValue;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            // Does not throw.
        }

        [TestMethod]
        public void PiconeroToMonero_OnePiconero()
        {
            ulong piconero = 1;
            decimal monero = PriceUtilities.PiconeroToMonero(piconero);
            Assert.AreEqual(0.000000000001M, monero);
        }

        [TestMethod]
        public void MoneroToPiconero_OnePiconero()
        {
            decimal monero = 0.000000000001M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.AreEqual(1ul, piconero);
        }

        [TestMethod]
        public void MoneroToPiconero_OneMonero()
        {
            decimal monero = 1M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.AreEqual(1000000000000ul, piconero);
        }

        [TestMethod]
        public void MoneroToPiconero_HalfMonero()
        {
            decimal monero = 0.5M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.AreEqual(500000000000ul, piconero);
        }

        [TestMethod]
        public void MoneroToPiconero_OneAndAHalfMonero()
        {
            decimal monero = 1.5M;
            var piconero = PriceUtilities.MoneroToPiconero(monero);
            Assert.AreEqual(1500000000000ul, piconero);
        }

        [TestMethod]
        public void MoneroToPiconero_OneThirteenthAMonero()
        {
            decimal monero = 0.0000000000001M;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.ThrowsException<InvalidOperationException>(GetPiconero);
        }

        [TestMethod]
        public void MoneroToPiconero_MaxMonero_Throws()
        {
            decimal monero = decimal.MaxValue;
            Action GetPiconero = () => PriceUtilities.MoneroToPiconero(monero);
            Assert.ThrowsException<OverflowException>(GetPiconero);
        }
    }
}
