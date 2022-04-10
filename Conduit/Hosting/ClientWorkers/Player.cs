﻿using Conduit.Network.Protocol.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Hosting.ClientWorkers
{
    public sealed class Player : ClientWorker
    {
        public Player (ClientMaintainer cm) : base(cm)
        {
        }

        public override void Handling()
        {
            RawPacket raw = new RawPacket();
            ClientMaintainer.Protocol.SRawPacket.Deserialize(ClientMaintainer.VClient.RemoteStream, raw);
            
            switch (raw.Id)
            {
                default:
                    {
                        //OnStatus();
                        break;
                    }
                case 0x01:
                    {
                        //OnPing(onlyheader.Length - 1);
                        break;
                    }
            }


        }
    }
}