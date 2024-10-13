using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingApp.SensorDataService.Domain.Models
{
    public class TrainingDataResponseModel
    {
        public Guid Id { get; set; }
        public DbGeography? Location { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
