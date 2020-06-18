using SpoolWatcher.Native;
using SpoolWatcher.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace SpoolWatcher
{
    public class SpoolWatcher : IDisposable
    {
        private IntPtr hPrinter = IntPtr.Zero;
        private bool disposed = false;
        private readonly string printerName;
        private readonly PrinterChange printerChange;
        private readonly PrinterNotifyCategory printerNotifyCategory;
        private readonly NotifyOptions[] notifyOptions;
        private ManualResetEventSlim stopEvent = new ManualResetEventSlim();
        private NotificationWaitHandle notificationWaitHandle;
        private Task waitEvents;

        public SpoolWatcher(string printerName, PrinterNotifyCategory printerNotifyCategory, params NotifyOptions[] notifyOptions) : this(printerName, 0, printerNotifyCategory, notifyOptions)
        {
            if (notifyOptions == null || notifyOptions.Length == 0)
                throw new ArgumentNullException(nameof(notifyOptions));
        }

        public SpoolWatcher(string printerName, PrinterChange printerChange, PrinterNotifyCategory printerNotifyCategory) : this(printerName, printerChange, printerNotifyCategory, null)
        {
        }

        public SpoolWatcher(string printerName, PrinterChange printerChange, PrinterNotifyCategory printerNotifyCategory, params NotifyOptions[] notifyOptions)
        {
            this.printerName = printerName;
            this.printerChange = printerChange;
            this.printerNotifyCategory = printerNotifyCategory;
            this.notifyOptions = notifyOptions;
        }

        public void Start()
        {
            if (!WinSpool.OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
            {
                var errorCode = Marshal.GetLastWin32Error();

                throw new InvalidOperationException($"Error: {errorCode}");
            }

            var printerNotifyOptions = new PrinterNotifyOptions();
            printerNotifyOptions.Version = 2;
            printerNotifyOptions.Count = (uint)notifyOptions.Length;

            var pNotifyOptionsSz = Marshal.SizeOf<PrinterNotifyOptions>();

            printerNotifyOptions.pTypes = Marshal.AllocHGlobal(pNotifyOptionsSz * notifyOptions.Length);

            var optionsType = CreateOptionsType();

            for (int i = 0; i < optionsType.Count(); i++)
            {
                var ptr = IntPtr.Add(printerNotifyOptions.pTypes, i * pNotifyOptionsSz);

                Marshal.StructureToPtr(optionsType.ElementAt(i), ptr, false);
            }            

            var hNotification = WinSpool.FindFirstPrinterChangeNotification(hPrinter, (uint)printerChange, (uint)printerNotifyCategory, ref printerNotifyOptions);

            notificationWaitHandle = new NotificationWaitHandle(hNotification);

            foreach (var optionType in optionsType)
            {
                Marshal.FreeHGlobal(optionType.pFields);
            }

            Marshal.FreeHGlobal(printerNotifyOptions.pTypes);

            waitEvents = Task.Run(() => WaitForNotifications());
        }

        public void Stop()
        {
            stopEvent.Set();
        }

        private void WaitForNotifications()
        {
            var handles = new WaitHandle[] { notificationWaitHandle, stopEvent.WaitHandle };

            while (true)
            {
                var index = WaitHandle.WaitAny(handles);

                if (index == 0)
                {
                    if (WinSpool.FindNextPrinterChangeNotification(notificationWaitHandle.SafeWaitHandle.DangerousGetHandle(), out var change, IntPtr.Zero, out var pNotifyInfo))
                    {
                        WinSpool.FreePrinterNotifyInfo(pNotifyInfo);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private IEnumerable<PrinterNotifyOptionsType> CreateOptionsType()
        {
            var printerNotifyOptionsTypes = new List<PrinterNotifyOptionsType>();

            var sz = sizeof(ushort);

            int ofs;

            foreach (var notifyOption in notifyOptions)
            {
                var optionType = new PrinterNotifyOptionsType();

                switch (notifyOption.NotifyType)
                {
                    case NotifyType.Printer:
                        var pNotifyOption = (PrinterWatcherOptions)notifyOption;

                        optionType.Type = (ushort)NotifyType.Printer;
                        optionType.Count = (uint)pNotifyOption.PrinterNotifyFields.Length;
                        optionType.pFields = Marshal.AllocHGlobal(pNotifyOption.PrinterNotifyFields.Length * sz);

                        ofs = 0;

                        foreach (var field in pNotifyOption.PrinterNotifyFields)
                        {
                            Marshal.WriteInt16(optionType.pFields, ofs, (short)field);
                            ofs += sz;
                        }

                        break;
                    case NotifyType.Job:
                        var jNotifyOption = (JobNotifyOptions)notifyOption;

                        optionType.Type = (ushort)NotifyType.Job;
                        optionType.Count = (uint)jNotifyOption.JobNotifyFields.Length;
                        optionType.pFields = Marshal.AllocHGlobal(jNotifyOption.JobNotifyFields.Length * sz);

                        ofs = 0;

                        foreach (var field in jNotifyOption.JobNotifyFields)
                        {
                            Marshal.WriteInt16(optionType.pFields, ofs, (short)field);
                            ofs += sz;
                        }

                        break;
                    default:
                        break;
                }

                printerNotifyOptionsTypes.Add(optionType);
            }

            return printerNotifyOptionsTypes;
        }

        ~SpoolWatcher()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                stopEvent.Dispose();
            }

            if (hPrinter != IntPtr.Zero)
                WinSpool.ClosePrinter(hPrinter);

            if (notificationWaitHandle != null && !notificationWaitHandle.SafeWaitHandle.IsInvalid)
                WinSpool.FindClosePrinterChangeNotification(notificationWaitHandle.SafeWaitHandle.DangerousGetHandle());

            disposed = true;
        }
    }
}
