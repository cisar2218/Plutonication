//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Substrate.NetApi.Model.Types.Base;
using System.Collections.Generic;


namespace Substrate.NetApi.Generated.Model.pallet_operations.pallet
{
    
    
    /// <summary>
    /// >> Event
    /// The `Event` enum of this pallet
    /// </summary>
    public enum Event
    {
        
        /// <summary>
        /// >> ConsumersCounterIncremented
        /// A consumers counter was incremented for an account
        /// </summary>
        ConsumersCounterIncremented = 0,
        
        /// <summary>
        /// >> ConsumersCounterDecremented
        /// A consumers counter was decremented for an account
        /// </summary>
        ConsumersCounterDecremented = 1,
    }
    
    /// <summary>
    /// >> 88 - Variant[pallet_operations.pallet.Event]
    /// The `Event` enum of this pallet
    /// </summary>
    public sealed class EnumEvent : BaseEnumRust<Event>
    {
        
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EnumEvent()
        {
				AddTypeDecoder<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>(Event.ConsumersCounterIncremented);
				AddTypeDecoder<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>(Event.ConsumersCounterDecremented);
        }
    }
}
