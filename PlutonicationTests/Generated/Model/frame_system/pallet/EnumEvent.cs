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


namespace Substrate.NetApi.Generated.Model.frame_system.pallet
{
    
    
    /// <summary>
    /// >> Event
    /// Event for the System pallet.
    /// </summary>
    public enum Event
    {
        
        /// <summary>
        /// >> ExtrinsicSuccess
        /// An extrinsic completed successfully.
        /// </summary>
        ExtrinsicSuccess = 0,
        
        /// <summary>
        /// >> ExtrinsicFailed
        /// An extrinsic failed.
        /// </summary>
        ExtrinsicFailed = 1,
        
        /// <summary>
        /// >> CodeUpdated
        /// `:code` was updated.
        /// </summary>
        CodeUpdated = 2,
        
        /// <summary>
        /// >> NewAccount
        /// A new account was created.
        /// </summary>
        NewAccount = 3,
        
        /// <summary>
        /// >> KilledAccount
        /// An account was reaped.
        /// </summary>
        KilledAccount = 4,
        
        /// <summary>
        /// >> Remarked
        /// On on-chain remark happened.
        /// </summary>
        Remarked = 5,
    }
    
    /// <summary>
    /// >> 21 - Variant[frame_system.pallet.Event]
    /// Event for the System pallet.
    /// </summary>
    public sealed class EnumEvent : BaseEnumExt<Event, Substrate.NetApi.Generated.Model.frame_support.dispatch.DispatchInfo, BaseTuple<Substrate.NetApi.Generated.Model.sp_runtime.EnumDispatchError, Substrate.NetApi.Generated.Model.frame_support.dispatch.DispatchInfo>, BaseVoid, Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32, Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32, BaseTuple<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32, Substrate.NetApi.Generated.Model.primitive_types.H256>>
    {
    }
}
