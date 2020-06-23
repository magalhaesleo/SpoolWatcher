using Microsoft.Win32.SafeHandles;

namespace SpoolerWatcher.Handles
{
    internal class SafeHPrinter : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeHPrinter() : base(ownsHandle: true)
        {
        }
        protected override bool ReleaseHandle()
        {
            return WinSpool.ClosePrinter(handle);
        }
    }
}
