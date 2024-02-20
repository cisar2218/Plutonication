# Plutonication

![Plutonication illustrative image](https://github.com/cisar2218/Plutonication/assets/77352013/78c46443-8b41-4f62-a2d9-0b206bb7cd6d)

Communications protocol that enables seamless interactions between dApps and wallets across all platforms.

Establishing a connection is as easy as scanning a QR code.

This is a c# version.

# Requirements

-C# and Visual Studio installed on your system.

The package uses **.NET 6**.

# Installation

Nuget package: https://www.nuget.org/packages/Plutonication
```
dotnet add package Plutonication
```

# Other versions

- Javascript/Typescript = https://github.com/rostislavLitovkin/plutonication

# Usage

The overall structure of Plutonication is designed to be as little intrusive as possible.

A comprehensive guide for adding Plutonication to your dApp / Wallet can be found here: https://plutonication-acnha.ondigitalocean.app/docs/csharp.

# How Plutonication works

The private key is always saved in your wallet on your phone and is never sent anywhere.

You need to pair the dApp with the wallet. To do so, the wallet needs to receive a special link with information needed to establish the connection. The wallet can receive this link, for example, by scanning a QR code. Once the link is received, the dApp and the wallet will get paired via websockets to establish a stable connection between different platforms. After the connection is established, the wallet is ready to receive any Extrinsics, which it can then sign and send back to the dApp.

To get a more in-depth details of the underlying backend, read this guide: https://plutonication-acnha.ondigitalocean.app/docs/flask-server.

# Problem / Motivation

Currently, there is no way to connect a crypto wallet to more exotic devices, like gaming console and wearables.

Plutonication enables that!

# Limitations

- Both devices need to support internet connection

# Inspiration

- [https://walletconnect.com/](https://walletconnect.com/)

# Contributions

Contributions are welcome. 

# License

This project is licensed under the MIT License.

