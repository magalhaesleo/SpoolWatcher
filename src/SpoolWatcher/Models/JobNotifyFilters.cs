using System;

namespace SpoolerWatcher
{
    [Flags]
    public enum JobNotifyFilters
    {
        PRINTER_NAME = 1,
        MACHINE_NAME = 2,
        PORT_NAME = 4,
        USER_NAME = 8,
        NOTIFY_NAME = 16,
        DATATYPE = 32,
        PRINT_PROCESSOR = 64,
        PARAMETERS = 128,
        DRIVER_NAME = 256,
        DEVMODE = 512,
        STATUS = 1024,
        STATUS_STRING = 2048,
        SECURITY_DESCRIPTOR = 4096,
        DOCUMENT = 8192,
        PRIORITY = 16384,
        POSITION = 32768,
        SUBMITTED = 65536,
        START_TIME = 131072,
        UNTIL_TIME = 262144,
        TIME = 524288,
        TOTAL_PAGES = 1048576,
        PAGES_PRINTED = 2097152,
        TOTAL_BYTES = 4194304,
        BYTES_PRINTED = 8388608
    }
}
