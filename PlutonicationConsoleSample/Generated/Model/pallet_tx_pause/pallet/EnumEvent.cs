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


namespace Substrate.NetApi.Generated.Model.pallet_tx_pause.pallet
{
    
    
    /// <summary>
    /// >> Event
    /// The `Event` enum of this pallet
    /// </summary>
    public enum Event
    {
        
        /// <summary>
        /// >> CallPaused
        /// This pallet, or a specific call is now paused.
        /// </summary>
        CallPaused = 0,
        
        /// <summary>
        /// >> CallUnpaused
        /// This pallet, or a specific call is now unpaused.
        /// </summary>
        CallUnpaused = 1,
    }
    
    /// <summary>
    /// >> 85 - Variant[pallet_tx_pause.pallet.Event]
    /// The `Event` enum of this pallet
    /// </summary>
    public sealed class EnumEvent : BaseEnumRust<Event>
    {
        
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EnumEvent()
        {
				AddTypeDecoder<Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Generated.Model.bounded_collections.bounded_vec.BoundedVecT3, Substrate.NetApi.Generated.Model.bounded_collections.bounded_vec.BoundedVecT3>>(Event.CallPaused);
				AddTypeDecoder<Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Generated.Model.bounded_collections.bounded_vec.BoundedVecT3, Substrate.NetApi.Generated.Model.bounded_collections.bounded_vec.BoundedVecT3>>(Event.CallUnpaused);
        }
    }
}
