using System;
using Chaos.NaCl;
using Newtonsoft.Json;
using Plutonication;
using Schnorrkel;
using SocketIOClient.Transport;
using Substrate.NET.Wallet;
using Substrate.NET.Wallet.Keyring;
using Substrate.NetApi;
using Substrate.NetApi.Generated.Model.sp_core.crypto;
using Substrate.NetApi.Generated.Model.sp_runtime.generic.unchecked_extrinsic;
using Substrate.NetApi.Generated.Model.sp_runtime.multiaddress;
using Substrate.NetApi.Generated.Storage;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using Substrate.NetApi.Model.Types.Primitive;

namespace PlutonicationTests;

public class Plutonication
{
    private static readonly Meta META = new Meta() { name = "Automated test wallet" };

    AccessCredentials ac = new AccessCredentials
    {
        Name = "C# Plutonication automated test",
        Icon = "https://rostislavlitovkin.pythonanywhere.com/plutowalleticonwhite",
        Url = "wss://plutonication-acnha.ondigitalocean.app/",
    };

    SubstrateClient substrateClient;

    public Account GetAccount()
    {
        var keyring = new Substrate.NET.Wallet.Keyring.Keyring();

        // Soft/Hard derivation is still not existant, so I used random account instead of Alice
        // https://github.com/SubstrateGaming/Substrate.NET.Wallet/pull/11
        Wallet wallet = keyring.AddFromMnemonic(
            "flight rent steel toddler casino party exact duck square segment charge swap",
            META,
            Substrate.NetApi.Model.Types.KeyType.Sr25519
        );

        return wallet.Account;
    }

    [SetUp]
    public async Task SetupAsync() {
        substrateClient = new SubstrateClient(
            // Randomly chosen chain.
            // I generated the SubstrateClientExt according to this guide:
            // https://github.com/SubstrateGaming/Substrate.NET.API/wiki/Docs#scaffolding-with-substratenettoolchain
            new System.Uri("wss://ws.test.azero.dev"),
            ChargeTransactionPayment.Default()
        );

        await substrateClient.ConnectAsync();
    }

    [Test]
    public void AccessCredentialsToUri()
    {
        Console.WriteLine(ac.ToUri());

        Assert.That(ac.ToUri().ToString() == $"plutonication:?url=wss%3A%2F%2Fplutonication-acnha.ondigitalocean.app%2F&key={ac.Key}&name=C%23 Plutonication automated test&icon=https%3A%2F%2Frostislavlitovkin.pythonanywhere.com%2Fplutowalleticonwhite");
    }

    [Test]
    public async Task CommunicationBetweenDAppAndWalletAsync()
    {
        Account account = new Account();

        Account automatedTestsAccount = GetAccount();

        await Task.WhenAll(
            Task.Run(async () =>
            {
                account = await PlutonicationDAppClient.InitializeAsync(ac, (string _) => { });
            }),
            Task.Run(async () =>
            {
                // Wait 3 seconds so that the PlutonicationDAppClient has got enough time to connect
                await Task.Delay(3000);

                await PlutonicationWalletClient.InitializeAsync(
                    ac,
                    automatedTestsAccount.Value,
                    async (unCheckedExtrinsic, runtime) => {
                        //
                        // You can use the unCheckedExtrinsic and runtime to showcase the
                        // info about the transaction to the user before signing it.
                        //

                        // Get the Extrinsic Payload and sign it
                        Substrate.NetApi.Model.Extrinsics.Payload payload = unCheckedExtrinsic.GetPayload(runtime);
                        unCheckedExtrinsic.AddPayloadSignature(await automatedTestsAccount.SignPayloadAsync(payload));

                        var signerResult = new SignerResult
                        {
                            // Id of the signature
                            id = 1,
                            signature = Utils.Bytes2HexString(
                                // This 1 means the signature is using Sr25519
                                new byte[1] { 1 }
                                // The signature
                                .Concat(unCheckedExtrinsic.Signature).ToArray()
                            ).ToLower(),
                        };

                        // Send the payload signature to the dApp.
                        await PlutonicationWalletClient.SendPayloadSignatureAsync(signerResult);
                    },
                    async (raw) => {
                        //
                        // You can use the raw to show the message to the user before signing it.
                        //

                        if (raw.type != "bytes")
                        {
                            // Unsupported
                            return;
                        }

                        byte[] rawMessageBytes = Utils.HexToByteArray(raw.data);

                        var signerResult = new SignerResult
                        {
                            // Id of the signature
                            id = 1,
                            signature = Utils.Bytes2HexString(
                                await automatedTestsAccount.SignAsync(rawMessageBytes)
                            ).ToLower(),
                        };

                        await PlutonicationWalletClient.SendRawSignatureAsync(signerResult);
                    }
                );
            })
        );

        Assert.That(account.Value == automatedTestsAccount.Value);

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
            64, // Lifetime. For details, refer to: https://polkadot.js.org/docs/api/FAQ/#how-long-do-transactions-live
            CancellationToken.None);

        // Wait 1 second
        await Task.Delay(1000);

        byte[] message = new byte[] { 1, 2, 3 };

        byte[] signature = await account.SignAsync(message);

        Assert.That(await account.VerifyAsync(signature, account.Bytes, message));
    }
}

