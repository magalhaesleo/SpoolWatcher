namespace SpoolerWatcher
{
    public class PrinterNotifyOptions : NotifyOptions
    {
        public override NotifyType NotifyType => NotifyType.Printer;

        public PrinterNotifyFields[] PrinterNotifyFields { get; }

        public PrinterNotifyOptions(PrinterNotifyFields[] printerNotifyFields)
        {
            PrinterNotifyFields = printerNotifyFields;
        }
    }
}
