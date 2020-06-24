using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher.Native.Structures
{
    internal struct PrinterNotifyInfo
    {
        public uint Version;
        public uint Flags;
        public uint Count;
        public PrinterNotifyInfoData[] aData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyInfoCustom
    {
        public uint Version;
        public uint Flags;
        public uint Count;
        public NotifyData aData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyInfoData
    {
        public NotifyType Type;
        public ushort Field;
        public uint Reserved;
        public uint Id;
        public NotifyData NotifyData;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct NotifyData
    {
        [FieldOffset(0)]
        public uint adwData0;
        [FieldOffset(4)]
        public uint adwData1;
        [FieldOffset(0)]
        public Data Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Data
    {
        public uint cbBuf;
        public IntPtr pBuf;
    }
}
