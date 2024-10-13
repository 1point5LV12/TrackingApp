using Microsoft.EntityFrameworkCore;
using TrackingApp.TrainingDataService.Data;
using TrackingApp.TrainingDataService.Domain.Entities;

namespace TrackingApp.TrainingDataService.Logic
{
    public class TraineeService
    {
        private readonly AppDbContext _context;

        public TraineeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Trainee> AddTraineeAsync(Trainee trainee)
        {
            _context.Trainees.Add(trainee);
            await _context.SaveChangesAsync();
            return trainee;
        }

        public async Task<IEnumerable<Trainee>> GetAllTraineesAsync()
        {
            return await _context.Trainees
                .Include(t => t.Country)
                .Include(t => t.Rank) 
                .ToListAsync();
        }

        public async Task<Trainee?> GetTraineeByIdAsync(Guid id)
        {
            return await _context.Trainees
                .Include(t => t.Country)
                .Include(t => t.Rank)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateTraineeAsync(Trainee trainee)
        {
            var existingTrainee = await _context.Trainees.FindAsync(trainee.Id);
            if (existingTrainee == null)
            {
                return false;
            }

            existingTrainee.Name = trainee.Name;
            existingTrainee.RankId = trainee.RankId;
            existingTrainee.CountryId = trainee.CountryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTraineeAsync(Guid id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee == null)
            {
                return false;
            }

            _context.Trainees.Remove(trainee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
