# Plutonication
c# .NET 6 class class library for network TCP communication. Originaly designed for [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet).
- [Plutonication](#plutonication)
  - [Installation](#installation)
  - [What it does?](#what-it-does)
  - [How it is done?](#how-it-is-done)
  - [Usecases](#usecases)
    - [Projects](#projects)
    - [Why this solution](#why-this-solution)
      - [I. Event driven architecture that is easy to use](#i-event-driven-architecture-that-is-easy-to-use)
      - [II. Connection with authentification by default](#ii-connection-with-authentification-by-default)
      - [III. Send data easily](#iii-send-data-easily)
      - [IV. Receive \& process data](#iv-receive--process-data)
      - [V. Multiplatform solution](#v-multiplatform-solution)
      - [VI. Adjustable](#vi-adjustable)
  - [Usage by pure code examples](#usage-by-pure-code-examples)
    - [Server (dApp) Demo](#server-dapp-demo)
    - [Client (CryptoWallet) Demo](#client-cryptowallet-demo)
  - [Usage by object](#usage-by-object)
    - [`PlutoEventManager` class](#plutoeventmanager-class)
      - [Try to establish connection](#try-to-establish-connection)
        - [Server (dApp)](#server-dapp)
        - [Client (Wallet)](#client-wallet)
      - [Sending](#sending)
        - [Overview](#overview)
        - [Sending `PlutoMessage` object](#sending-plutomessage-object)
        - [Sending `MessageCode` object alone](#sending-messagecode-object-alone)
        - [Sending Ajuna.NetApi Method](#sending-ajunanetapi-method)
        - [Sending Async](#sending-async)
      - [Receiving](#receiving)
        - [Common setup](#common-setup)
      - [Processing of incoming messages](#processing-of-incoming-messages)
      - [List of events](#list-of-events)
        - [`ConnectionEstablished` event and `ConnectionRefused` event](#connectionestablished-event-and-connectionrefused-event)
        - [`SetupReceiveLoopAsync()` method](#setupreceiveloopasync-method)
        - [`MessageReceived` event](#messagereceived-event)
      - [Closing connection](#closing-connection)
    - [`PlutoMessage` class](#plutomessage-class)
      - [How to create](#how-to-create)
        - [Variations](#variations)
    - [`AccessCredentials` class](#accesscredentials-class)
    - [`CodeMessage` enum](#codemessage-enum)
    - [More flexible objects](#more-flexible-objects)
      - [PlutoManager](#plutomanager)
      - [ClientPlutoManager](#clientplutomanager)
      - [ServerPlutoManager](#serverplutomanager)
## Installation
Install Plutonication package in LTS from nuget. See [Nuget](https://www.nuget.org/packages/Plutonication).
## What it does?
- Allow you to communicate between 2 applications on LAN using [TCP sockets](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/sockets/socket-services?source=recommendations) (dApp and crypto Wallet in case of [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet)).
- Designed to send transactions ([see `Method` class](https://github.com/ajuna-network/Ajuna.NetApi/blob/6976d251c3ae468b1190f13b0656ce54d94bf0af/Ajuna.NetApi/Model/Extrinsics/Method.cs) in [Ajuna.NetApi](https://github.com/ajuna-network/Ajuna.NetApi)), public keys and other data of your choice.
## How it is done?
Communication is established via [TCP protocol](https://en.wikipedia.org/wiki/Transmission_Control_Protocol).
- Wallet acts as client (e.g. [PlutoWallet](https://github.com/RostislavLitovkin/PlutoWallet))
- dApp acts as server
- Completely [**P2P**](https://en.wikipedia.org/wiki/Peer-to-peer). No third party server involved.
- Note that connection may not be completely secure. Keep that please in mind and **don't transfer any sensitive information via Plutonication**. We hope that with smart design choices of Plutonication and PlutoWallet there is no need to transfer any sensitive data.
## Usecases
### Projects
1. You want to implement dApp (that communicate via TCP with Wallet)
    - Send *transactions* into cryptowallet where you can sign them.
    - Share *publickey* with dApp.
    - Send responses.
    - Also possible to send other data, see [sending](#sending).
2. You want to implement cryptowallet (that communicate via TCP) with dApps.
3. You want to implement general app that has on one hand high level of abstraction of TCP communication but on the other hand is extensible.
### Why this solution
#### I. Event driven architecture that is easy to use
1. Setup methods that handle [`MessageReceived`](#messagereceived-event) event which will pop up every time you receive message.
2. Establish connection with just [one call](#try-to-establish-connection) and you are ready to go.
3. To send message just use `SendMessage(PlutoMessage msg)`.
4. To close connection just use `CloseConnection()` method.
#### II. Connection with authentification by default
- [`AcccessCredentials`](#accesscredentials-class) class enable you to convert information (that are must for connection) to URI. That way, it's easy to share. E.g. PlutoWallet use QR code scanning to access dApp's URI.
#### III. Send data easily
- Each message has id header `MessageCode` (e.g. `MessageCode.PublicKey`). This way you determinate, how to interpret the received data.
- Sending messages (byte[], string or transaction ([see `Method` class](https://github.com/ajuna-network/Ajuna.NetApi/blob/6976d251c3ae468b1190f13b0656ce54d94bf0af/Ajuna.NetApi/Model/Extrinsics/Method.cs) in [Ajuna.NetApi](https://github.com/ajuna-network/Ajuna.NetApi))). Optionaly you can serialize any data of your choice and send them as `byte[]`.
- `SendMethod(Method m)` makes very convinient to send methods.
#### IV. Receive & process data
- As mentioned in [above](#iii-send-data-easily): network stream receiving loop is setup automaticaly.
- Handle event `OnMessageReceived` with you custom function, which process the incoming message.
#### V. Multiplatform solution
- Plutonication can be implemented on any device that run .NET. This include desktop programs (Windows, MacOS, Linux), mobile apps (android, IOS), smart watches, smart TVs, IOT devices and other.
#### VI. Adjustable
- To implement custom solution you can adjust and implement your own variations. 
- If `PlutoEventManager` is not general or specific enought for your usecase: Extend `class PlutoManager` the way YOU like it. PlutoManager contains methods that designed to build custom abstraction.

## Usage by pure code examples
If you like learn&play with prepared demo:
1. Create 2 .NET C# cmd apps from our examples bellow
2. Run Server code
3. Run Client code
4. Learn, play & develop

### Server (dApp) Demo
Create custom dApp using `PlutoEventManager`. Example of usecase: [PlutoWallet](https://github.com/RostislavLitovkin/PlutoWallet) send its *publickey* to dApp. App then can read information about Wallet. DApp send custom *transaction* to PlutoWallet. In [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet) you can *sign* this *transaction* with *privatekey* (or omit).
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
    Console.WriteLine("Connection Established! :)");
};

manager.ConnectionRefused += () =>
{
    Console.WriteLine("Connection Refused! :(");
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
            Console.WriteLine("Message with unknown code received: " + msg.Identifier);
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
Console.ReadKey(); // after you press key on your keyboard code goes on ...
// ... app is going to close connection ... -----------------------------------

// PART 4.5 (optional): stop receiving messages -------------------------------
manager.StopReceiveLoop();

// STEP 5: end connection -----------------------------------------------------
manager.CloseConnection();

/* 
Notes 
- If you strugle with events see handling and raising events C#.
- If you strugle with connection establishment check firewall rules (can be problem 
on server side - this code). This case is specific for LAN. On localhost it should
works just fine.
- for more details see documentation on github: www.github.com/cisar2218/Plutonication
*/
```

### Client (CryptoWallet) Demo
Use this code to test serverside behaviour or implement apps that support comunication between Wallet and dApp. [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet) uses similar code to pair itself with dApps (by scanning QR code - generated by dApp - where [URI of AccessCredential](#parse-to-uri) is encoded). PlutoWallet at first share its *publickey* to give dApp information abou itself.
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
    Console.WriteLine("Connection Established! :)");
};

manager.ConnectionRefused += () =>
{
    Console.WriteLine("Connection Refused! :(");
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
            Console.WriteLine("Message with unknown code received: " + msg.Identifier);
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
on server side). This case is specific for LAN. On localhost it should
work just fine.
- for more details see documentation on github: www.github.com/cisar2218/Plutonication
*/
```

## Usage by object

### `PlutoEventManager` class
`PlutoEventManager` class will be your main class used from Plutonication package if you want to build simple dApp that communicate with PlutoWallet. We recommend run and play with [code examples](#usage-by-pure-code-examples) and then study additional details in documentation. 
> It has the highest level of abstraction, if you want more flexible solution, see [more flexible objects](#more-flexible-objects). 
#### Try to establish connection
To establish connection instantiate `PlutoEventManager` and then make call specific for your usecase ([client (wallet)](#client-wallet) / [server (dApp)](#server-dapp)).
```cs
PlutoEventManager manager = new PlutoEventManager();
// ... make your connection call bellow ...
```
##### Server (dApp)
To start listening as server use async method bellow:
```cs
string key = "samplePassword";
int port = 8080;
await manager.ListenSafeAsync( key: key, port: port );
```
Server will be started at port that you have specified. Client can connect with password (which matches `key`). Optionaly you can specify timeout param which default value is 1 minute.
```cs
int timeoutMiliseconds = 3*60_000; // 3 minutes
await manager.ListenSafeAsync("samplePassword", 8080, timeoutMiliseconds);
```
##### Client (Wallet)
- Make sure server is running before you attempt connection.
- Make sure that address, port and key is matching.
- To connect with server use async method bellow.
```cs
string key = "samplePassword";
int port = 8080;
await manager.ConnectSafeAsync( key: key, port: port );
```
[`ConnectionEstablished`](#connectionestablished-event-and-connectionrefused-event) event is raised when connection established. [`ConnectionRefused`](#connectionestablished-event-and-connectionrefused-event) event is raised when connection refused.
#### Sending
- Make sure connection is [established](#connectionestablished-event-and-connectionrefused-event). And other side is [ready](#common-setup) to receive messages.
- To send messages 2 main methods are used: `sendMessage()`, `sendMethod()`. See they variations bellow to find your usecase.
##### Overview
Data that you are sending are stored in object called [`PlutoMessage`](#plutomessage-class).
##### Sending `PlutoMessage` object
##### Sending `MessageCode` object alone 
##### Sending Ajuna.NetApi Method
##### Sending Async
- all 'SendSomething' methods implement their async version
#### Receiving
Receiving messages is based on event handeling. On each incoming message:
1. Incoming message is added to `IncomingMessages` queue
2. `MessageReceived` event is triggered
##### Common setup
I supose you have instanciated `PlutoEventManager` and [established connection](#connectionestablished-event-and-connectionrefused-event).
1. Set which function will execute on message received event.
2. Deque and process message (inside your custom method which handles incoming messages)
3. Start receiving messages by calling `SetupReceiveLoopAsync()` method.
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

Task receiving = manager.SetupReceiveLoopAsync();
```
Messages are stored in queue. By calling the event bellow, put the oldest message out of the queue.
```cs
PlutoMessage msg = manager.IncomingMessages.Dequeue();
```

#### Processing of incoming messages
> Messages are in form of [`PlutoMessage` object](#plutomessage-class).
 
To interpret data in form of `byte[]`, you need to read the Identifier member of message. Based on `msg.Indentifier` process message like so:
```cs
PlutoMessage msg = manager.IncomingMessages.Dequeue();
switch (msg.Identifier) {
    // handle short responses
    case MessageCode.Success:
    case MessageCode.Refused:
    case MessageCode.FilledOut:
        Console.WriteLine("Code: '{0}'!", msg.Identifier);
        // ... process message here ...
        break;
    // handle incoming string data
    case MessageCode.PublicKey:
        Console.WriteLine("Code: '{0}'. public key delivered!", msg.Identifier);
        string publicKey = msg.CustomDataToString();
        // ... process publiKey here ...
        break;
    // handle incoming methods to be sign
    case MessageCode.Method:
        Console.WriteLine("Code: '{0}'. Method delivered!", msg.Identifier);
        Method m = msg.GetMethod();
        // ... process method here ...
        break;
    //...

    // handle unknown message codes
    dafault:
        Console.Writeline("Unknown message code + ", msg.Indetifier);
        break;
}
```
#### List of events
##### `ConnectionEstablished` event and `ConnectionRefused` event
Before you call method which [tries to establish connection](#try-to-establish-connection) with other app setup these events.
```cs
// bind individual events to a function
// this event will fire when apps are connected
manager.ConnectionEstablished += () =>
{
    Console.WriteLine("Connection Established! :)");
    // ... execude any code here ...
};

// this event will fire when other side refuse connection
manager.ConnectionRefused += () =>
{
    Console.WriteLine("Connection Refused! :(");
    // ... execude any code here ...
};
```
You don't have to use lambda function ofc.:
```cs
manager.ConnectionRefused += MyHandleFunction;

// ... 

public void MyHandleFunction() {
    Console.WriteLine("Connection Refused! :(");
    //... handle here ...
}
```
> Don't know about events? Check [official documentation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-subscribe-to-and-unsubscribe-from-events) about events
##### `SetupReceiveLoopAsync()` method
##### `MessageReceived` event
Before you call method which [receives messages](#setupreceiveloopasync-method) setup this event:
```cs
manager.MessageReceived += () => {
    // ... handle message here ...
};
```
See [Common setup](#common-setup) and [complete examples](#usage-by-pure-code-examples) for complete examples.
> Don't know about events? Check [official documentation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-subscribe-to-and-unsubscribe-from-events) about events
#### Closing connection
To close connection manually call `PlutoEventManager`'s method `CloseConnection`. From now on connection will be closed.
```cs
PlutoEventManager manager = new PlutoEventManager();
// ... 
manager.CloseConnection();
```
### `PlutoMessage` class
- Class that make abstraction over data that are send and received.
- **Properties**:
  - `MessageCode Identifier` - header which is used to identify type of message received.
  - `Byte[] CustomData` - message itself serialized to bytes
- **Methods**:
  - `string CustomDataToString()` - used to convert received data to string (e.g. publickey).
  - `byte[] ToByteArray()` - used to align `Identifier` and `CustomData` together into one `byte` array before sending. User typicaly don't need to use this method.
  - `Method GetMethod()` - When received `CustomData` are serialized `Method`, you can convert them with this method. E.g.: 
    ```cs
    if (incomingMessage.Identifier == MessageCode.Method) {
        Method m = incomingMessage.GetMethod();
        // ...
    }
    ```
    #### Parameters
  1. MessageCode Identifier
  2. byte[] CustomData
#### How to create
```cs
PlutoMessage msgToSend = new PlutoMessage(MessageCode.XXX, dataToSend);
```
Now you are able to [send message](#sending) with `PlutoEventManager`. To receive see [receiving messages](#receiving) section.
##### Variations
You can use different types of pluto messages.
1. **Method**
- You can [send Ajuna.NetApi Methods](#sending-method) with EZ. Receiving Methods [here](#receiving).
- Create method:
```cs
// define method with your values
byte moduleIndex = 0;
byte callIndex = 1;
byte[] parameters = new byte[3] {4,8,1};

Method method = new Method(moduleIndex, callIndex, parameters);
// method with empty params also possible like this: 
Method methodEmptyParams = new Method(moduleIndex, callIndex, new byte[0]);

// send with PlutoEventManager or PlutoManager class
manager.SendMethod(method);
```
Where manager is [`PlutoEventManager`](#plutoeventmanager-class).
- Unpack method:
```cs
// incomingMessage is message you have received earlier
PlutoMessage incomingMessage;
// is incomingMessage method?
if (incomingMessage.Identifier == MessageCode.Method) {
    // YES, ITS METHOD
    Method m = incomingMessage.GetMethod();
    // ... process method ...
} else { /* IS NOT METHOD */ }
```

2. **String** (publickey)
- typicaly used to send publickey from wallet to dApp, but can send any `string` data.
- Create `PlutoMessage` with containing *publickey*
```cs
string publickey = "YourKeyGoesHere";
var keyMsg = new PlutoMessage(MessageCode.PublicKey, publickey);
// to send with PlutoEventManager / PlutoManager
manager.SendMessage(keyMsg);
```
- Unpack `PlutoMessage` with *publickey*
```cs
// incomingMessage is message you have received earlier
PlutoMessage incomingMessage;
// is incomingMessage method?
if (incomingMessage.Identifier == MessageCode.PublicKey) {
    // YES, ITS PUBLICKEY
    string publickey = incomingMessage.CustomDataToString();
    // ... process publickey here ...
} else { /* IS NOT PUBLICKEY */ }
```
3. **Byte[]**
- you can also serialize and send any type of data. Give it propper header you can process it when received
```cs
var byteMsg = new PlutoMessage(MessageCode.Method, new byte[] {4,8,1});
```
4. **MessageCode**
- You can send `MessageCode` alone like so. This is typical for response messages.
```cs
var respose = new PlutoMessage(MessageCode.Success);
```
- Handle responses like so
```cs
// incomingMessage is message you have received earlier
PlutoMessage incomingMessage;

if (incomingMessage.Identifier == MessageCode.Success) {
    // ... handle response here ...
} else if (incomingMessage.Identifier == MessageCode.Refused){
    // ... handle response here ...
} else if ( /* other codes */ ){
    // ... handle response here ...
}
```
### `AccessCredentials` class
- This class is used to store information about dApp that are needed for connection.
- **Properties**:
  - `string Address` - LAN ip address in string format (where to connect).
  - `int Port` - Port number in int format (where to connect).
  - `string Key` - Acts like password or auth token. Has to match on server/client side. Can be generated or setted manualy.
  - `string Name` (optional) - dApp name. This way wallet with dApp's `AccessCredentials` object know name of dApp without connecting.
  - `string Icon` (optional) - Url that leads to image of dApp. E.g icon.
- **Methods**:
  - `static string GenerateKey(int keyLen=30)` - generates key for you. THe `keyLen` argument is optional (default value is 30).
  - `Uri ToUri()` - Converts properties of `AccessCredentials` object to URI address.
    - Uri address can be (and in case of PlutoWallet is) encoded to QR code.
    - `AccessCredentials` has constructor that accepts `Uri` object:
        ```cs
        var credentials = AccessCredentials(Uri uri);
        ```

### `CodeMessage` enum
`MessageCode` class serves as a header of messages. When receiving message we have to interpret incoming bytes that why header convention is essential in this type of network communication.
- We plan to add more codes in the future.
- You can implement your own new code.
- See table of basics MessageCodes bellow.

| MessageCode | Value | Convinient use |
|-------------|-------|------|
|PublicKey |0| *This* message contains (string) public key. |
|Success | 1| Response that confirms succesful operation. |
|Refused| 2| Response that your message was refused. |
|Method| 3| *This* message contains Ajuna Method. |
|Auth| 4| *This* message contains key. |
|FilledOut| 5| Other side is overloaded. Try send message later. |
> See [`PlutoMessage` class](#plutomessage-class) for common message format.
### More flexible objects
#### PlutoManager
Naked `PlutoManager` method:
```cs
PlutoMessage msg = await manager.ReceiveMsgAsync();
```
#### ClientPlutoManager
#### ServerPlutoManager
