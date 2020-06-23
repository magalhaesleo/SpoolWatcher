using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher
{
    [StructLayout(LayoutKind.Sequential)]
    internal class PrinterNotifyOptionsType
    {
        public ushort Type;
        public ushort Reserved0;
        public uint Reserved1;
        public uint Reserved2;
        public uint Count;
        public IntPtr pFields;
    }
}
