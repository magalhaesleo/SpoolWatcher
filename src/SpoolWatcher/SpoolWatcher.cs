using SpoolerWatcher.Events;
using SpoolerWatcher.Handles;
using SpoolerWatcher.Helpers;
using SpoolerWatcher.Native.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace SpoolerWatcher
{
    public sealed class SpoolWatcher : IDisposable
    {
        private SafeHPrinter hPrinter;
        private bool disposed = false;
        private readonly string printerName;
        private readonly PrinterChange printerChange;
        private readonly PrinterNotifyCategory printerNotifyCategory;
        private readonly NotifyOptions[] notifyOptions;
        private readonly ManualResetEventSlim stopEvent = new ManualResetEventSlim();
        private SafeNotificationHandle notificationHandle;
        private Thread tNotifications;

        public event EventHandler<SpoolerNotificationEventArgs> SpoolerNotificationReached;

        public SpoolWatcher(string printerName, PrinterNotifyCategory printerNotifyCategory, params NotifyOptions[] notifyOptions) : this(printerName, printerNotifyCategory, 0, notifyOptions)
        {
            if (notifyOptions == null || notifyOptions.Length == 0)
                throw new ArgumentException(nameof(notifyOptions));
        }

        public SpoolWatcher(string printerName, PrinterNotifyCategory printerNotifyCategory, PrinterChange printerChange) : this(printerName, printerNotifyCategory, printerChange, null)
        {
        }

        public SpoolWatcher(string printerName, PrinterNotifyCategory printerNotifyCategory, PrinterChange printerChange, params NotifyOptions[] notifyOptions)
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

            var printerNotifyOptions = new PrinterNotifyOptionsNative();
            printerNotifyOptions.Version = 2;
            printerNotifyOptions.Count = (uint)notifyOptions.Length;

            var pNotifyOptionsSz = Marshal.SizeOf<PrinterNotifyOptionsNative>();

            printerNotifyOptions.pTypes = Marshal.AllocHGlobal(pNotifyOptionsSz * notifyOptions.Length);

            var optionsType = CreateOptionsType();

            for (int i = 0; i < optionsType.Count(); i++)
            {
                var ptr = IntPtr.Add(printerNotifyOptions.pTypes, i * pNotifyOptionsSz);

                Marshal.StructureToPtr(optionsType.ElementAt(i), ptr, false);
            }

            notificationHandle = WinSpool.FindFirstPrinterChangeNotification(hPrinter, (uint)printerChange, (uint)printerNotifyCategory, ref printerNotifyOptions);

            foreach (var optionType in optionsType)
            {
                Marshal.FreeHGlobal(optionType.pFields);
            }

            Marshal.FreeHGlobal(printerNotifyOptions.pTypes);

            tNotifications = new Thread(() => WaitForNotifications());

            tNotifications.Start();
        }

        public void Stop()
        {
            stopEvent.Set();

            tNotifications.Join();

            notificationHandle.Close();
        }

        private void WaitForNotifications()
        {
            var handles = new WaitHandle[] { notificationHandle, stopEvent.WaitHandle };

            while (WaitHandle.WaitAny(handles) == 0)
            {
                if (WinSpool.FindNextPrinterChangeNotification(notificationHandle, out var change, IntPtr.Zero, out var pNotifyInfo))
                {
                    if (SpoolerNotificationReached != null)
                    {
                        PrinterNotifyInfo printerNotifyInfo = pNotifyInfo;

                        var datas = new List<NotificationInfo>();

                        for (uint i = 0; i < printerNotifyInfo.Count; i++)
                        {
                            var notificationInfo = new NotificationInfo();

                            notificationInfo.Id = printerNotifyInfo.aData[i].Id;

                            switch ((NotifyType)printerNotifyInfo.aData[i].Field)
                            {
                                case NotifyType.Job:
                                    notificationInfo.Data = DataFieldParser.GetJobTypeData(printerNotifyInfo.aData[i]);
                                        break;
                                case NotifyType.Printer:
                                    notificationInfo.Data = DataFieldParser.GetPrinterTypeData(printerNotifyInfo.aData[i]);
                                    break;
                                default:
                                    break;
                            }                            

                            datas.Add(notificationInfo);
                        }

                        var evArgs = new SpoolerNotificationEventArgs
                        {
                            PrinterChange = (PrinterChange)change,
                            NotificationsData = datas.ToArray()
                        };

                        SpoolerNotificationReached(this, evArgs);
                    }

                    pNotifyInfo.Close();
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
                        var pNotifyOption = (PrinterNotifyOptions)notifyOption;

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


        public void Dispose()
        {
            if (disposed)
                return;

            stopEvent.Dispose();
            
            notificationHandle?.Dispose();
            
            hPrinter?.Dispose();

            disposed = true;
        }
    }
}
