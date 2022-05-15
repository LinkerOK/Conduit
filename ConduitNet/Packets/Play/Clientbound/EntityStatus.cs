﻿namespace Conduit.Net.Packets.Play.Clientbound
{
    public sealed class EntityStatus: Packet
    {
        public int EntityId;
        public byte Status;

        public EntityStatus() => Id = 0x1B;
    }
}