﻿using Conduit.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conduit.Hosting
{
    public abstract class ClientWorker
    {
        protected ClientMaintainer ClientMaintainer { get; private set; }

        public ClientWorker(ClientMaintainer cm)
        {
            ClientMaintainer = cm;
        }
        public abstract void Maintain();
        protected MemoryStream ReadToStream(int length)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(ClientMaintainer.VClient.NetworkStream.ReadData(length));
            ms.Position = 0;
            return ms;
        }
        protected void WaitToAvailable()
        {
            while (!ClientMaintainer.VClient.NetworkStream.DataAvailable)
            {
                Thread.Sleep(1);
            }
        }

        protected void ShutdownClient()
        {
            ClientMaintainer.VClient.Shutdown();
        }
    }
}
