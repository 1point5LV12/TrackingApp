using System.Collections.Generic;

namespace TrackingApp.TrainingDataService.Domain.Entities
{
    public class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Trainee> Trainees { get; set; }
    }
}
