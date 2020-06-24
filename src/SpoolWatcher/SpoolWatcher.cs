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
    public sealed class SpoolWatcher : ISpoolWatcher, IDisposable
    {
        private SafeHPrinter hPrinter;
        private bool disposed = false;
        private readonly string printerName;
        private readonly PrinterNotifyCategory printerNotifyCategory;
        private readonly ManualResetEventSlim stopEvent = new ManualResetEventSlim();
        private SafeNotificationHandle notificationHandle;
        private Thread tNotifications;
        public PrinterChange PrinterChange { get; set; }
        public PrinterNotifyFilters PrinterNotifyFilter { get; set; }
        public JobNotifyFilters JobNotifyFilter { get; set; }

        public event EventHandler<SpoolerNotificationEventArgs> SpoolerNotificationReached;

        public SpoolWatcher(string printerName) : this(printerName, PrinterNotifyCategory.CategoryAll)
        {
        }

        public SpoolWatcher(string printerName, PrinterNotifyCategory printerNotifyCategory)
        {
            this.printerName = printerName;
            this.printerNotifyCategory = printerNotifyCategory;
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

            var optionsType = CreateOptionsType();

            printerNotifyOptions.Count = (uint)optionsType.Count();

            var pNotifyOptionsSz = Marshal.SizeOf<PrinterNotifyOptions>();

            printerNotifyOptions.pTypes = Marshal.AllocHGlobal(pNotifyOptionsSz * optionsType.Count());

            for (int i = 0; i < optionsType.Count(); i++)
            {
                var ptr = IntPtr.Add(printerNotifyOptions.pTypes, i * pNotifyOptionsSz);

                Marshal.StructureToPtr(optionsType.ElementAt(i), ptr, false);
            }

            notificationHandle = WinSpool.FindFirstPrinterChangeNotification(hPrinter, (uint)PrinterChange, (uint)printerNotifyCategory, ref printerNotifyOptions);

            foreach (var optionType in optionsType)
            {
                Marshal.FreeHGlobal(optionType.pFields);
            }

            Marshal.FreeHGlobal(printerNotifyOptions.pTypes);

            if (notificationHandle.IsInvalid)
            {
                var errorCode = Marshal.GetLastWin32Error();

                throw new InvalidOperationException($"Error: {errorCode}");
            }

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
                                    notificationInfo.Type = NotifyType.Job;
                                    notificationInfo.Data = DataFieldParser.GetJobTypeData(printerNotifyInfo.aData[i]);
                                        break;
                                case NotifyType.Printer:
                                    notificationInfo.Type = NotifyType.Printer;
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

            var printerFields = PrinterNotifyFilter.ToPrinterNotifyFields();

            if (printerFields.Any())
            {
                var pOptionType = new PrinterNotifyOptionsType();

                pOptionType.Type = (ushort)NotifyType.Printer;

                pOptionType.Count = (uint)printerFields.Count();

                pOptionType.pFields = Marshal.AllocHGlobal(printerFields.Count() * sz);

                int ofs = 0;

                foreach (var field in printerFields)
                {
                    Marshal.WriteInt16(pOptionType.pFields, ofs, (short)field);

                    ofs += sz;
                }

                printerNotifyOptionsTypes.Add(pOptionType);
            }

            var jobFields = JobNotifyFilter.ToJobNotifyFields();

            if (jobFields.Any())
            {
                var jOptionType = new PrinterNotifyOptionsType();

                jOptionType.Type = (ushort)NotifyType.Job;

                jOptionType.Count = (uint)jobFields.Count();

                jOptionType.pFields = Marshal.AllocHGlobal(jobFields.Count() * sz);

                var ofs = 0;

                foreach (var field in jobFields)
                {
                    Marshal.WriteInt16(jOptionType.pFields, ofs, (short)field);
                    ofs += sz;
                }

                printerNotifyOptionsTypes.Add(jOptionType);
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
