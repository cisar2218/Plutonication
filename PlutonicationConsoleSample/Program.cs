// See https://aka.ms/new-console-template for more information
using Plutonication;
using Substrate.NET.Wallet;
using Substrate.NetApi;
using Substrate.NetApi.Generated;
using Substrate.NetApi.Generated.Model.sp_core.crypto;
using Substrate.NetApi.Generated.Model.sp_runtime.multiaddress;
using Substrate.NetApi.Generated.Storage;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;

Console.WriteLine("Hello, World!");

AccessCredentials ac = new AccessCredentials
{
    Name = "CSharp Plutonication console test",
    Url = "wss://plutonication-acnha.ondigitalocean.app/",
    Key = "134",
};

Console.WriteLine(ac.ToUri());

Account account = await PlutonicationDAppClient.InitializeAsync(ac, (string _) => { });

Console.WriteLine("Account connected: " + account);

await Task.Delay(1000);

var substrateClient = new SubstrateClient(
    // Randomly chosen chain.
    // I generated the SubstrateClientExt according to this guide:
    // https://github.com/SubstrateGaming/Substrate.NET.API/wiki/Docs#scaffolding-with-substratenettoolchain
    new Uri("wss://ws.test.azero.dev"),
    ChargeTransactionPayment.Default()
);

await substrateClient.ConnectAsync();

var destinationAccountId = new AccountId32();
destinationAccountId.Create(Utils.GetPublicKeyFrom("5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY"));

var destinationMultiAddress = new EnumMultiAddress();
destinationMultiAddress.Create(0, destinationAccountId);

var amount = new BaseCom<U128>(1000000000000); // This is equivalent to 1 TZERO token (10^12 planks)

// Building the transfer Method
Method transfer = BalancesCalls.Transfer(destinationMultiAddress, amount);

// Make a Balances.Transfer call
await substrateClient.Author.SubmitExtrinsicAsync(
    transfer,
    account, // Request a signature from this account
    ChargeTransactionPayment.Default(), // No tip
    0, // Lifetime. For details, refer to: https://polkadot.js.org/docs/api/FAQ/#how-long-do-transactions-live
    CancellationToken.None);

Console.WriteLine("Extrinsic submitted");

Console.ReadKey();
