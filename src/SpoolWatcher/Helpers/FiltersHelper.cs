using System;
using System.Collections.Generic;

namespace SpoolerWatcher.Helpers
{
    internal static class FiltersHelper
    {
        internal static IEnumerable<PrinterNotifyField> ToPrinterNotifyFields(this PrinterNotifyFilters printerNotifyFilter)
        {
            foreach (Enum value in Enum.GetValues(printerNotifyFilter.GetType()))
            {
                if (printerNotifyFilter.HasFlag(value))
                {
                    yield return (PrinterNotifyField)Enum.Parse(typeof(PrinterNotifyField), value.ToString());
                }
            }
        }

        internal static IEnumerable<JobNotifyField> ToJobNotifyFields(this JobNotifyFilters jobNotifyFilter)
        {
            foreach (Enum value in Enum.GetValues(jobNotifyFilter.GetType()))
            {
                if (jobNotifyFilter.HasFlag(value))
                {
                    yield return (JobNotifyField)Enum.Parse(typeof(JobNotifyField), value.ToString());
                }
            }
        }
    }
}
