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


namespace Substrate.NetApi.Generated.Model.pallet_nomination_pools
{
    
    
    /// <summary>
    /// >> ClaimPermission
    /// </summary>
    public enum ClaimPermission
    {
        
        /// <summary>
        /// >> Permissioned
        /// </summary>
        Permissioned = 0,
        
        /// <summary>
        /// >> PermissionlessCompound
        /// </summary>
        PermissionlessCompound = 1,
        
        /// <summary>
        /// >> PermissionlessWithdraw
        /// </summary>
        PermissionlessWithdraw = 2,
        
        /// <summary>
        /// >> PermissionlessAll
        /// </summary>
        PermissionlessAll = 3,
    }
    
    /// <summary>
    /// >> 151 - Variant[pallet_nomination_pools.ClaimPermission]
    /// </summary>
    public sealed class EnumClaimPermission : BaseEnum<ClaimPermission>
    {
    }
}
