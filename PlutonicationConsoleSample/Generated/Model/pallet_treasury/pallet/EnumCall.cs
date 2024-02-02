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


namespace Substrate.NetApi.Generated.Model.pallet_treasury.pallet
{
    
    
    /// <summary>
    /// >> Call
    /// Contains a variant per dispatchable extrinsic that this pallet has.
    /// </summary>
    public enum Call
    {
        
        /// <summary>
        /// >> propose_spend
        /// See [`Pallet::propose_spend`].
        /// </summary>
        propose_spend = 0,
        
        /// <summary>
        /// >> reject_proposal
        /// See [`Pallet::reject_proposal`].
        /// </summary>
        reject_proposal = 1,
        
        /// <summary>
        /// >> approve_proposal
        /// See [`Pallet::approve_proposal`].
        /// </summary>
        approve_proposal = 2,
        
        /// <summary>
        /// >> spend
        /// See [`Pallet::spend`].
        /// </summary>
        spend = 3,
        
        /// <summary>
        /// >> remove_approval
        /// See [`Pallet::remove_approval`].
        /// </summary>
        remove_approval = 4,
    }
    
    /// <summary>
    /// >> 131 - Variant[pallet_treasury.pallet.Call]
    /// Contains a variant per dispatchable extrinsic that this pallet has.
    /// </summary>
    public sealed class EnumCall : BaseEnumExt<Call, BaseTuple<Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U128>, Substrate.NetApi.Generated.Model.sp_runtime.multiaddress.EnumMultiAddress>, Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U32>, Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U32>, BaseTuple<Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U128>, Substrate.NetApi.Generated.Model.sp_runtime.multiaddress.EnumMultiAddress>, Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U32>>
    {
    }
}
