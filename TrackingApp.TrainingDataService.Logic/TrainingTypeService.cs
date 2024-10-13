using Microsoft.EntityFrameworkCore;
using TrackingApp.TrainingDataService.Data;
using TrackingApp.TrainingDataService.Domain.Entities;

namespace TrackingApp.TrainingDataService.Logic
{
    public class TrainingTypeService
    {
        private readonly AppDbContext _context;

        public TrainingTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingType> AddTrainingTypeAsync(TrainingType trainingType)
        {
            _context.TrainingTypes.Add(trainingType);
            await _context.SaveChangesAsync();
            return trainingType;
        }

        public async Task<IEnumerable<TrainingType>> GetAllTrainingTypesAsync()
        {
            return await _context.TrainingTypes.ToListAsync();
        }

        public async Task<TrainingType?> GetTrainingTypeByIdAsync(int id)
        {
            return await _context.TrainingTypes.FirstOrDefaultAsync(tt => tt.Id ==id);
        }

        public async Task<bool> UpdateTrainingTypeAsync(TrainingType trainingType)
        {
            var existingTrainingType = await _context.TrainingTypes.FindAsync(trainingType.Id);
            if (existingTrainingType == null)
            {
                return false;
            }

            existingTrainingType.Name = trainingType.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTrainingTypeAsync(int id)
        {
            var trainingType = await _context.TrainingTypes.FindAsync(id);
            if (trainingType == null)
            {
                return false;
            }

            _context.TrainingTypes.Remove(trainingType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
