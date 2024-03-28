using Chamber.Collections;
using Chamber.Core.Enums;
using Chamber.Core.Users;
using Chamber.Log;
using Chamber.Recievers;
using Messages.Building;
using Messages.Core.Types;
using Messages.Handling;
using Messages.Senders;
using Telegram.Bot.Types;

Logger.Init();
MessageReciever.Init();
CallBackReciever.Init();

MediaPathArchieve.InitialDirectory = "Resources\\Media";

MediaPathArchieve.AddStandartPath<Video>("Videos\\");
MediaPathArchieve.AddStandartPath<Voice>("Voices\\");
MediaPathArchieve.AddStandartPath<Audio>("Audios\\");
MediaPathArchieve.AddStandartPath<PhotoSize>("Photos\\");
MediaPathArchieve.AddStandartPath<VideoNote>("VideoNotes\\");
BotOptions options = new("test", "7063265373:AAFbiaDrVvV-ozMT5v5Q0l6ZtUievH9iHBA");

Builder.SetOptions(options);
Resiever.SetOptions(options);
Sender.SetOptions(options);
MessageDeleter.SetOptions(options);

Resiever.StartRecieving();

DataBase.Users.Add(new TelegramUser(5082579517, "79859338751", "Lekha", [UserLevel.Admin, UserLevel.Client]));

Console.ReadLine();