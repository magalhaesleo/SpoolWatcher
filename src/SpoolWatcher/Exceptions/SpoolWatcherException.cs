using System;

namespace SpoolerWatcher
{
    public class SpoolWatcherException : Exception
    {
        public SpoolWatcherException(string message) : base(message)
        {
        }
    }
}
