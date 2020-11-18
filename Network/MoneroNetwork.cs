namespace Monero.Client.Network
{
    /// <summary>
    /// A given flavor of the Monero network.
    /// </summary>
    public enum MoneroNetwork
    {
        /// <summary>
        /// Mainnet is the "production" network and blockchain.
        /// </summary>
        Mainnet,
        /// <summary>
        /// Stagenet is technically equivalent to mainnet, both in terms of features and consensus rules.
        /// </summary>
        Stagenet,
        /// <summary>
        /// Testnet is the "experimental" network and blockchain where things get released long before mainnet.
        /// </summary>
        Testnet,
    }
}