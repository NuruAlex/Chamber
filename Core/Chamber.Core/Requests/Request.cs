﻿using Chamber.Core.Users;

namespace Chamber.Core.Requests;

[Serializable]
public abstract class Request(long id, Client client) : BaseEntity(id)
{
    public Client Client { get; set; } = client;
}
