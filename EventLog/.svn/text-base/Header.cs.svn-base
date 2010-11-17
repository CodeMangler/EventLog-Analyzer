using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace pal.EventLog
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Header
    {
        public uint HeaderLength;
        public uint Signature;
        public uint Unknown1;
        public uint Unknown2;
        public uint Unknown3;
        public uint FooterOffset;
        public uint NextIndex;
        public uint FileLength;
        public uint Unknown6;
        public uint Unknown7;
        public uint Unknown8;
        public uint EndHeaderLength;

        public static Header Fetch(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] rawHeader = reader.ReadBytes(Marshal.SizeOf(typeof (Header)));
            GCHandle gcHandle = GCHandle.Alloc(rawHeader, GCHandleType.Pinned);
            Header header = (Header) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (Header));
            gcHandle.Free();
            return header;
        }

        public void Verify()
        {
            if (Signature != 0x654C664c)
                throw new Exception();
        }

        public override string ToString()
        {
            StringBuilder contents = new StringBuilder();
            contents.Append("HeaderLength: ").Append(HeaderLength).Append("\n");
            contents.Append("Signature: ").Append(Signature).Append("\n");
            contents.Append("FooterOffset: ").Append(FooterOffset).Append("\n");
            contents.Append("NextIndex: ").Append(NextIndex).Append("\n");
            contents.Append("FileLength: ").Append(FileLength).Append("\n");
            contents.Append("EndHeaderLength: ").Append(EndHeaderLength).Append("\n");
            return contents.ToString();
        }
    }
}