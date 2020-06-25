namespace SpoolerWatcher.Events
{
    public class NotificationInfo
    {
        public uint Id { get; set; }
        public NotifyType Type { get; set; }
        public NotificationInfoData InfoData { get; set; }
    }
}
