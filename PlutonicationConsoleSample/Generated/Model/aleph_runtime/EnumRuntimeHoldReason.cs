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


namespace Substrate.NetApi.Generated.Model.aleph_runtime
{
    
    
    /// <summary>
    /// >> RuntimeHoldReason
    /// </summary>
    public enum RuntimeHoldReason
    {
        
        /// <summary>
        /// >> Contracts
        /// </summary>
        Contracts = 18,
        
        /// <summary>
        /// >> SafeMode
        /// </summary>
        SafeMode = 23,
    }
    
    /// <summary>
    /// >> 245 - Variant[aleph_runtime.RuntimeHoldReason]
    /// </summary>
    public sealed class EnumRuntimeHoldReason : BaseEnumRust<RuntimeHoldReason>
    {
        
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EnumRuntimeHoldReason()
        {
				AddTypeDecoder<Substrate.NetApi.Generated.Model.pallet_contracts.pallet.EnumHoldReason>(RuntimeHoldReason.Contracts);
				AddTypeDecoder<Substrate.NetApi.Generated.Model.pallet_safe_mode.pallet.EnumHoldReason>(RuntimeHoldReason.SafeMode);
        }
    }
}
