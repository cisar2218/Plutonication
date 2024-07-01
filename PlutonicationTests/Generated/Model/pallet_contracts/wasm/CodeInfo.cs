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


namespace Substrate.NetApi.Generated.Model.pallet_contracts.wasm
{
    
    
    /// <summary>
    /// >> 272 - Composite[pallet_contracts.wasm.CodeInfo]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class CodeInfo : BaseType
    {
        
        /// <summary>
        /// >> owner
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32 Owner { get; set; }
        /// <summary>
        /// >> deposit
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U128> Deposit { get; set; }
        /// <summary>
        /// >> refcount
        /// </summary>
        public Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U64> Refcount { get; set; }
        /// <summary>
        /// >> determinism
        /// </summary>
        public Substrate.NetApi.Generated.Model.pallet_contracts.wasm.EnumDeterminism Determinism { get; set; }
        /// <summary>
        /// >> code_len
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U32 CodeLen { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "CodeInfo";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Owner.Encode());
            result.AddRange(Deposit.Encode());
            result.AddRange(Refcount.Encode());
            result.AddRange(Determinism.Encode());
            result.AddRange(CodeLen.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Owner = new Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32();
            Owner.Decode(byteArray, ref p);
            Deposit = new Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U128>();
            Deposit.Decode(byteArray, ref p);
            Refcount = new Substrate.NetApi.Model.Types.Base.BaseCom<Substrate.NetApi.Model.Types.Primitive.U64>();
            Refcount.Decode(byteArray, ref p);
            Determinism = new Substrate.NetApi.Generated.Model.pallet_contracts.wasm.EnumDeterminism();
            Determinism.Decode(byteArray, ref p);
            CodeLen = new Substrate.NetApi.Model.Types.Primitive.U32();
            CodeLen.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}