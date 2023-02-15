# Plutonication
c# .NET 6 class class library for network TCP communication. Originaly designed for [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet).
- [Plutonication](#plutonication)
  - [What it does?](#what-it-does)
  - [How it is done?](#how-it-is-done)
  - [Usa cases](#usa-cases)
    - [Projects](#projects)
    - [Why this solution](#why-this-solution)
      - [I. Event driven architecture: Easy to use](#i-event-driven-architecture-easy-to-use)
      - [II. Connection with authentification by default](#ii-connection-with-authentification-by-default)
      - [III. Send](#iii-send)
      - [IV. Receive data](#iv-receive-data)
      - [V. Adjustable](#v-adjustable)
  - [Usage by Pure code examples](#usage-by-pure-code-examples)
    - [Server (CryptoWallet) Demo](#server-cryptowallet-demo)
    - [Client (dApp) Demo](#client-dapp-demo)
  - [Usage by object](#usage-by-object)
    - [`PlutoEventManager` class](#plutoeventmanager-class)
      - [Sending](#sending)
        - [Overview](#overview)
        - [Sending `PlutoMessage` object](#sending-plutomessage-object)
        - [Sending Ajuna.NetApi Method](#sending-ajunanetapi-method)
        - [Sending Async](#sending-async)
      - [Receiving](#receiving)
      - [Common setup](#common-setup)
      - [Processing of incoming messages](#processing-of-incoming-messages)
      - [Closing Connection](#closing-connection)
    - [`PlutoMessage` class](#plutomessage-class)
      - [Parameters](#parameters)
      - [How to create](#how-to-create)
      - [Basics](#basics)
      - [Variations](#variations)
    - [`CodeMessage` enum](#codemessage-enum)
    - [More flexible objects](#more-flexible-objects)
      - [PlutoManager](#plutomanager)
      - [ClientPlutoManager](#clientplutomanager)
      - [ServerPlutoManager](#serverplutomanager)

## What it does?
- Allow you to communicate between 2 applications on LAN using [TCP sockets](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/sockets/socket-services?source=recommendations) (e.g. dApp and crypto Wallet in case of [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet)).
- Designed to send transactions ([see `Method` class](https://github.com/ajuna-network/Ajuna.NetApi/blob/6976d251c3ae468b1190f13b0656ce54d94bf0af/Ajuna.NetApi/Model/Extrinsics/Method.cs) in [Ajuna.NetApi](https://github.com/ajuna-network/Ajuna.NetApi)), public keys and other data of your choice.
## How it is done?
Communication is established via [TCP protocol](https://en.wikipedia.org/wiki/Transmission_Control_Protocol).
- Wallet acts as client (e.g. [PlutoWallet](https://github.com/RostislavLitovkin/PlutoWallet))
- dApp acts as server
- Completely [**P2P**](https://en.wikipedia.org/wiki/Peer-to-peer). No third party server involved.
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

## Usage by Pure code examples
If you like learn&play with prepared demo:
1. Create C# cmd apps
2. Run Server code
3. Run Client code
4. Watch for their outputs
### Server (CryptoWallet) Demo
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
### Client (dApp) Demo

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

## Usage by object

### `PlutoEventManager` class
#### Sending
##### Overview
Data that you are sending are stored in object called [`PlutoMessage`](#plutomessage-class).
##### Sending `PlutoMessage` object
##### Sending Ajuna.NetApi Method
##### Sending Async
- all 'SendSomething' methods implement their async version
#### Receiving
Receiving messages is based on event handeling. On each incoming message:
1. Incoming message is added to `IncomingMessages` queue
2. `MessageReceived` event is triggered
#### Common setup
I supose you have instanciated `PlutoEventManager` and Establish
```cs
// PlutoEventManager manager = new PlutoEventManager();
// 
// ...

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
```
Common version with `PlutoEventManager`:
```cs
PlutoMessage msg = manager.IncomingMessages.Dequeue();
```
Naked `PlutoManager` method:
```cs
PlutoMessage msg = await manager.ReceiveMsgAsync();
```
#### Processing of incoming messages
Based on `msg.Indentifier` process message like so:
```cs
PlutoMessage msg = manager.IncomingMessages.Dequeue();
switch (msg.Identifier) {
    case MessageCode.Success:
        Console.WriteLine("Code: '{0}'. public key delivered!", msg.Identifier);
        break;
    //...
}
```
#### Closing Connection
### `PlutoMessage` class
#### Parameters
  1. MessageCode Identifier
  2. byte[] CustomData
#### How to create
#### Basics
```cs
PlutoMessage msgToSend = new PlutoMessage(MessageCode.XXX, dataToSend);
```
Now you are able to [send message](#sending) with `PlutoEventManager`. To receive see [receiving messages](#receiving) section.
#### Variations
You can use different types of pluto messages.
1. **String**
- typicaly used to send publickey from wallet to dApp, but can send any string type of data
```cs
var keyMsg = new PlutoMessage(MessageCode.PublicKey, "keySample");
```
1. **Byte[]**
- you can also serialize and send any type of data. Give it propper header you can process it when received
```cs
var byteMsg = new PlutoMessage(MessageCode.Method, new byte[3] {4,8,1});
```
1. **MessageCode**
- You can send `MessageCode` alone like so. This is typical for response messages.
```cs
var respose = new PlutoMessage(MessageCode.Success);
```
1. **Method**
- Don't forget you can [send Ajuna.NetApi Methods](#sending-method). Receiving Methods [here](#receiving).

### `CodeMessage` enum
`MessageCode` class serves as a header of messages. When receiving message we have to interpret incoming bytes that why header convention is essential in this type of network communication.
- We plan to add more codes in the future.
- You can implement your own new code.
- See table of basics MessageCodes bellow.

| MessageCode | Value |
|-------------|-------|
|PublicKey |0|
|Success | 1|
|Refused| 2|
|Method| 3|
|Auth| 4|
|FilledOut| 5|
> See [`PlutoMessage` class](#plutomessage-class) for common message format.
### More flexible objects
#### PlutoManager
#### ClientPlutoManager
#### ServerPlutoManager
