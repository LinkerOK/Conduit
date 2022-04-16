﻿using System.IO;
using System.IO.Compression;
using System.Text;

namespace Conduit.Net.IO.RawPacket
{
    public class CompressedWriter : IWriter
    {
        private readonly Binary.Writer _binaryWriter;
        private readonly int _compressionTreshold;
        private readonly CompressionLevel _compressionLevel;

        public CompressedWriter(Stream stream, int compressionTreshold, CompressionLevel compressionLevel, bool leaveOpen = false)
            : this(new Binary.Writer(stream, Encoding.UTF8, leaveOpen), compressionTreshold, compressionLevel) { }
        public CompressedWriter(Binary.Writer binaryWriter, int compressionTreshold, CompressionLevel compressionLevel)
        {
            _compressionTreshold = compressionTreshold;
            _compressionLevel = compressionLevel;

            _binaryWriter = binaryWriter;
        }

        public void Write(Packets.RawPacket rawPacket)
        {
            if (rawPacket.Data.Length + 2 >= _compressionTreshold)
            {
                using var compressedMemory = new MemoryStream();
                using var compressedBinaryWriter = new Binary.Writer(new ZLibStream(compressedMemory, _compressionLevel, false), Encoding.UTF8, false);

                compressedBinaryWriter.Write7BitEncodedInt(rawPacket.Id);
                compressedBinaryWriter.Write(rawPacket.Data);

                var compressedData = compressedMemory.ToArray();

                _binaryWriter.Write7BitEncodedInt(compressedData.Length + 1);
                _binaryWriter.Write7BitEncodedInt(rawPacket.Length);
            }
            else
            {
                _binaryWriter.Write7BitEncodedInt(rawPacket.Length + 1);
                _binaryWriter.Write7BitEncodedInt(0);
                _binaryWriter.Write7BitEncodedInt(rawPacket.Id);
                _binaryWriter.Write(rawPacket.Data);
            }
        }

        public void Dispose()
        {
            _binaryWriter?.Dispose();
        }
    }
}