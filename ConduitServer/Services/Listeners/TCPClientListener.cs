﻿using System;

namespace ConduitServer.Services.Listeners
{
    class TCPClientListener : Service, IClientListener
    {
        public event Action<Client>? Connected;
    }
}