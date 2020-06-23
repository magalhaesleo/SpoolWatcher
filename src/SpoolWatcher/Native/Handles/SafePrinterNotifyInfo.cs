using Microsoft.Win32.SafeHandles;
using SpoolerWatcher.Native.Structures;
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

        public static implicit operator PrinterNotifyInfo(SafePrinterNotifyInfo safePrinterNotifyInfo) => !safePrinterNotifyInfo.IsInvalid ? default : Marshal.PtrToStructure<PrinterNotifyInfo>(safePrinterNotifyInfo.handle);
    }
}
