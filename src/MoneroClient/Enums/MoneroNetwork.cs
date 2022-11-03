namespace Monero.Client.Network
{
    /// <summary>
    /// A given flavor of the Monero network.
    /// </summary>
    public enum MoneroNetwork
    {
        /// <summary>
        /// Mainnet is the "production" network and blockchain.
        /// Port 18082 for Wallet and port 18081 for Daemon.
        /// </summary>
        Mainnet,

        /// <summary>
        /// Stagenet is technically equivalent to mainnet, both in terms of features and consensus rules.
        /// Port 38082 for Wallet and port 38081 for Daemon.
        /// </summary>
        Stagenet,

        /// <summary>
        /// Testnet is the "experimental" network and blockchain where things get released long before mainnet.
        /// Port 28082 for Wallet and port 28081 for Daemon.
        /// </summary>
        Testnet,
    }
}