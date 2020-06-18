using NUnit.Framework;
using SpoolWatcher;
using SpoolWatcher.Options.Jobs;

namespace SpoolWatchers.Tests
{
    public class SpoolWatcherIntegrationTest
    {
        private SpoolWatcher.SpoolWatcher spoolWatcher;
        [SetUp]
        public void Setup()
        {
            var fields = new JobNotifyFields[] { JobNotifyFields.Status };

            var jOptions = new JobNotifyOptions(fields);

            spoolWatcher = new SpoolWatcher.SpoolWatcher("Brother Color Type4 Class Driver", PrinterNotifyCategory.CategoryAll, jOptions);
        }

        [Test]
        public void Test1()
        {
            spoolWatcher.Start();

            while (true)
            {
            }

            spoolWatcher.Stop();
        }
    }
}