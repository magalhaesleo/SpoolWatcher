using SpoolerWatcher.Events;
using SpoolerWatcher.Native.Structures;

using System.Runtime.InteropServices;

namespace SpoolerWatcher.Helpers
{
    internal static class DataFieldParser
    {
        internal static NotificationInfoData GetJobTypeData(PrinterNotifyInfoData infoData)
        {
            switch ((JobNotifyField)infoData.Field)
            {
                case JobNotifyField.PRINTER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.PRINTER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.MACHINE_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.MACHINE_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.PORT_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.PORT_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.USER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.USER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.NOTIFY_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.NOTIFY_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.DATATYPE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.DATATYPE,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.PRINT_PROCESSOR:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.PRINT_PROCESSOR,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.PARAMETERS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.PARAMETERS,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.DRIVER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.DRIVER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.DEVMODE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.DEVMODE,
                        Data = infoData.NotifyData.Data.pBuf
                    };
                case JobNotifyField.STATUS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.STATUS,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.STATUS_STRING:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.STATUS_STRING,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.DOCUMENT:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.DOCUMENT,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case JobNotifyField.PRIORITY:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.PRIORITY,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.POSITION:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.POSITION,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.SUBMITTED:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.SUBMITTED,
                        Data = Marshal.PtrToStructure<SystemTime>(infoData.NotifyData.Data.pBuf).ToDateTime()
                    };
                case JobNotifyField.START_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.START_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.UNTIL_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.UNTIL_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.TOTAL_PAGES:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.TOTAL_PAGES,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.PAGES_PRINTED:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.PAGES_PRINTED,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.TOTAL_BYTES:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.TOTAL_BYTES,
                        Data = infoData.NotifyData.adwData0
                    };
                case JobNotifyField.BYTES_PRINTED:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)JobNotifyField.TOTAL_BYTES,
                        Data = infoData.NotifyData.adwData0
                    };
                default:
                    break;
            }

            return null;
        }

        internal static NotificationInfoData GetPrinterTypeData(PrinterNotifyInfoData infoData)
        {
            switch ((PrinterNotifyField)infoData.Field)
            {
                case PrinterNotifyField.PRINTER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.PRINTER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.SHARE_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.SHARE_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.PORT_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.PORT_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.DRIVER_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.DRIVER_NAME,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.COMMENT:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.COMMENT,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.LOCATION:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.LOCATION,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.DEVMODE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.DEVMODE,
                        Data = infoData.NotifyData.Data.pBuf
                    };
                case PrinterNotifyField.SEPFILE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.SEPFILE,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.PRINT_PROCESSOR:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.PRINT_PROCESSOR,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.PARAMETERS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.PARAMETERS,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.DATATYPE:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.DATATYPE,
                        Data = Marshal.PtrToStringUni(infoData.NotifyData.Data.pBuf)
                    };
                case PrinterNotifyField.SECURITY_DESCRIPTOR:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.SECURITY_DESCRIPTOR,
                        Data = infoData.NotifyData.Data.pBuf
                    };
                case PrinterNotifyField.ATTRIBUTES:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.ATTRIBUTES,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.PRIORITY:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.PRIORITY,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.DEFAULT_PRIORITY:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.DEFAULT_PRIORITY,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.START_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.START_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.UNTIL_TIME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.UNTIL_TIME,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.STATUS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.STATUS,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.CJOBS:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.CJOBS,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.AVERAGE_PPM:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.AVERAGE_PPM,
                        Data = infoData.NotifyData.adwData0
                    };
                case PrinterNotifyField.OBJECT_GUID:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.OBJECT_GUID
                    };
                case PrinterNotifyField.FRIENDLY_NAME:
                    return new NotificationInfoData
                    {
                        DataType = (ushort)PrinterNotifyField.FRIENDLY_NAME
                    };
                default:
                    break;
            }

            return null;
        }
    }
}
