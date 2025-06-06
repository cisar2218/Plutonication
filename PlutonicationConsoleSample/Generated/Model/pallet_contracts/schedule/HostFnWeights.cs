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


namespace Substrate.NetApi.Generated.Model.pallet_contracts.schedule
{
    
    
    /// <summary>
    /// >> 324 - Composite[pallet_contracts.schedule.HostFnWeights]
    /// </summary>
    [SubstrateNodeType(TypeDefEnum.Composite)]
    public sealed class HostFnWeights : BaseType
    {
        
        /// <summary>
        /// >> caller
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Caller { get; set; }
        /// <summary>
        /// >> is_contract
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight IsContract { get; set; }
        /// <summary>
        /// >> code_hash
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight CodeHash { get; set; }
        /// <summary>
        /// >> own_code_hash
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight OwnCodeHash { get; set; }
        /// <summary>
        /// >> caller_is_origin
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight CallerIsOrigin { get; set; }
        /// <summary>
        /// >> caller_is_root
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight CallerIsRoot { get; set; }
        /// <summary>
        /// >> address
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Address { get; set; }
        /// <summary>
        /// >> gas_left
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight GasLeft { get; set; }
        /// <summary>
        /// >> balance
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Balance { get; set; }
        /// <summary>
        /// >> value_transferred
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ValueTransferred { get; set; }
        /// <summary>
        /// >> minimum_balance
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight MinimumBalance { get; set; }
        /// <summary>
        /// >> block_number
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight BlockNumber { get; set; }
        /// <summary>
        /// >> now
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Now { get; set; }
        /// <summary>
        /// >> weight_to_fee
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight WeightToFee { get; set; }
        /// <summary>
        /// >> input
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Input { get; set; }
        /// <summary>
        /// >> input_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight InputPerByte { get; set; }
        /// <summary>
        /// >> r#return
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight RHreturn { get; set; }
        /// <summary>
        /// >> return_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ReturnPerByte { get; set; }
        /// <summary>
        /// >> terminate
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Terminate { get; set; }
        /// <summary>
        /// >> random
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Random { get; set; }
        /// <summary>
        /// >> deposit_event
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight DepositEvent { get; set; }
        /// <summary>
        /// >> deposit_event_per_topic
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight DepositEventPerTopic { get; set; }
        /// <summary>
        /// >> deposit_event_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight DepositEventPerByte { get; set; }
        /// <summary>
        /// >> debug_message
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight DebugMessage { get; set; }
        /// <summary>
        /// >> debug_message_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight DebugMessagePerByte { get; set; }
        /// <summary>
        /// >> set_storage
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight SetStorage { get; set; }
        /// <summary>
        /// >> set_storage_per_new_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight SetStoragePerNewByte { get; set; }
        /// <summary>
        /// >> set_storage_per_old_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight SetStoragePerOldByte { get; set; }
        /// <summary>
        /// >> set_code_hash
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight SetCodeHash { get; set; }
        /// <summary>
        /// >> clear_storage
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ClearStorage { get; set; }
        /// <summary>
        /// >> clear_storage_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ClearStoragePerByte { get; set; }
        /// <summary>
        /// >> contains_storage
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ContainsStorage { get; set; }
        /// <summary>
        /// >> contains_storage_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ContainsStoragePerByte { get; set; }
        /// <summary>
        /// >> get_storage
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight GetStorage { get; set; }
        /// <summary>
        /// >> get_storage_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight GetStoragePerByte { get; set; }
        /// <summary>
        /// >> take_storage
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight TakeStorage { get; set; }
        /// <summary>
        /// >> take_storage_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight TakeStoragePerByte { get; set; }
        /// <summary>
        /// >> transfer
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Transfer { get; set; }
        /// <summary>
        /// >> call
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Call { get; set; }
        /// <summary>
        /// >> delegate_call
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight DelegateCall { get; set; }
        /// <summary>
        /// >> call_transfer_surcharge
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight CallTransferSurcharge { get; set; }
        /// <summary>
        /// >> call_per_cloned_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight CallPerClonedByte { get; set; }
        /// <summary>
        /// >> instantiate
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Instantiate { get; set; }
        /// <summary>
        /// >> instantiate_transfer_surcharge
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight InstantiateTransferSurcharge { get; set; }
        /// <summary>
        /// >> instantiate_per_input_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight InstantiatePerInputByte { get; set; }
        /// <summary>
        /// >> instantiate_per_salt_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight InstantiatePerSaltByte { get; set; }
        /// <summary>
        /// >> hash_sha2_256
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashSha2256 { get; set; }
        /// <summary>
        /// >> hash_sha2_256_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashSha2256PerByte { get; set; }
        /// <summary>
        /// >> hash_keccak_256
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashKeccak256 { get; set; }
        /// <summary>
        /// >> hash_keccak_256_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashKeccak256PerByte { get; set; }
        /// <summary>
        /// >> hash_blake2_256
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashBlake2256 { get; set; }
        /// <summary>
        /// >> hash_blake2_256_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashBlake2256PerByte { get; set; }
        /// <summary>
        /// >> hash_blake2_128
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashBlake2128 { get; set; }
        /// <summary>
        /// >> hash_blake2_128_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight HashBlake2128PerByte { get; set; }
        /// <summary>
        /// >> ecdsa_recover
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight EcdsaRecover { get; set; }
        /// <summary>
        /// >> ecdsa_to_eth_address
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight EcdsaToEthAddress { get; set; }
        /// <summary>
        /// >> sr25519_verify
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Sr25519Verify { get; set; }
        /// <summary>
        /// >> sr25519_verify_per_byte
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight Sr25519VerifyPerByte { get; set; }
        /// <summary>
        /// >> reentrance_count
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight ReentranceCount { get; set; }
        /// <summary>
        /// >> account_reentrance_count
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight AccountReentranceCount { get; set; }
        /// <summary>
        /// >> instantiation_nonce
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight InstantiationNonce { get; set; }
        /// <summary>
        /// >> add_delegate_dependency
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight AddDelegateDependency { get; set; }
        /// <summary>
        /// >> remove_delegate_dependency
        /// </summary>
        public Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight RemoveDelegateDependency { get; set; }
        
