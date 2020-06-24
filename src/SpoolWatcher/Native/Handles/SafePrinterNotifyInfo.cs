using Microsoft.Win32.SafeHandles;
using SpoolerWatcher.Native.Structures;
using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher.Handles
{
    internal class SafePrinterNotifyInfo : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafePrinterNotifyInfo() : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return WinSpool.FreePrinterNotifyInfo(handle);
        }

        public static implicit operator PrinterNotifyInfoCustom(SafePrinterNotifyInfo safePrinterNotifyInfo)
        {
            if (safePrinterNotifyInfo.IsInvalid)
                return default;

            var notifyInfo = Marshal.PtrToStructure<PrinterNotifyInfo>(safePrinterNotifyInfo.handle);

            var pData = (long)safePrinterNotifyInfo.handle + (long)Marshal.OffsetOf<PrinterNotifyInfo>("aData");

            var data = new PrinterNotifyInfoData[notifyInfo.Count];

            for (uint i = 0; i < notifyInfo.Count; i++)
            {
                data[i] = Marshal.PtrToStructure<PrinterNotifyInfoData>((IntPtr)pData);

                pData += Marshal.SizeOf<PrinterNotifyInfoData>();
            }

            return new PrinterNotifyInfoCustom
            {
                Version = notifyInfo.Version,
                Flags = notifyInfo.Flags,
                Count = notifyInfo.Count,
                aData = data
            };
        }
    }
}
