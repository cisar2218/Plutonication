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


namespace Substrate.NetApi.Generated.Model.pallet_multisig
{
    
    
    /// <summary>
    /// >> 267 - Composite[pallet_multisig.Multisig]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class Multisig : BaseType
    {
        
        /// <summary>
        /// >> when
        /// </summary>
        public Substrate.NetApi.Generated.Model.pallet_multisig.Timepoint When { get; set; }
        /// <summary>
        /// >> deposit
        /// </summary>
        public Substrate.NetApi.Model.Types.Primitive.U128 Deposit { get; set; }
        /// <summary>
        /// >> depositor
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32 Depositor { get; set; }
        /// <summary>
        /// >> approvals
        /// </summary>
        public Substrate.NetApi.Generated.Model.bounded_collections.bounded_vec.BoundedVecT15 Approvals { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "Multisig";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(When.Encode());
            result.AddRange(Deposit.Encode());
            result.AddRange(Depositor.Encode());
            result.AddRange(Approvals.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            When = new Substrate.NetApi.Generated.Model.pallet_multisig.Timepoint();
            When.Decode(byteArray, ref p);
            Deposit = new Substrate.NetApi.Model.Types.Primitive.U128();
            Deposit.Decode(byteArray, ref p);
            Depositor = new Substrate.NetApi.Generated.Model.sp_core.crypto.AccountId32();
            Depositor.Decode(byteArray, ref p);
            Approvals = new Substrate.NetApi.Generated.Model.bounded_collections.bounded_vec.BoundedVecT15();
            Approvals.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