        /// <inheritdoc/>
        public override string TypeName()
        {
            return "HostFnWeights";
        }
        
        /// <inheritdoc/>
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(Caller.Encode());
            result.AddRange(IsContract.Encode());
            result.AddRange(CodeHash.Encode());
            result.AddRange(OwnCodeHash.Encode());
            result.AddRange(CallerIsOrigin.Encode());
            result.AddRange(CallerIsRoot.Encode());
            result.AddRange(Address.Encode());
            result.AddRange(GasLeft.Encode());
            result.AddRange(Balance.Encode());
            result.AddRange(ValueTransferred.Encode());
            result.AddRange(MinimumBalance.Encode());
            result.AddRange(BlockNumber.Encode());
            result.AddRange(Now.Encode());
            result.AddRange(WeightToFee.Encode());
            result.AddRange(Input.Encode());
            result.AddRange(InputPerByte.Encode());
            result.AddRange(RHreturn.Encode());
            result.AddRange(ReturnPerByte.Encode());
            result.AddRange(Terminate.Encode());
            result.AddRange(Random.Encode());
            result.AddRange(DepositEvent.Encode());
            result.AddRange(DepositEventPerTopic.Encode());
            result.AddRange(DepositEventPerByte.Encode());
            result.AddRange(DebugMessage.Encode());
            result.AddRange(DebugMessagePerByte.Encode());
            result.AddRange(SetStorage.Encode());
            result.AddRange(SetStoragePerNewByte.Encode());
            result.AddRange(SetStoragePerOldByte.Encode());
            result.AddRange(SetCodeHash.Encode());
            result.AddRange(ClearStorage.Encode());
            result.AddRange(ClearStoragePerByte.Encode());
            result.AddRange(ContainsStorage.Encode());
            result.AddRange(ContainsStoragePerByte.Encode());
            result.AddRange(GetStorage.Encode());
            result.AddRange(GetStoragePerByte.Encode());
            result.AddRange(TakeStorage.Encode());
            result.AddRange(TakeStoragePerByte.Encode());
            result.AddRange(Transfer.Encode());
            result.AddRange(Call.Encode());
            result.AddRange(DelegateCall.Encode());
            result.AddRange(CallTransferSurcharge.Encode());
            result.AddRange(CallPerClonedByte.Encode());
            result.AddRange(Instantiate.Encode());
            result.AddRange(InstantiateTransferSurcharge.Encode());
            result.AddRange(InstantiatePerInputByte.Encode());
            result.AddRange(InstantiatePerSaltByte.Encode());
            result.AddRange(HashSha2256.Encode());
            result.AddRange(HashSha2256PerByte.Encode());
            result.AddRange(HashKeccak256.Encode());
            result.AddRange(HashKeccak256PerByte.Encode());
            result.AddRange(HashBlake2256.Encode());
            result.AddRange(HashBlake2256PerByte.Encode());
            result.AddRange(HashBlake2128.Encode());
            result.AddRange(HashBlake2128PerByte.Encode());
            result.AddRange(EcdsaRecover.Encode());
            result.AddRange(EcdsaToEthAddress.Encode());
            result.AddRange(Sr25519Verify.Encode());
            result.AddRange(Sr25519VerifyPerByte.Encode());
            result.AddRange(ReentranceCount.Encode());
            result.AddRange(AccountReentranceCount.Encode());
            result.AddRange(InstantiationNonce.Encode());
            result.AddRange(AddDelegateDependency.Encode());
            result.AddRange(RemoveDelegateDependency.Encode());
            return result.ToArray();
        }
        
