using SpoolerWatcher.Events;
using System;

namespace SpoolerWatcher
{
    public class SpoolNotificationEventArgs : EventArgs
    {
        public PrinterChange PrinterChange { get; set; }
        public NotificationInfo[] NotificationInfos { get; set; }
    }
}
