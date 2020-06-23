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
        private SpoolWatcher spoolWatcher;
        private PrintQueue printQueue;

        [SetUp]
        public void Setup()
        {
            var fields = new JobNotifyFields[] { JobNotifyFields.Status };

            var jOptions = new JobNotifyOptions(fields);

            printQueue = LocalPrintServer.GetDefaultPrintQueue();

            spoolWatcher = new SpoolWatcher(printQueue.Name, PrinterNotifyCategory.CategoryAll, jOptions);
        }

        [Test]
        public void Print_Test_Page_Should_Call_Event()
        {
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

            Assert.That(waitEv.Wait(timeout: TimeSpan.FromSeconds(5)));

            spoolWatcher.Stop();

            spoolWatcher.Dispose();
        }
    }
}