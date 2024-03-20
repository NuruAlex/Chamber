using Chamber.ConsoleApp;
using Chamber.Recievers;
using Messages.Core.Types;
using Messages.Handling;
using Messages.Senders;

Logger logger = new();
MessageReciever messageReciever = new();
TextReciever textReciever = new();
ContactReciever contactReciever = new();

BotOptions options = new("test", "7063265373:AAFbiaDrVvV-ozMT5v5Q0l6ZtUievH9iHBA");

Resiever.SetOptions(options);
Sender.SetOptions(options);

Resiever.StartRecieving();


Console.ReadLine();