using Microsoft.AspNetCore.Mvc;
using TrackingApp.TrainingDataService.Logic;
using TrackingApp.TrainingDataService.Domain.Entities;

namespace TrackingApp.TrainingDataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingTypesController : ControllerBase
    {
        private readonly TrainingTypeService _trainingTypeService;

        public TrainingTypesController(TrainingTypeService trainingTypeService)
        {
            _trainingTypeService = trainingTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingType>>> GetTrainingTypes()
        {
            var trainingTypes = await _trainingTypeService.GetAllTrainingTypesAsync();
            return Ok(trainingTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingType>> GetTrainingType(int id)
        {
            var trainingType = await _trainingTypeService.GetTrainingTypeByIdAsync(id);
            if (trainingType == null)
            {
                return NotFound();
            }
            return Ok(trainingType);
        }

        [HttpPost]
        public async Task<ActionResult<TrainingType>> CreateTrainingType(TrainingType trainingType)
        {
            var createdTrainingType = await _trainingTypeService.AddTrainingTypeAsync(trainingType);
            return CreatedAtAction(nameof(GetTrainingType), new { id = createdTrainingType.Id }, createdTrainingType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainingType(int id, TrainingType trainingType)
        {
            if (id != trainingType.Id)
            {
                return BadRequest();
            }

            var updated = await _trainingTypeService.UpdateTrainingTypeAsync(trainingType);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingType(int id)
        {
            var deleted = await _trainingTypeService.DeleteTrainingTypeAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
