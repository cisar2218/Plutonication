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


namespace Substrate.NetApi.Generated.Model.pallet_contracts.storage
{
    
    
    /// <summary>
    /// >> 275 - Composite[pallet_contracts.storage.DeletionQueueManager]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class DeletionQueueManager : BaseType
    {
        
        /// <summary>
        /// >> insert_counter
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 InsertCounter { get; set; }
        /// <summary>
        /// >> delete_counter
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 DeleteCounter { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "DeletionQueueManager";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(InsertCounter.Encode());
            result.AddRange(DeleteCounter.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            InsertCounter = new Substrate.NetApi.Model.Types.Primitive.U32();
            InsertCounter.Decode(byteArray, ref p);
            DeleteCounter = new Substrate.NetApi.Model.Types.Primitive.U32();
            DeleteCounter.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
