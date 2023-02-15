# Plutonication
c# .NET 6 class class library for network TCP communication. Originaly designed for [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet).
## What it does?
- Allow you to communicate between 2 applications on LAN using [TCP sockets](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/sockets/socket-services?source=recommendations) (e.g. dApp and crypto Wallet in case of [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet)).
- Designed to send transactions ([see `Method` class](https://github.com/ajuna-network/Ajuna.NetApi/blob/6976d251c3ae468b1190f13b0656ce54d94bf0af/Ajuna.NetApi/Model/Extrinsics/Method.cs) in [Ajuna.NetApi](https://github.com/ajuna-network/Ajuna.NetApi)), public keys and other data of your choice.
## How it is done?
Communication is established via [TCP protocol](https://en.wikipedia.org/wiki/Transmission_Control_Protocol).
- Wallet acts as client (e.g. [PlutoWallet](https://github.com/RostislavLitovkin/PlutoWallet))
- dApp acts as server
- Completely [**P2P**](https://en.wikipedia.org/wiki/Peer-to-peer). Any third party server involved.
- Note that connection may not be completely secure. Keep that please in mind and **don't transfer any sensitive information via Plutonication**. We hope that with smart design choices of Plutonication and PlutoWallet there is no need to transfer any sensitive data.
## Usa cases
### Projects
1. You want to implement dApp (that communicate via TCP with Wallet)
2. You want to implement cryptowallet (that communicate via TCP) including:
3. You want to implement general TCP app which
### Why this solution
#### I. Event driven architecture: Easy to use
No need to setup receiving loop.
1. Setup methods that handle events which will pop up every time you receive message.
2. Establish connection with just one call and you are ready to go.
3. To send message just use `SendMessage(PlutoMessage msg)`.
4. To close connection just use `CloseConnection()` method.
#### II. Connection with authentification by default
- AcccessCredentials class enable you to convert information (which are must for connection) to URI. That way, it's easy to share. E.g. PlutoWallet use QR code scanning to access dApp's URI.
#### III. Send
- Each message has id header `MessageCode`(e.g. `MessageCode.PublicKey`). This way you determinate, how to interpret the data.
- Sending messages (byte[], string or transaction(=[see `Method` class](https://github.com/ajuna-network/Ajuna.NetApi/blob/6976d251c3ae468b1190f13b0656ce54d94bf0af/Ajuna.NetApi/Model/Extrinsics/Method.cs) in [Ajuna.NetApi](https://github.com/ajuna-network/Ajuna.NetApi))). This enable you to serialize any data of your choice and send them as `byte[]`.
- `SendMethod(Method m)` makes very convinient to send your method.
#### IV. Receive data
- As mentioned in **I.**: network stream receiving loop is setup automaticaly.
- Handle event `OnMessageReceived` with you custom function, which process the incoming message.
#### V. Adjustable
- To implement custom solution, you can adjust and implement your own variations. 
- If `PlutoEventManager` is not general or specific enought for your usecase: Extend `class PlutoManager` the way YOU like it. PlutoManager contains methods that designed to build custom abstraction.

## Usage
**! Note that Plutonication is still in development stage !** We are looking forward to deliver the best solution soon.
### Pure code examples:
If you like learn&play with prepared demo:
1. Create C# cmd apps
2. Run Server code
3. Run Client code
4. Watch for their outputs
#### Server (CryptoWallet) Demo
```cs
using Plutonication;

/*
    Serveside (dApp) code example
*/

// PART 1: Instanciate manager ------------------------------------------------
PlutoEventManager manager = new PlutoEventManager();

AccessCredentials ac = new AccessCredentials(
    address: PlutoManager.GetMyIpAddress(),
    port: 8080,
    key: "samplePassword"
);

// PART 2: Listen for incoming connections ------------------------------------
await manager.ListenSafeAsync(
    key: ac.Key, 
    port: ac.Port
);

// PART 3: Setup Events -------------------------------------------------------

// bind individual events to a function
manager.ConnectionEstablished += () =>
{
    Console.WriteLine("Connectin Established! :)");
};

manager.ConnectionRefused += () =>
{
    Console.WriteLine("Connectin Refused! :()");
    return;
};

manager.MessageReceived += () => {
    Console.WriteLine("message received!");

    // Pop oldest message from message queue
    PlutoMessage msg = manager.IncomingMessages.Dequeue();
    
    // Based on MessageCode process you message
    switch (msg.Identifier) {
        case MessageCode.PublicKey:
            Console.WriteLine("Public key received: " + msg.CustomDataToString());
            // send response:
            Task sendingSuccess = manager.SendMessageAsync(MessageCode.Success);
            break;
        // handle other message code as you wish
        // case MessageCode.XXX
        // ... process message object ...   
        // break;

        default:
            Console.WriteLine("Unknown code: " + msg.Identifier);
            Task sendingRefused = manager.SendMessageAsync(MessageCode.Refused);
            break;
    }
};


// PART 4-5: Start receiving messages -----------------------------------------
Task receiving = manager.SetupReceiveLoopAsync();

// PART 4-5: Send messages of your choice -------------------------------------
// For more options and details read documentation
Task sendingMsg = manager.SendMessageAsync(new PlutoMessage(MessageCode.PublicKey, "publicKeySample"));


// ... app is comunicating ... ------------------------------------------------
Console.ReadKey(); // after you press key on your board code goes on ...
// ... app is going to close connection ... -----------------------------------

// PART 4.5 (optional): stop receiving messages -------------------------------
manager.StopReceiveLoop();

// STEP 5: end connection -----------------------------------------------------
manager.CloseConnection();

/* 
Notes 
- if you strugle with events see handling and raising events c#
- if you strugle with connection establishment check firewall rules (can be problem 
on server side - this code). This case is specific for LAN. On localhost it should
work just fine.
- for more details see documentation on github: www.github.com/cisar2218/Plutonication
*/
```
#### Client (dApp) Demo

```cs
using System.Net;
using Plutonication;

/*
    Clientside (Wallet) code example
*/

// PART 1: Instanciate manager ------------------------------------------------
PlutoEventManager manager = new PlutoEventManager();


// PART 1.5: Create AccessCredentials object
/* 
    ! Credentials params HAVE TO MATCH server side
    - give Uri object generated from Serverside dApp; accessCredentials.ToUri();
    - or manualy set properties (ip address 127.0.0.1 for localhost testing)
*/
// A. Variant with URI
/* AccessCredentials ac = new AccessCredentials( uri ); */

// B. Manualy set credentials
AccessCredentials ac = new AccessCredentials(
    address: IPAddress.Parse("127.0.0.1"),
    port: 8080,
    key: "samplePassword" 
);

// PART 2: Connect to remote server (dApp) ------------------------------------
await manager.ConnectSafeAsync(ac);

// PART 3: Setup Events -------------------------------------------------------

// bind individual events to a function
manager.ConnectionEstablished += () =>
{
    Console.WriteLine("Connectin Established! :)");
};

manager.ConnectionRefused += () =>
{
    Console.WriteLine("Connectin Refused! :()");
    return;
};


manager.MessageReceived += () => {
    Console.WriteLine("message received!");

    // Pop oldest message from message queue
    PlutoMessage msg = manager.IncomingMessages.Dequeue();
    
    // Based on MessageCode process you message
    switch (msg.Identifier) {
        case MessageCode.Success:
            Console.WriteLine("Code: '{0}'. public key delivered!", msg.Identifier);
            break;
        // handle other message code as you wish
        // case MessageCode.XXX
        // ... process message object ...   
        // break;

        default:
            Console.WriteLine("Unknown code: " + msg.Identifier);
            break;
    }
};


// PART 4-5: Start receiving messages -----------------------------------------
Task receiving = manager.SetupReceiveLoopAsync();

// PART 4-5: Send messages of your choice -------------------------------------
/* Now you are able to send messages of your choice. For details see documentation.
    manager.SendMessage(...);
    await manager.SendMessageAsync(...);
    manager.SendMethod(...);
    await manager.SendMethodAsync(...);
    ...
*/

// ... app is comunicating ... ------------------------------------------------
Console.ReadKey(); // after you press key on your board code goes on ...
// ... app is going to close connection ... -----------------------------------

// PART 4.5 (optional): stop receiving messages -------------------------------
manager.StopReceiveLoop();

// STEP 5: end connection -----------------------------------------------------
manager.CloseConnection();

/* 
Notes 
- if you strugle with events see handling and raising events c#
- if you strugle with connection establishment check firewall rules (can be problem 
on server side - this code). This case is specific for LAN. On localhost it should
work just fine.
- for more details see documentation on github: www.github.com/cisar2218/Plutonication
*/
```
### Server specific code
- Server in context of crypto is dApp
### Client specific code
- Client in context of crypto is Wallet
### Sending
### Receiving
### Close Connection
