using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyInfo
    {
        public uint Version;
        public uint Flags;
        public uint Count;
        public PrinterNotifyInfoData[] aData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyInfoData
    {
        public ushort Type;
        public ushort Field;
        public uint Reserved;
        public uint Id;
        public NotifyData NotifyData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NotifyData
    {
        public uint adwData0;
        public uint adwData1;
        public Data Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Data
    {
        public uint cbBuf;
        public IntPtr pBuf;
    }
}
