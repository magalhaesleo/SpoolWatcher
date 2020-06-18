using System;
using System.Runtime.InteropServices;

namespace SpoolWatcher.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyOptions
    {
        public uint Version;
        public uint Flags;
        public uint Count;
        public IntPtr pTypes;
    }
}
