namespace TrackingApp.SensorDataService.Domain.Models
{
    public class TrainingResponseModel
    {
        public Guid Id { get; set; }
        public Guid TraineeId { get; set; }
        public int TrainingTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TrainingDataResponseModel> TrainingData { get; set; } = new List<TrainingDataResponseModel>();
    }
}
