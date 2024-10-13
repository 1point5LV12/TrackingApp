using Microsoft.AspNetCore.Mvc;
using TrackingApp.TrainingDataService.Domain.Entities;
using TrackingApp.TrainingDataService.Logic;

namespace TrackingApp.TrainingDataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraineesController : ControllerBase
    {
        private readonly TraineeService _traineeService;

        public TraineesController(TraineeService traineeService)
        {
            _traineeService = traineeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainee>>> GetTrainees()
        {
            var trainees = await _traineeService.GetAllTraineesAsync();
            return Ok(trainees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainee>> GetTrainee(Guid id)
        {
            var trainee = await _traineeService.GetTraineeByIdAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }
            return Ok(trainee);
        }

        [HttpPost]
        public async Task<ActionResult<Trainee>> CreateTrainee(Trainee trainee)
        {
            var createdTrainee = await _traineeService.AddTraineeAsync(trainee);
            return CreatedAtAction(nameof(GetTrainee), new { id = createdTrainee.Id }, createdTrainee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainee(Guid id, Trainee trainee)
        {
            if (id != trainee.Id)
            {
                return BadRequest();
            }

            var updated = await _traineeService.UpdateTraineeAsync(trainee);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee(Guid id)
        {
            var deleted = await _traineeService.DeleteTraineeAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
