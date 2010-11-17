namespace pal.EventLog
{
    internal class Globals
    {
        public static int MetadataSize
        {
            get { return 56; /*Marshal.SizeOf(typeof (Metadata))*/ }
        }

        public static int HeaderSize
        {
            get { return 48; /*Marshal.SizeOf(typeof (Header))*/ }
        }

        public static int FooterSize
        {
            get { return 40; /* Marshal.SizeOf(typeof(Footer)) */ }
        }

        public static int NullCharSize
        {
            get { return UnicodeCharSize; }
        }

        public static int UnicodeCharSize
        {
            get { return 2; }
        }
    }
}