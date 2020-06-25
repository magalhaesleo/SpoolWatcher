using System;

namespace SpoolerWatcher
{
    public interface ISpoolWatcher : IDisposable
    {
        event EventHandler<SpoolerNotificationEventArgs> SpoolerNotificationReached;
        PrinterNotifyFilters PrinterNotifyFilter { get; set; }
        JobNotifyFilters JobNotifyFilter { get; set; }
        PrinterChange PrinterChange { get; set; }
        void Start();
        void Stop();
    }
}
