namespace TrackingApp.SensorDataService.Domain.Models
{
    public class TrainingModel
    {
        public Guid TraineeId { get; set; }
        public int TrainingTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // TrainingData information
        public List<TrainingDataModel> TrainingData { get; set; } = new List<TrainingDataModel>();
    }
}
