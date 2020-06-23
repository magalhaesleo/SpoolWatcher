using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
        public DateTime ToDateTime()
        {
            return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
        }
    }
}
