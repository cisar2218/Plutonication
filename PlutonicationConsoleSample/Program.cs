﻿using Plutonication;
using Substrate.NetApi;
using Substrate.NetApi.Generated.Model.sp_core.crypto;
using Substrate.NetApi.Generated.Model.sp_runtime.multiaddress;
using Substrate.NetApi.Generated.Storage;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;

Console.WriteLine("Hello, Plutonication!");

AccessCredentials ac = new AccessCredentials
{
    // Name of your dApp
    Name = "CSharp Plutonication console test",

    // Icon of your dApp
    Icon = "https://rostislavlitovkin.pythonanywhere.com/plutowalleticonwhite",

    // Address of Plutonication server
    // Feel free to use this one
    Url = "wss://plutonication-acnha.ondigitalocean.app/",
};

// Show the AccessCredentials to the wallet
Console.WriteLine(ac.ToUri());
Console.WriteLine();
Console.WriteLine("The following link opens you a QR code image that can be scanned by PlutoWallet: ");
Console.WriteLine("https://api.qrserver.com/v1/create-qr-code/?size=400x400&margin=40&data=" + ac.ToEncodedString());

// Get the Plutonication account
Account account = await PlutonicationDAppClient.InitializeAsync(
    // Include your access credentials
    ac,

    // Optinally add extra behaviour on when the wallet successfully connects
    (string receivedPubkey) => {
        Console.WriteLine("Received public key: " + receivedPubkey);
    }
);

Console.WriteLine("Account connected: " + account);

await Task.Delay(1000);

var substrateClient = new SubstrateClient(
    // Randomly chosen chain.
    // I generated the SubstrateClientExt according to this guide:
    // https://github.com/SubstrateGaming/Substrate.NET.API/wiki/Docs#scaffolding-with-substratenettoolchain
    new Uri("wss://ws.test.azero.dev"),
    ChargeTransactionPayment.Default()
);

// Do not forget to connect the SubstrateClient
await substrateClient.ConnectAsync();

var destinationAccountId = new AccountId32();
destinationAccountId.Create(Utils.GetPublicKeyFrom("5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY"));

var destinationMultiAddress = new EnumMultiAddress();
destinationMultiAddress.Create(0, destinationAccountId);

var amount = new BaseCom<U128>(1000000000000); // This is equivalent to 1 TZERO token (10^12 planks)

// Building the transfer Method
Method transfer = BalancesCalls.TransferAllowDeath(destinationMultiAddress, amount);

// Make a Balances.Transfer call
await substrateClient.Author.SubmitExtrinsicAsync(
    transfer,
    account, // Request a signature from the Plutonication account
    ChargeTransactionPayment.Default(), // No tip
    64, // Lifetime. For details, refer to: https://polkadot.js.org/docs/api/FAQ/#how-long-do-transactions-live
    CancellationToken.None
);

Console.WriteLine("Extrinsic submitted");

// Wait 1 second
await Task.Delay(1000);

byte[] message = new byte[] { 1, 2, 3 };

byte[] signature = await account.SignAsync(message);

Console.WriteLine("Message signed");

Console.WriteLine("Signature: " + Utils.Bytes2HexString(signature));

Console.ReadKey();
