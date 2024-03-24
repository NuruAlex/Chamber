using Chamber.Recievers;
using Messages.Core.Types;
using Messages.Handling;
using Messages.Senders;
using Chamber.Log;

Logger.Init();
MessageReciever.Init();
CallBackReciever.Init();

BotOptions options = new("test", "7063265373:AAFbiaDrVvV-ozMT5v5Q0l6ZtUievH9iHBA");

Resiever.SetOptions(options);
Sender.SetOptions(options);
MessageDeleter.SetOptions(options);

Resiever.StartRecieving();




Console.ReadLine();