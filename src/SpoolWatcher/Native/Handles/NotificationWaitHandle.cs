using Microsoft.Win32.SafeHandles;
using System;
using System.Threading;

namespace SpoolerWatcher
{
    internal class NotificationWaitHandle : WaitHandle
    {
        internal NotificationWaitHandle(IntPtr hNotification)
        {
            SafeWaitHandle = new SafeWaitHandle(hNotification, false);
        }
    }
}
