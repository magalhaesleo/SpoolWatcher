using System;

namespace SpoolerWatcher
{
    public interface ISpoolWatcher
    {
        event EventHandler<SpoolerNotificationEventArgs> SpoolerNotificationReached;

        void Start();
        void Stop();
    }
}
