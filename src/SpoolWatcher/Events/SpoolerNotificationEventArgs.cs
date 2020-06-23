using SpoolerWatcher.Events;
using System;

namespace SpoolerWatcher
{
    public class SpoolerNotificationEventArgs : EventArgs
    {
        public PrinterChange PrinterChange { get; set; }
        public uint Id { get; set; }
        public NotificationInfo[] NotificationsData { get; set; }
    }
}
