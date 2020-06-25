# SpoolWatcher
A simple wrapper to simplify the usage of functions FindFirstPrinterChangeNotification, FindNextPrinterChangeNotification and FindClosePrinterChangeNotification in managed code.

![Nuget](https://img.shields.io/nuget/v/SpoolWatcher) ![Nuget](https://img.shields.io/nuget/dt/SpoolWatcher)

SpoolWatcher is available via [NuGet](https://www.nuget.org/packages/SpoolWatcher/).
# Usage

This is a sample to watch the jobs status of a Printer

```cs
        using (var spoolWatcher = new SpoolWatcher("EPSON Universal Print Driver 3"))
        {
            spoolWatcher.JobNotifyFilter = JobNotifyFilters.STATUS;

            spoolWatcher.SpoolerNotificationReached += SpoolWatcher_SpoolerNotificationReached;

            spoolWatcher.Start();

            while (true)
            {
                Console.WriteLine("press esc to exit...");
                
                var key = Console.ReadKey();
                
                if (key.Key == ConsoleKey.Escape)
                      break;         
            }
            
            spoolWatcher.Stop();
        }
```

And the callback event like this

```cs
        private static void SpoolWatcher_SpoolerNotificationReached(object sender, SpoolNotificationEventArgs e)
        {
            Console.WriteLine("signaled");

            foreach (var data in e.NotificationsData)
            {
                Console.WriteLine("type: {0}", data.Data.DataType);
                Console.WriteLine("data: {0}", data.Data.Data);
            }
        }
```

To watch a print server instead a printer pass null as argument in constructor

```cs
            using (var spoolWatcher = new SpoolWatcher(null))
            {
                spoolWatcher.PrinterChange = PrinterChange.PRINTER_CHANGE_ADD_PRINTER | PrinterChange.PRINTER_CHANGE_DELETE_PRINTER;

                spoolWatcher.SpoolerNotificationReached += (sender, args) =>
                {
                    Console.WriteLine("Change {0}", args.PrinterChange);
                };

                spoolWatcher.Start();

                while (true)
                {
                    Console.WriteLine("press esc to exit...");
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Escape)
                        break;
                }
            }
```
