using SpoolWatcher.Options.Printers;

namespace SpoolWatcher.Options
{
    public class PrinterWatcherOptions : NotifyOptions
    {
        public override NotifyType NotifyType => NotifyType.Printer;

        public PrinterNotifyFields[] PrinterNotifyFields { get; }

        public PrinterWatcherOptions(PrinterNotifyFields[] printerNotifyFields)
        {
            PrinterNotifyFields = printerNotifyFields;
        }
    }
}
