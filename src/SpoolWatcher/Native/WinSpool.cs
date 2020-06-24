using SpoolerWatcher.Handles;
using System;
using System.Runtime.InteropServices;

namespace SpoolerWatcher
{
    internal static class WinSpool
    {
        [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenPrinter(string printerName, out SafeHPrinter printerHandle, IntPtr printerDefaults);

        [DllImport("winspool.drv", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "FindFirstPrinterChangeNotification", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern SafeNotificationHandle FindFirstPrinterChangeNotification(SafeHPrinter hPrinter, uint fdwFlags, uint fdwOptions, ref PrinterNotifyOptions pPrinterNotifyOptions);

        [DllImport("winspool.drv", EntryPoint = "FindNextPrinterChangeNotification", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindNextPrinterChangeNotification(SafeNotificationHandle hChange, out uint pdwChange, IntPtr printerNotifyOptions, out SafePrinterNotifyInfo pPrinterNotifyInfo);

        [DllImport("winspool.drv", EntryPoint = "FindClosePrinterChangeNotification", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindClosePrinterChangeNotification(IntPtr hChange);

        [DllImport("winspool.drv", EntryPoint = "FreePrinterNotifyInfo", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreePrinterNotifyInfo(IntPtr pPrinterNotifyInfo);
    }
}