        /// <inheritdoc/>
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            Caller = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Caller.Decode(byteArray, ref p);
            IsContract = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            IsContract.Decode(byteArray, ref p);
            CodeHash = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            CodeHash.Decode(byteArray, ref p);
            OwnCodeHash = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            OwnCodeHash.Decode(byteArray, ref p);
            CallerIsOrigin = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            CallerIsOrigin.Decode(byteArray, ref p);
            CallerIsRoot = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            CallerIsRoot.Decode(byteArray, ref p);
            Address = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Address.Decode(byteArray, ref p);
            GasLeft = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            GasLeft.Decode(byteArray, ref p);
            Balance = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Balance.Decode(byteArray, ref p);
            ValueTransferred = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ValueTransferred.Decode(byteArray, ref p);
            MinimumBalance = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            MinimumBalance.Decode(byteArray, ref p);
            BlockNumber = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            BlockNumber.Decode(byteArray, ref p);
            Now = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Now.Decode(byteArray, ref p);
            WeightToFee = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            WeightToFee.Decode(byteArray, ref p);
            Input = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Input.Decode(byteArray, ref p);
            InputPerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            InputPerByte.Decode(byteArray, ref p);
            RHreturn = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            RHreturn.Decode(byteArray, ref p);
            ReturnPerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ReturnPerByte.Decode(byteArray, ref p);
            Terminate = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Terminate.Decode(byteArray, ref p);
            Random = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Random.Decode(byteArray, ref p);
            DepositEvent = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            DepositEvent.Decode(byteArray, ref p);
            DepositEventPerTopic = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            DepositEventPerTopic.Decode(byteArray, ref p);
            DepositEventPerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            DepositEventPerByte.Decode(byteArray, ref p);
            DebugMessage = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            DebugMessage.Decode(byteArray, ref p);
            DebugMessagePerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            DebugMessagePerByte.Decode(byteArray, ref p);
            SetStorage = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            SetStorage.Decode(byteArray, ref p);
            SetStoragePerNewByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            SetStoragePerNewByte.Decode(byteArray, ref p);
            SetStoragePerOldByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            SetStoragePerOldByte.Decode(byteArray, ref p);
            SetCodeHash = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            SetCodeHash.Decode(byteArray, ref p);
            ClearStorage = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ClearStorage.Decode(byteArray, ref p);
            ClearStoragePerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ClearStoragePerByte.Decode(byteArray, ref p);
            ContainsStorage = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ContainsStorage.Decode(byteArray, ref p);
            ContainsStoragePerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ContainsStoragePerByte.Decode(byteArray, ref p);
            GetStorage = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            GetStorage.Decode(byteArray, ref p);
            GetStoragePerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            GetStoragePerByte.Decode(byteArray, ref p);
            TakeStorage = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            TakeStorage.Decode(byteArray, ref p);
            TakeStoragePerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            TakeStoragePerByte.Decode(byteArray, ref p);
            Transfer = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Transfer.Decode(byteArray, ref p);
            Call = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Call.Decode(byteArray, ref p);
            DelegateCall = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            DelegateCall.Decode(byteArray, ref p);
            CallTransferSurcharge = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            CallTransferSurcharge.Decode(byteArray, ref p);
            CallPerClonedByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            CallPerClonedByte.Decode(byteArray, ref p);
            Instantiate = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Instantiate.Decode(byteArray, ref p);
            InstantiateTransferSurcharge = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            InstantiateTransferSurcharge.Decode(byteArray, ref p);
            InstantiatePerInputByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            InstantiatePerInputByte.Decode(byteArray, ref p);
            InstantiatePerSaltByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            InstantiatePerSaltByte.Decode(byteArray, ref p);
            HashSha2256 = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashSha2256.Decode(byteArray, ref p);
            HashSha2256PerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashSha2256PerByte.Decode(byteArray, ref p);
            HashKeccak256 = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashKeccak256.Decode(byteArray, ref p);
            HashKeccak256PerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashKeccak256PerByte.Decode(byteArray, ref p);
            HashBlake2256 = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashBlake2256.Decode(byteArray, ref p);
            HashBlake2256PerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashBlake2256PerByte.Decode(byteArray, ref p);
            HashBlake2128 = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashBlake2128.Decode(byteArray, ref p);
            HashBlake2128PerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            HashBlake2128PerByte.Decode(byteArray, ref p);
            EcdsaRecover = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            EcdsaRecover.Decode(byteArray, ref p);
            EcdsaToEthAddress = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            EcdsaToEthAddress.Decode(byteArray, ref p);
            Sr25519Verify = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Sr25519Verify.Decode(byteArray, ref p);
            Sr25519VerifyPerByte = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            Sr25519VerifyPerByte.Decode(byteArray, ref p);
            ReentranceCount = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            ReentranceCount.Decode(byteArray, ref p);
            AccountReentranceCount = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            AccountReentranceCount.Decode(byteArray, ref p);
            InstantiationNonce = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            InstantiationNonce.Decode(byteArray, ref p);
            AddDelegateDependency = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            AddDelegateDependency.Decode(byteArray, ref p);
            RemoveDelegateDependency = new Substrate.NetApi.Generated.Model.sp_weights.weight_v2.Weight();
            RemoveDelegateDependency.Decode(byteArray, ref p);
            var bytesLength = p - start;
            TypeSize = bytesLength;
            Bytes = new byte[bytesLength];
            global::System.Array.Copy(byteArray, start, Bytes, 0, bytesLength);
        }
    }
}
