using Microsoft.Win32.SafeHandles;

namespace SpoolerWatcher.Handles
{
    internal class SafeNotificationHandle : SafeHandleMinusOneIsInvalid
    {
        internal SafeNotificationHandle() : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return WinSpool.FindClosePrinterChangeNotification(handle);
        }

        public static implicit operator NotificationWaitHandle(SafeNotificationHandle notificationHandle) => notificationHandle.IsInvalid ? default : new NotificationWaitHandle(notificationHandle.handle);
    }
}
