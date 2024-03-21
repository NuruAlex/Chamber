using Chamber.Collections;
using Chamber.ConsoleApp;
using Chamber.Recievers;
using Messages.Core.Types;
using Messages.Handling;
using Messages.Senders;

Logger.Init();
MessageReciever.Init();
TextReciever.Init();
ContactReciever.Init();
CallBackReciever.Init();

BotOptions options = new("test", "7063265373:AAFbiaDrVvV-ozMT5v5Q0l6ZtUievH9iHBA");

Resiever.SetOptions(options);
Sender.SetOptions(options);
MessageDeleter.SetOptions(options);

Resiever.StartRecieving();





DataBase.Problems.Add(
    new(1, "Отклонить заявку", "Необходимо откатить назад заявку"),
    new(2, "Нет ответа на вопрос", "Подождать. Если прошло более суток - переотправить"),
    new(3, "Ошибка при передаче в ВС9", "Откатить заявку назад и переотправить в проект"),
    new(4, "Нет Responce Id", "Откатить заявку назад и переотправить данные")
    );











Console.ReadLine();