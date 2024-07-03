namespace RunningAdvisor.ViewModels
{
    public class BeginnerRunningDataViewModel
    {
        public int Id { get; set; }
        public TimeSpan Time3k { get; set; }
        public int AvgHeartRate3k { get; set; }
        public TimeSpan Time5k { get; set; }
        public int AvgHeartRate5k { get; set; }
        public TimeSpan Time10k { get; set; }
        public int AvgHeartRate10k { get; set; }
    }
}
