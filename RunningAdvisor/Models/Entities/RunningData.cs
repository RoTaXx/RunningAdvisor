namespace RunningAdvisor.Models.Entities
{
    public class RunningData
    {
        public int Id { get; set; } 
        public TimeSpan Time10k { get; set; } 
        public TimeSpan Time21k { get; set; } 
        public TimeSpan Time42k { get; set; } 
        public int AvgHeartRate10k { get; set; } 
        public int AvgHeartRate21k { get; set; } 
        public int AvgHeartRate42k { get; set; } 
        public int Cadence { get; set; }

    }
}
