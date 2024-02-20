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


namespace Substrate.NetApi.Generated.Model.primitives
{
    
    
    /// <summary>
    /// >> 316 - Composite[primitives.SessionValidators]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class SessionValidators : BaseType
    {
        
        /// <summary>
        /// >> committee
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> Committee { get; set; }
        /// <summary>
        /// >> non_committee
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> NonCommittee { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "SessionValidators";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Committee.Encode());
            result.AddRange(NonCommittee.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Committee = new Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            Committee.Decode(byteArray, ref p);
            NonCommittee = new Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            NonCommittee.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
