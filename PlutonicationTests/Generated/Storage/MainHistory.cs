//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Meta;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Substrate.NetApi.Generated.Storage
{
    
    
    /// <summary>
    /// >> HistoryStorage
    /// </summary>
    public sealed class HistoryStorage
    {
        
        // Substrate client for the storage calls.
        private SubstrateClientExt _client;
        
        /// <summary>
        /// >> HistoryStorage Constructor
        /// </summary>
        public HistoryStorage(SubstrateClientExt client)
        {
            this._client = client;
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("History", "HistoricalSessions"), new System.Tuple<Substrate.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(new Substrate.NetApi.Model.Meta.Storage.Hasher[] {
                            Substrate.NetApi.Model.Meta.Storage.Hasher.Twox64Concat}, typeof(Substrate.NetApi.Model.Types.Primitive.U32), typeof(Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Generated.Model.primitive_types.H256, Substrate.NetApi.Model.Types.Primitive.U32>)));
            _client.StorageKeyDict.Add(new System.Tuple<string, string>("History", "StoredRange"), new System.Tuple<Substrate.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>(null, null, typeof(Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Model.Types.Primitive.U32, Substrate.NetApi.Model.Types.Primitive.U32>)));
        }
        
        /// <summary>
        /// >> HistoricalSessionsParams
        ///  Mapping from historical session indices to session-data root hash and validator count.
        /// </summary>
        public static string HistoricalSessionsParams(Substrate.NetApi.Model.Types.Primitive.U32 key)
        {
            return RequestGenerator.GetStorage("History", "HistoricalSessions", Substrate.NetApi.Model.Meta.Storage.Type.Map, new Substrate.NetApi.Model.Meta.Storage.Hasher[] {
                        Substrate.NetApi.Model.Meta.Storage.Hasher.Twox64Concat}, new Substrate.NetApi.Model.Types.IType[] {
                        key});
        }
        
        /// <summary>
        /// >> HistoricalSessionsDefault
        /// Default value as hex string
        /// </summary>
        public static string HistoricalSessionsDefault()
        {
            return "0x00";
        }
        
        /// <summary>
        /// >> HistoricalSessions
        ///  Mapping from historical session indices to session-data root hash and validator count.
        /// </summary>
        public async Task<Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Generated.Model.primitive_types.H256, Substrate.NetApi.Model.Types.Primitive.U32>> HistoricalSessions(Substrate.NetApi.Model.Types.Primitive.U32 key, CancellationToken token)
        {
            string parameters = HistoryStorage.HistoricalSessionsParams(key);
            var result = await _client.GetStorageAsync<Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Generated.Model.primitive_types.H256, Substrate.NetApi.Model.Types.Primitive.U32>>(parameters, token);
            return result;
        }
        
        /// <summary>
        /// >> StoredRangeParams
        ///  The range of historical sessions we store. [first, last)
        /// </summary>
        public static string StoredRangeParams()
        {
            return RequestGenerator.GetStorage("History", "StoredRange", Substrate.NetApi.Model.Meta.Storage.Type.Plain);
        }
        
        /// <summary>
        /// >> StoredRangeDefault
        /// Default value as hex string
        /// </summary>
        public static string StoredRangeDefault()
        {
            return "0x00";
        }
        
        /// <summary>
        /// >> StoredRange
        ///  The range of historical sessions we store. [first, last)
        /// </summary>
        public async Task<Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Model.Types.Primitive.U32, Substrate.NetApi.Model.Types.Primitive.U32>> StoredRange(CancellationToken token)
        {
            string parameters = HistoryStorage.StoredRangeParams();
            var result = await _client.GetStorageAsync<Substrate.NetApi.Model.Types.Base.BaseTuple<Substrate.NetApi.Model.Types.Primitive.U32, Substrate.NetApi.Model.Types.Primitive.U32>>(parameters, token);
            return result;
        }
    }
    
    /// <summary>
    /// >> HistoryCalls
    /// </summary>
    public sealed class HistoryCalls
    {
    }
    
    /// <summary>
    /// >> HistoryConstants
    /// </summary>
    public sealed class HistoryConstants
    {
    }
}
