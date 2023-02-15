# Plutonication
c# .NET 6 class class library for network TCP communication. Originaly designed for [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet).
## What it does?
- Allow you to communicate between 2 applications on LAN using [TCP sockets](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/sockets/socket-services?source=recommendations) (e.g. dApp and crypto Wallet in case of [PlutoWallet project](https://github.com/RostislavLitovkin/PlutoWallet)).
- Designed to send transactions (Methods), public keys and other data of your choice.
## How it is done?
Communication is established via [TCP protocol](https://en.wikipedia.org/wiki/Transmission_Control_Protocol).
- Wallet acts as client (e.g. [PlutoWallet](https://github.com/RostislavLitovkin/PlutoWallet))
- dApp acts as server
- Completely [**P2P**](https://en.wikipedia.org/wiki/Peer-to-peer). Any third party server involved.
- Note that connection is not completely secure. Keep that please in mind and **don't transfer any sensitive information via Plutonication**. We hope that with smart design choices of Plutonication and PlutoWallet there is no need to transfer any sensitive data.
## Usa cases
### Projects
1. You want to implement cryptowallet (that communicate via TCP) including:
2. You want to implement dApp (that communicate via TCP)
3. You want to implement general TCP app which
### Why this solution
#### I. Event driven architecture: Easy to use
No need to setup receiving loop.
1. Setup events which will pop up every time you receive message.
2. Then establish connection with just one call and you are ready to go.
3. To send message just use `SendMessage(PlutoMessage msg)`.
4. To close connection just use `CloseConnection()` method.
#### II. Connection with authentification by default
- AcccessCredentials class enable you to convert information (which are must for connection) to URI. That way, it's easy to share. E.g. PlutoWallet use QR code scanning to access dApp's URI.
#### III. Send
- Each message has id header `MessageCode`(e.g. `MessageCode.PublicKey`). This way you determinate, how to interpret the data.
- Sending messages (byte[], string, transaction(=[see `Method` class](https://github.com/ajuna-network/Ajuna.NetApi/blob/6976d251c3ae468b1190f13b0656ce54d94bf0af/Ajuna.NetApi/Model/Extrinsics/Method.cs) in [Ajuna.NetApi](https://github.com/ajuna-network/Ajuna.NetApi))). Serialize any data of your choice.
- `SendMethod(Method m)` makes very convinient to send your method.
#### IV. Receive data
- As mentioned in `I.` there network stream receiving loop is automaticaly setup.
- Setup event `OnMessageReceived`. Handle every received message there.
#### V. Adjustable
- To implement custom solution, you can adjust and implement your own variations. 
- If `PlutoEventManager` is not general or specific enought for your usecase: Extend `class PlutoManager` the way YOU like it. PlutoManager contains methods that designed to build custom abstraction.

## Usage
**! Note that Plutonication is still in development stage !** We are looking forward to deliver the best solution soon.
### Pure code examples:
- Server (CryptoWallet) Demo **TODO**
- Client (dApp) Demo **TODO**
### Server specific code
- Server in context of crypto is dApp
### Client specific code
- Client in context of crypto is Wallet
### Sending
### Receiving
### Close Connection
