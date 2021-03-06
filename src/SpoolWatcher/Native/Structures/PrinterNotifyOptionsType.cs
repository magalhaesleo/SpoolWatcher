﻿using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PrinterNotifyOptionsType
    {
        public NotifyType Type;
        public ushort Reserved0;
        public uint Reserved1;
        public uint Reserved2;
        public uint Count;
        public IntPtr pFields;
    }
}
