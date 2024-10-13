using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Spatial;
using TrackingApp.SensorDataService.Data;
using TrackingApp.SensorDataService.Domain.Entities;
using TrackingApp.SensorDataService.Domain.Models;

namespace TrackingApp.SensorDataService.Logic
{
    public class TrainingService
    {
        private readonly AppDbContext _context;

        public TrainingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Training> AddTrainingWithDetailsAsync(TrainingModel model)
        {
            var training = new Training
            {
                Id = Guid.NewGuid(),
                TraineeId = model.TraineeId,
                TrainingTypeId = model.TrainingTypeId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            foreach (var data in model.TrainingData)
            {
                var trainingData = new TrainingData
                {
                    Id = Guid.NewGuid(),
                    TrainingId = training.Id,
                    Location = data.Location,
                    RecordDate = data.RecordDate
                };

                training.TrainingData.Add(trainingData);
            }

            _context.Trainings.Add(training);

            await _context.SaveChangesAsync();

            return training;
        }

        public async Task<TrainingResponseModel?> GetTrainingAsync(Guid trainingId)
        {
            var training = await _context.Trainings
                                    .Include(t => t.TrainingData)
                                    .FirstOrDefaultAsync(t => t.Id == trainingId);

            if (training == null) return null;

            return new TrainingResponseModel
            {
                Id = training.Id,
                TraineeId = training.TraineeId,
                TrainingTypeId = training.TrainingTypeId,
                StartDate = training.StartDate,
                EndDate = training.EndDate,
                TrainingData = training.TrainingData.Select(td => new TrainingDataResponseModel
                {
                    Id = td.Id,
                    Location = td.Location,
                    RecordDate = td.RecordDate
                }).ToList()
            };
        }

        public async Task<DbGeography?> GetCurrentPosition(Guid traineeId)
        {
            var training = await _context.Trainings
                          .Include(t => t.TrainingData)
                          .OrderByDescending(t => t.StartDate)
                          .FirstOrDefaultAsync(t => t.TraineeId == traineeId);

            if (training?.TrainingData == null) return null;

            var currentPosition = training.TrainingData.OrderByDescending(td => td.RecordDate).FirstOrDefault();

            return currentPosition?.Location;
        }

        public async Task<List<DbGeography?>?> GetTrainingData(Guid traineeId)
        {
            var training = await _context.Trainings
                          .Include(t => t.TrainingData)
                          .OrderByDescending(t => t.StartDate)
                          .FirstOrDefaultAsync(t => t.TraineeId == traineeId);

            if (training?.TrainingData == null) return null;

            return training.TrainingData
                        .OrderBy(td => td.RecordDate)
                        .Select(td => td.Location)
                        .ToList();
        }
    }
}
