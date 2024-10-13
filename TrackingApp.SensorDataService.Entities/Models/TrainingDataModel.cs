using System.Data.Entity.Spatial;

namespace TrackingApp.SensorDataService.Domain.Models
{
    public class TrainingDataModel
    {
        public DbGeography? Location { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
