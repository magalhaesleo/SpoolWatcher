using Microsoft.Win32.SafeHandles;
using System.Threading;

namespace SpoolWatcher
{
    public class NotificationWaitHandle : WaitHandle
    {
        public NotificationWaitHandle(SafeWaitHandle hNotification)
        {
            SafeWaitHandle = hNotification;
        }
    }
}
