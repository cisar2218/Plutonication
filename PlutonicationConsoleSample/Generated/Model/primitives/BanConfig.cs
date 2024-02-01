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
    /// >> 69 - Composite[primitives.BanConfig]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class BanConfig : BaseType
    {
        
        /// <summary>
        /// >> minimal_expected_performance
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_arithmetic.per_things.Perbill MinimalExpectedPerformance { get; set; }
        /// <summary>
        /// >> underperformed_session_count_threshold
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 UnderperformedSessionCountThreshold { get; set; }
        /// <summary>
        /// >> clean_session_counter_delay
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 CleanSessionCounterDelay { get; set; }
        /// <summary>
        /// >> ban_period
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 BanPeriod { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "BanConfig";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(MinimalExpectedPerformance.Encode());
            result.AddRange(UnderperformedSessionCountThreshold.Encode());
            result.AddRange(CleanSessionCounterDelay.Encode());
            result.AddRange(BanPeriod.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            MinimalExpectedPerformance = new Substrate.NetApi.Generated.Model.sp_arithmetic.per_things.Perbill();
            MinimalExpectedPerformance.Decode(byteArray, ref p);
            UnderperformedSessionCountThreshold = new Substrate.NetApi.Model.Types.Primitive.U32();
            UnderperformedSessionCountThreshold.Decode(byteArray, ref p);
            CleanSessionCounterDelay = new Substrate.NetApi.Model.Types.Primitive.U32();
            CleanSessionCounterDelay.Decode(byteArray, ref p);
            BanPeriod = new Substrate.NetApi.Model.Types.Primitive.U32();
            BanPeriod.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}