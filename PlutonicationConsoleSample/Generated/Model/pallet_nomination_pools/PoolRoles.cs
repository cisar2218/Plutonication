//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Substrate.NetApi.Attributes;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Metadata.V14;
using System.Collections.Generic;


namespace Substrate.NetApi.Generated.Model.pallet_nomination_pools
{
    
    
    /// <summary>
    /// >> 291 - Composite[pallet_nomination_pools.PoolRoles]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class PoolRoles : BaseType
    {
        
        /// <summary>
        /// >> depositor
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32 Depositor { get; set; }
        /// <summary>
        /// >> root
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseOpt<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> Root { get; set; }
        /// <summary>
        /// >> nominator
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseOpt<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> Nominator { get; set; }
        /// <summary>
        /// >> bouncer
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseOpt<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> Bouncer { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "PoolRoles";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Depositor.Encode());
            result.AddRange(Root.Encode());
            result.AddRange(Nominator.Encode());
            result.AddRange(Bouncer.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Depositor = new Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32();
            Depositor.Decode(byteArray, ref p);
            Root = new Substrate.NetApi.Model.Types.Base.BaseOpt<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            Root.Decode(byteArray, ref p);
            Nominator = new Substrate.NetApi.Model.Types.Base.BaseOpt<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            Nominator.Decode(byteArray, ref p);
            Bouncer = new Substrate.NetApi.Model.Types.Base.BaseOpt<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            Bouncer.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
