# Plutonication
c# .NET 6 class class library for network communication originaly designed for [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet).
## What it does?
- Allow you to communicate between dApp and crypto Wallet.
- Designed to send transactions (Methods), public keys and other data of your choice.
## How it is done?
Communication is established via [TCP protocol](https://en.wikipedia.org/wiki/Transmission_Control_Protocol).
- Wallet acts as client (e.g. [PlutoWallet](https://github.com/RostislavLitovkin/PlutoWallet))
- dApp acts as server
- Completely [**P2P**](https://en.wikipedia.org/wiki/Peer-to-peer). Any third party server involved.
- Note that connection is not completely secure. Keep that please in mind and **don't transfer any sensitive information via Plutonication**. We hope that with smart design choices of Plutonication and PlutoWallet there is no need to transfer any sensitive data.
## Usage
**! Note that Plutonication is still in development stage !** We are looking forward to deliver the best solution soon.
### Guided walkthrought
1. Start server at dApp:
```c#
// TODO code example
```
2. Share information about connection. We suggest defined URI via QR code.
```c#
// TODO code example
```
3. Connect Wallet to dApp:
4. Send messages and transactons
```c#
// TODO code example
```
5. Receive messages and transactions
```c#
// TODO code example
```
6. Message processing
```c#
// TODO code example
```
7. Close connestion
```c#
// TODO code example
```
### Pure Code Example
#### Wallet (client)
```c#
// Pluto Manager will manage all communication for you
ClientPlutoManager plutoManager = ...;

// Connect to server on LAN via ip, port and auth
plutoManager.Connect(...);

// Create message to be send
PlutoMessage plutoMsg = new PlutoMessage(...);
await plutoManager.SendMessageAsync(plutoMsg);
await plutoManager.SendMethod(new Method);


PlutoMessage incomingMsg = await plutoManager.ReceiveMessageAsync();
switch(incomingMsg.Identifier) 
{
  case MessageCode.Method:
    // Handle message here
    // incoming msg is Method
    Method method = incommingMessage.GetMethod();
    // ... process method here ...
    break;
  // add other message code handling that your app implements
  case MessageCode.Success:
    // ... handle ...
    break;
  default:
    // ... handle unknown message code ...
    break;
}

// Close Connection
plutoManager.CloseConnection();
```
#### dApp (server)
```c#
// TODO code example
```
### Detailed documentation of objects and methods
#### PlutoManager
##### `SendMessage(PlutoMessage msg)`
##### `SendMethod(Method method)`
##### `ReceiveMessage()`
### ClientPlutoManager
### ServerPlutoManager
#### PlutoMessage
- MessageCode
- CustomData
