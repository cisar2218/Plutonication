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
using Substrate.NetApi.Model.Types.Metadata.Base;
using System.Collections.Generic;


namespace Substrate.NetApi.Generated.Model.primitives
{
    
    
    /// <summary>
    /// >> 293 - Composite[primitives.EraValidators]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class EraValidators : BaseType
    {
        
        /// <summary>
        /// >> reserved
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> Reserved { get; set; }
        /// <summary>
        /// >> non_reserved
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32> NonReserved { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "EraValidators";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Reserved.Encode());
            result.AddRange(NonReserved.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Reserved = new Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            Reserved.Decode(byteArray, ref p);
            NonReserved = new Substrate.NetApi.Model.Types.Base.BaseVec<Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32>();
            NonReserved.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            global::System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
