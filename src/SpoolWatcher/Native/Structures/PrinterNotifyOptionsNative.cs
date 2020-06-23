using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyOptionsNative
    {
        public uint Version;
        public uint Flags;
        public uint Count;
        public IntPtr pTypes;
    }
}
