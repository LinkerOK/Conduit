﻿using Conduit.Net.Serialization.Attributes;

namespace Conduit.Net.Packets
{
    public class Packet
    {
        [VarInt]
        public int Length;
        [VarInt]
        public int Id;
    }
}