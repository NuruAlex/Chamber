﻿using Chamber.Core.Users;
using Messages.Core.Types;
using Messages.Senders;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Chamber.Dialogs.FieldRequestDialog;

[JsonObject]
public class RequireNewSetrificateNumber(Client client) : IDataProcess
{

    [JsonProperty]
    public long NewCertificateNumber { get; set; }

    [JsonProperty]
    public bool WasDone { get; set; } = false;
    public Client Client { get; set; } = client;

    public async void NextAction(Message message)
    {

        if (message.Text == null)
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, введите новый номер сертификата"));
            return;
        }
        if (!long.TryParse(message.Text, out var number))
        {
            await Sender.SendMessage(new TextMessage(Client.Id, "Кажется произошла ошибка, номер сертификата должен быть числом:"));
            return;
        }

        NewCertificateNumber = number;
        WasDone = true;
    }

    public async void Start()
    {
        await Sender.SendMessage(new TextMessage(Client.Id, "Введите новый номер сертификата:"));
        WasDone = false;
    }
}
