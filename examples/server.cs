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
Console.WriteLine("... waiting for connection ...");
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
Console.WriteLine("[After you type connection will stop]");
Console.ReadKey(); // after you press key on your keyboard code goes on ...
// ... app is going to close connection ... -----------------------------------

// PART 4.5 (optional): stop receiving messages -------------------------------
manager.StopReceiveLoop();

// STEP 5: end connection -----------------------------------------------------
manager.CloseConnection();