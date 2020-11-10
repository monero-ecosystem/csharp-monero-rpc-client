using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD
{
    public enum TransferPriority : uint
    {
        /// <summary>
        /// Fee multiplier 1x
        /// </summary>
        Unimportant = 0,
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