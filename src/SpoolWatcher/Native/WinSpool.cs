using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace SpoolWatcher.Native
{
    internal static class WinSpool
    {
        [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenPrinter(string PrinterName, out IntPtr PrinterHandle, IntPtr PrinterDefaults);

        [DllImport("winspool.drv", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "FindFirstPrinterChangeNotification", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern SafeWaitHandle FindFirstPrinterChangeNotification(IntPtr hPrinter, uint fdwFlags, uint fdwOptions, ref PrinterNotifyOptions pPrinterNotifyOptions);

        [DllImport("winspool.drv", EntryPoint = "FindNextPrinterChangeNotification", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindNextPrinterChangeNotification(IntPtr hChange, out uint pdwChange, IntPtr PrinterNotifyOptions, out IntPtr pPrinterNotifyInfo);

        [DllImport("winspool.drv", EntryPoint = "FindClosePrinterChangeNotification", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool FindClosePrinterChangeNotification(IntPtr hChange);

        [DllImport("winspool.drv", EntryPoint = "FreePrinterNotifyInfo", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FreePrinterNotifyInfo(IntPtr pPrinterNotifyInfo);
    }
}
