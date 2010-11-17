using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace pal.EventLog
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Footer
    {
        public uint FooterLength;
        public uint Unknown0;
        public uint Unknown1;
        public uint Unknown2;
        public uint Unknown3;
        public uint Unknown4;
        public uint FooterOffset;
        public uint NextIndex;
        public uint Unknown7;
        public uint EndFooterLength;

        public static Footer Fetch(BinaryReader reader)
        {
            reader.BaseStream.Seek(-40, SeekOrigin.End);
            byte[] rawHeader = reader.ReadBytes(Marshal.SizeOf(typeof (Footer)));
            GCHandle gcHandle = GCHandle.Alloc(rawHeader, GCHandleType.Pinned);
            Footer footer = (Footer) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (Footer));
            gcHandle.Free();
            return footer;
        }

        public void Verify()
        {
            if (Unknown0 != 0x11111111 || Unknown1 != 0x22222222 || Unknown2 != 0x33333333 || Unknown3 != 0x44444444)
                throw new Exception();
        }

        public override string ToString()
        {
            StringBuilder contents = new StringBuilder();
            contents.Append("FooterLength: ").Append(FooterLength).Append("\n");
            contents.Append("Unknown0: ").Append(Unknown0).Append("\n");
            contents.Append("Unknown1: ").Append(Unknown1).Append("\n");
            contents.Append("Unknown2: ").Append(Unknown2).Append("\n");
            contents.Append("Unknown3: ").Append(Unknown3).Append("\n");
            contents.Append("Unknown4: ").Append(Unknown4).Append("\n");
            contents.Append("FooterOffset: ").Append(FooterOffset).Append("\n");
            contents.Append("NextIndex: ").Append(NextIndex).Append("\n");
            contents.Append("Unknown7: ").Append(Unknown7).Append("\n");
            contents.Append("EndFooterLength: ").Append(EndFooterLength).Append("\n");
            return contents.ToString();
        }
    }
}