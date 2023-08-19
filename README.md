# Plutonication

Communications protocol that enables seamless interactions between dApps and wallets across all platforms.

Establishing a connection is as easy as scanning a QR code.

This is a c# version.

# Installation

Nuget package: https://www.nuget.org/packages/Plutonication
```
dotnet add package Plutonication
```

# Other versions

- Javascript/Typescript - in works
- Kotlin - planned
- Swift - planned

# Use of Plutonication

The overall structure of Plutonication is designed to be as little intrusive as possible.

If you are building a dApp, you will want to interact with `PlutonicationDAppClient` static class.

Here is how:
```C#
use Plutonication
use Substrate.NetApi

// Access credentials are used to show correct info to the wallet.
AccessCredentials ac = new AccessCredentials
{
    // Plutonication endpoint url - leave this as is
    Url = "wss://plutonication-53tvi.ondigitalocean.app/plutonication",

    // Name of the dApp
    Name = "Galaxy Logic Game",

    // dApp icon
    Icon = "https://rostislavlitovkin.pythonanywhere.com/logo",

    // Unique key used for differentiating - leave this as is
    Key = AccessCredentials.GenerateKey(),
};

// Create a client that connects to the RPC node
// this uses Substrate.NetApi
SubstrateClient substrateClient = new SubstrateClient(
    // RPC node endpoint
    new Uri("wss://rococo-asset-hub-rpc.polkadot.io"),
    Substrate.NetApi.Model.Extrinsics.ChargeTransactionPayment.Default());

await PlutonicationDAppClient.InitializeAsync(
    ac,
    pubkey =>
    {
        // Do something with the pubkey,
        // For example show it to the user.
        Console.WriteLine(pubkey);
    },
    substrateClient);

// Use AccessCredentials to generate a link for connecting.
// Use this link to generate a correct QR code.
// You may use a package like https://github.com/Redth/ZXing.Net.Maui for QR code generation.
qrCode.Value = ac.ToUri().ToString();

// Asure that the pubkey has been received
// ..

// Now you can send payloads
// Do not forget to import other types from Substrate.NetApi
EnumMultiAddress mint_to = new EnumMultiAddress();
var account32 = new AccountId32();
account32.Create(Utils.GetPublicKeyFrom(pubkey));
mint_to.Create(MultiAddress.Id, account32);

System.Collections.Generic.List<byte> parameters = new List<byte>();

// collectionId
parameters.AddRange(new U32(7).Encode());

// itemId
parameters.AddRange(new U32(0).Encode());

// mintTo
parameters.AddRange(mint_to.Encode());

// witnessData
parameters.AddRange(new byte[0] { });

// Send the payload to the wallet for signing.
await PlutonicationDAppClient.SendPayloadAsync(52, 3, parameters.ToArray());
```

# Problem / Motivation
Currently, there is no way to connect a wallet to more exotic devices, like gaming console and wearables.

# Projects utilising Plutonication

- https://github.com/RostislavLitovkin/galaxylogicgamemaui

Feel free to add your own project by making a PR.

# Inspiration
- https://walletconnect.com/
