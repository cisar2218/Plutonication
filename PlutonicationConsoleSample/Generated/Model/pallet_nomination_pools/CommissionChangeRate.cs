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
    /// >> 66 - Composite[pallet_nomination_pools.CommissionChangeRate]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class CommissionChangeRate : BaseType
    {
        
        /// <summary>
        /// >> max_increase
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_arithmetic.per_things.Perbill MaxIncrease { get; set; }
        /// <summary>
        /// >> min_delay
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 MinDelay { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "CommissionChangeRate";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(MaxIncrease.Encode());
            result.AddRange(MinDelay.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            MaxIncrease = new Substrate.NetApi.Generated.Model.sp_arithmetic.per_things.Perbill();
            MaxIncrease.Decode(byteArray, ref p);
            MinDelay = new Substrate.NetApi.Model.Types.Primitive.U32();
            MinDelay.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
