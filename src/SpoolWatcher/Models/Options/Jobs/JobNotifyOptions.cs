namespace SpoolerWatcher
{
    public class JobNotifyOptions : NotifyOptions
    {
        public override NotifyType NotifyType => NotifyType.Job;

        public JobNotifyFields[] JobNotifyFields { get; }

        public JobNotifyOptions(JobNotifyFields[] jobNotifyFields)
        {
            JobNotifyFields = jobNotifyFields;
        }
    }
}
