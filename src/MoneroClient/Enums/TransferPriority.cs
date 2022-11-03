namespace Monero.Client.Enums
{
    public enum TransferPriority : uint
    {
        /// <summary>
        /// Really just Normal (4x)
        /// </summary>
        Default = 0,

        /// <summary>
        /// Fee multiplier 1x
        /// </summary>
        Unimportant,

        /// <summary>
        /// Fee multiplier 4x (default)
        /// </summary>
        Normal,

        /// <summary>
        /// Fee multiplier 20x
        /// </summary>
        Elevated,

        /// <summary>
        /// Fee multiplier 166x
        /// </summary>
        Priority,
    }
}