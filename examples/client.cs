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
Console.WriteLine("[After you type connection will stop]");
Console.ReadKey(); // after you press key on your board code goes on ...
// ... app is going to close connection ... -----------------------------------

// PART 4.5 (optional): stop receiving messages -------------------------------
manager.StopReceiveLoop();

// STEP 5: end connection -----------------------------------------------------
manager.CloseConnection();
