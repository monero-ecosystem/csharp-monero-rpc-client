namespace Monero.Client.Enums
{
    public enum TransferType
    {
        /// <summary>
        /// All the transfers.
        /// </summary>
        All,

        /// <summary>
        /// Only transfers which are not yet spent.
        /// </summary>
        Available,

        /// <summary>
        /// Only transfers which are already spent.
        /// </summary>
        Unavailable,
    }
}