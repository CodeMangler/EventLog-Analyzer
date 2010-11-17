using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace pal.EventLog
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Metadata
    {
        public uint Length;
        public uint Reserved;
        public uint RecordNumber;
        public uint TimeGenerated;
        public uint TimeWritten;
        public uint EventID;
        public ushort EventType;
        public ushort NumStrings;
        public ushort EventCategory;
        public ushort ReservedFlags;
        public uint ClosingRecordNumber;
        public uint StringOffset;
        public uint UserSidLength;
        public uint UserSidOffset;
        public uint DataLength;
        public uint DataOffset;

        public static Metadata Fetch(BinaryReader readerPositionedAtTheRightOffset)
        {
            return Fetch(readerPositionedAtTheRightOffset.ReadBytes(Globals.MetadataSize));
        }

        public static Metadata Fetch(byte[] metadataBytes)
        {
            byte[] rawMetadata = new byte[Globals.MetadataSize];
            Array.Copy(metadataBytes, rawMetadata, Globals.MetadataSize);
            GCHandle gcHandle = GCHandle.Alloc(rawMetadata, GCHandleType.Pinned);
            Metadata metadata = (Metadata) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (Metadata));
            gcHandle.Free();
            return metadata;
        }

        public override string ToString()
        {
            StringBuilder contents = new StringBuilder();
            contents.Append("Length: ").Append(Length).Append("\n");
            contents.Append("Reserved: ").Append(Reserved).Append("\n");
            contents.Append("RecordNumber: ").Append(RecordNumber).Append("\n");
            contents.Append("TimeGenerated: ").Append(DateTime.FromBinary(TimeGenerated)).Append("\n");
            contents.Append("TimeWritten: ").Append(DateTime.FromBinary(TimeWritten)).Append("\n");
            contents.Append("EventID: ").Append(EventID).Append("\n");
            contents.Append("EventType: ").Append(EventType).Append("\n");
            contents.Append("NumStrings: ").Append(NumStrings).Append("\n");
            contents.Append("EventCategory: ").Append(EventCategory).Append("\n");
            contents.Append("ReservedFlags: ").Append(ReservedFlags).Append("\n");
            contents.Append("ClosingRecordNumber: ").Append(ClosingRecordNumber).Append("\n");
            contents.Append("StringOffset: ").Append(StringOffset).Append("\n");
            contents.Append("UserSidLength: ").Append(UserSidLength).Append("\n");
            contents.Append("UserSidOffset: ").Append(UserSidOffset).Append("\n");
            contents.Append("DataLength: ").Append(DataLength).Append("\n");
            contents.Append("DataOffset: ").Append(DataOffset).Append("\n");
            return contents.ToString();
        }
    }
}