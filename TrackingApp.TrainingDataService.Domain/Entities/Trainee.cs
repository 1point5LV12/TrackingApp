using System;

namespace TrackingApp.TrainingDataService.Domain.Entities
{
    public class Trainee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? RankId  { get; set; }

        public Rank? Rank { get; set; }

        public int? CountryId { get; set; }

        public Country? Country { get; set; }
    }
}
