//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Substrate.NetApi.Generated.Storage;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Meta;
using Substrate.NetApi.Model.Types.Base;
using System.Collections.Generic;


namespace Substrate.NetApi.Generated
{
    
    
    /// <summary>
    /// >> Substrate Client Extension, including all Storage classes direct access.
    /// </summary>
    public sealed class SubstrateClientExt : Substrate.NetApi.SubstrateClient
    {
        
        /// <summary>
        /// StorageKeyDict for key definition informations.
        /// </summary>
        public System.Collections.Generic.Dictionary<System.Tuple<string, string>, System.Tuple<Substrate.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>> StorageKeyDict;
        
        /// <summary>
        /// SystemStorage storage calls.
        /// </summary>
        public SystemStorage SystemStorage;
        
        /// <summary>
        /// RandomnessCollectiveFlipStorage storage calls.
        /// </summary>
        public RandomnessCollectiveFlipStorage RandomnessCollectiveFlipStorage;
        
        /// <summary>
        /// SchedulerStorage storage calls.
        /// </summary>
        public SchedulerStorage SchedulerStorage;
        
        /// <summary>
        /// AuraStorage storage calls.
        /// </summary>
        public AuraStorage AuraStorage;
        
        /// <summary>
        /// TimestampStorage storage calls.
        /// </summary>
        public TimestampStorage TimestampStorage;
        
        /// <summary>
        /// BalancesStorage storage calls.
        /// </summary>
        public BalancesStorage BalancesStorage;
        
        /// <summary>
        /// TransactionPaymentStorage storage calls.
        /// </summary>
        public TransactionPaymentStorage TransactionPaymentStorage;
        
        /// <summary>
        /// AuthorshipStorage storage calls.
        /// </summary>
        public AuthorshipStorage AuthorshipStorage;
        
        /// <summary>
        /// StakingStorage storage calls.
        /// </summary>
        public StakingStorage StakingStorage;
        
        /// <summary>
        /// HistoryStorage storage calls.
        /// </summary>
        public HistoryStorage HistoryStorage;
        
        /// <summary>
        /// SessionStorage storage calls.
        /// </summary>
        public SessionStorage SessionStorage;
        
        /// <summary>
        /// AlephStorage storage calls.
        /// </summary>
        public AlephStorage AlephStorage;
        
        /// <summary>
        /// ElectionsStorage storage calls.
        /// </summary>
        public ElectionsStorage ElectionsStorage;
        
        /// <summary>
        /// TreasuryStorage storage calls.
        /// </summary>
        public TreasuryStorage TreasuryStorage;
        
        /// <summary>
        /// VestingStorage storage calls.
        /// </summary>
        public VestingStorage VestingStorage;
        
        /// <summary>
        /// UtilityStorage storage calls.
        /// </summary>
        public UtilityStorage UtilityStorage;
        
        /// <summary>
        /// MultisigStorage storage calls.
        /// </summary>
        public MultisigStorage MultisigStorage;
        
        /// <summary>
        /// SudoStorage storage calls.
        /// </summary>
        public SudoStorage SudoStorage;
        
        /// <summary>
        /// ContractsStorage storage calls.
        /// </summary>
        public ContractsStorage ContractsStorage;
        
        /// <summary>
        /// NominationPoolsStorage storage calls.
        /// </summary>
        public NominationPoolsStorage NominationPoolsStorage;
        
        /// <summary>
        /// IdentityStorage storage calls.
        /// </summary>
        public IdentityStorage IdentityStorage;
        
        /// <summary>
        /// CommitteeManagementStorage storage calls.
        /// </summary>
        public CommitteeManagementStorage CommitteeManagementStorage;
        
        public SubstrateClientExt(System.Uri uri, Substrate.NetApi.Model.Extrinsics.ChargeType chargeType) : 
                base(uri, chargeType)
        {
            StorageKeyDict = new System.Collections.Generic.Dictionary<System.Tuple<string, string>, System.Tuple<Substrate.NetApi.Model.Meta.Storage.Hasher[], System.Type, System.Type>>();
            this.SystemStorage = new SystemStorage(this);
            this.RandomnessCollectiveFlipStorage = new RandomnessCollectiveFlipStorage(this);
            this.SchedulerStorage = new SchedulerStorage(this);
            this.AuraStorage = new AuraStorage(this);
            this.TimestampStorage = new TimestampStorage(this);
            this.BalancesStorage = new BalancesStorage(this);
            this.TransactionPaymentStorage = new TransactionPaymentStorage(this);
            this.AuthorshipStorage = new AuthorshipStorage(this);
            this.StakingStorage = new StakingStorage(this);
            this.HistoryStorage = new HistoryStorage(this);
            this.SessionStorage = new SessionStorage(this);
            this.AlephStorage = new AlephStorage(this);
            this.ElectionsStorage = new ElectionsStorage(this);
            this.TreasuryStorage = new TreasuryStorage(this);
            this.VestingStorage = new VestingStorage(this);
            this.UtilityStorage = new UtilityStorage(this);
            this.MultisigStorage = new MultisigStorage(this);
            this.SudoStorage = new SudoStorage(this);
            this.ContractsStorage = new ContractsStorage(this);
            this.NominationPoolsStorage = new NominationPoolsStorage(this);
            this.IdentityStorage = new IdentityStorage(this);
            this.CommitteeManagementStorage = new CommitteeManagementStorage(this);
        }
    }
}