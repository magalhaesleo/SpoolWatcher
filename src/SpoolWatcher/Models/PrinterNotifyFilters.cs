using System;

namespace SpoolerWatcher
{
    [Flags]
    public enum PrinterNotifyFilters
    {
        SERVER_NAME = 1,
        PRINTER_NAME = 2,
        SHARE_NAME = 4,
        PORT_NAME = 8,
        DRIVER_NAME = 16,
        COMMENT = 32,
        LOCATION = 64,
        DEVMODE = 128,
        SEPFILE = 256,
        PRINT_PROCESSOR = 512,
        PARAMETERS = 1024,
        DATATYPE = 2048,
        SECURITY_DESCRIPTOR = 4096,
        ATTRIBUTES = 8192,
        PRIORITY = 16384,
        DEFAULT_PRIORITY = 32768,
        START_TIME = 65536,
        UNTIL_TIME = 131072,
        STATUS = 262144,
        STATUS_STRING = 524288,
        CJOBS = 1048576,
        AVERAGE_PPM = 2097152,
        TOTAL_PAGES = 4194304,
        PAGES_PRINTED = 8388608,
        TOTAL_BYTES = 16777216,
        BYTES_PRINTED = 33554432,
        OBJECT_GUID = 67108864,
        FRIENDLY_NAME = 134217728,
    }
}
