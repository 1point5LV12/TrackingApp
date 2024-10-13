using System.Data.Entity.Spatial;

namespace TrackingApp.SensorDataService.Domain.Entities
{
    public class TrainingData
    {
        public Guid Id { get; set; }

        public DbGeography? Location { get; set; }

        public DateTime RecordDate { get; set; }

        public Guid TrainingId { get; set; }

        public Training Training { get; set; }
    }
}
