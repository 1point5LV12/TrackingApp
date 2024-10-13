namespace TrackingApp.SensorDataService.Domain.Entities
{
    public class Training
    {
        public Guid Id { get; set; }
        
        public Guid TraineeId { get; set; }

        public int TrainingTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<TrainingData> TrainingData { get; set; } = new List<TrainingData>();
    }
}
