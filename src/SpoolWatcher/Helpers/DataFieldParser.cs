using SpoolerWatcher.Events;
using SpoolerWatcher.Native.Structures;

using System.Runtime.InteropServices;

namespace SpoolerWatcher.Helpers
{
    internal static class DataFieldParser
    {
        internal static NotificationInfoData GetJobTypeData(PrinterNotifyInfoData infoData)
        {
            switch ((JobNotifyFields)infoData.Field)
            {
                case JobNotifyFields.PRINTER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.PRINTER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.MACHINE_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.MACHINE_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.PORT_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.PORT_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.USER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.USER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.NOTIFY_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.NOTIFY_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.DATATYPE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.DATATYPE,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.PRINT_PROCESSOR:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.PRINT_PROCESSOR,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.PARAMETERS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.PARAMETERS,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.DRIVER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.DRIVER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.DEVMODE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.DEVMODE
                    };
                case JobNotifyFields.STATUS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.STATUS,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.STATUS_STRING:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.STATUS_STRING,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.DOCUMENT:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.DOCUMENT,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyFields.PRIORITY:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.PRIORITY,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.POSITION:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.POSITION,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.SUBMITTED:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.SUBMITTED,
                        Data = Marshal.PtrToStructure<SystemTime>(infoData.NotifyData.Data.pBuf).ToDateTime()
                    };
                case JobNotifyFields.START_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.START_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.UNTIL_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.UNTIL_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.TOTAL_PAGES:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.TOTAL_PAGES,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.PAGES_PRINTED:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.PAGES_PRINTED,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.TOTAL_BYTES:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.TOTAL_BYTES,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyFields.BYTES_PRINTED:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyFields.TOTAL_BYTES,
                        Data = infoData.NotifyData.adwData0
                    };
                default:
                    break;
            }

            return null;
        }

        internal static NotificationInfoData GetPrinterTypeData(PrinterNotifyInfoData infoData)
        {
            switch ((PrinterNotifyFields)infoData.Field)
            {
                case PrinterNotifyFields.PRINTER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.PRINTER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.SHARE_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.SHARE_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.PORT_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.PORT_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.DRIVER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.DRIVER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.COMMENT:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.COMMENT,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.LOCATION:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.LOCATION,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.DEVMODE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.DEVMODE
                    };
                case PrinterNotifyFields.SEPFILE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.SEPFILE,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.PRINT_PROCESSOR:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.PRINT_PROCESSOR,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.PARAMETERS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.PARAMETERS,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.DATATYPE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.DATATYPE,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyFields.SECURITY_DESCRIPTOR:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.SECURITY_DESCRIPTOR
                    };
                case PrinterNotifyFields.ATTRIBUTES:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.ATTRIBUTES,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.PRIORITY:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.PRIORITY,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.DEFAULT_PRIORITY:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.DEFAULT_PRIORITY,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.START_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.START_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.UNTIL_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.UNTIL_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.STATUS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.STATUS,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.CJOBS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.CJOBS,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.AVERAGE_PPM:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.AVERAGE_PPM,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyFields.OBJECT_GUID:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.OBJECT_GUID
                    };
                case PrinterNotifyFields.FRIENDLY_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyFields.FRIENDLY_NAME
                    };
                default:
                    break;
            }

            return null;
        }
    }
}
