using NUnit.Framework;
using SpoolerWatcher;
using System;
using System.Printing;
using System.Text;
using System.Threading;

namespace SpoolerWatchers.Tests
{
    public class SpoolWatcherIntegrationTest
    {
        private PrintQueue printQueue;
        private SpoolWatcher spoolWatcher;

        [SetUp]
        public void Setup()
        {
            printQueue = LocalPrintServer.GetDefaultPrintQueue();
            spoolWatcher = new SpoolWatcher(printQueue.Name);
        }


        [Test]
        public void Watch_With_All_Filters_Should_Call_Event()
        {
            spoolWatcher.PrinterChange = PrinterChange.PRINTER_CHANGE_ADD_FORM | PrinterChange.PRINTER_CHANGE_CONFIGURE_PORT;

            spoolWatcher.PrinterNotifyFilter = PrinterNotifyFilters.DATATYPE | PrinterNotifyFilters.STATUS;

            spoolWatcher.JobNotifyFilter = JobNotifyFilters.DATATYPE | JobNotifyFilters.STATUS;

            spoolWatcher.Start();

            using var waitEv = new ManualResetEventSlim();

            spoolWatcher.SpoolerNotificationReached += (o, e) =>
            {
                waitEv.Set();
            };

            var job = printQueue.AddJob();

            var bytes = Encoding.UTF8.GetBytes("Test printing");
            job.JobStream.Write(bytes, 0, bytes.Length);
            job.JobStream.Close();

            Assert.That(waitEv.Wait(timeout: TimeSpan.FromSeconds(10)));

            spoolWatcher.Stop();
        }

        [Test]
        public void Watch_Using_Both_Filters_Should_Call_Event()
        {
            spoolWatcher.PrinterNotifyFilter = PrinterNotifyFilters.DATATYPE | PrinterNotifyFilters.STATUS;

            spoolWatcher.JobNotifyFilter = JobNotifyFilters.DATATYPE | JobNotifyFilters.STATUS;

            spoolWatcher.Start();

            using var waitEv = new ManualResetEventSlim();

            spoolWatcher.SpoolerNotificationReached += (o, e) =>
            {
                waitEv.Set();
            };

            var job = printQueue.AddJob();

            var bytes = Encoding.UTF8.GetBytes("Test printing");
            job.JobStream.Write(bytes, 0, bytes.Length);
            job.JobStream.Close();

            Assert.That(waitEv.Wait(timeout: TimeSpan.FromSeconds(10)));

            spoolWatcher.Stop();
        }

        [Test]
        public void Watch_Using_Something_PrinterChange_Filter_Should_Be_Ok()
        {
            spoolWatcher.PrinterChange = PrinterChange.PRINTER_CHANGE_ADD_JOB | PrinterChange.PRINTER_CHANGE_CONFIGURE_PORT;

            spoolWatcher.Start();

            using var waitEv = new ManualResetEventSlim();

            spoolWatcher.SpoolerNotificationReached += (o, e) =>
            {
                waitEv.Set();
            };

            var job = printQueue.AddJob();

            var bytes = Encoding.UTF8.GetBytes("Test printing");
            job.JobStream.Write(bytes, 0, bytes.Length);
            job.JobStream.Close();

            Assert.That(waitEv.Wait(timeout: TimeSpan.FromSeconds(10)));

            spoolWatcher.Stop();
        }

        [TearDown]
        public void TearDown()
        {
            printQueue.Dispose();
            spoolWatcher.Dispose();
        }
    }
}