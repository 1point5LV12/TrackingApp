using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Spatial;
using TrackingApp.SensorDataService.Domain.Models;
using TrackingApp.SensorDataService.Logic;

namespace TrackingApp.SensorDataService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingsController : ControllerBase
    {
        private readonly TrainingService _trainingService;

        public TrainingsController(TrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTraining(TrainingModel model)
        {
            var createdTraining = await _trainingService.AddTrainingWithDetailsAsync(model);
            return CreatedAtAction(nameof(GetTraining), new { id = createdTraining.Id }, createdTraining.Id);
        }

        [HttpGet]
        [Route("GetTraining")]
        public async Task<ActionResult<TrainingResponseModel>> GetTraining(Guid id)
        {
            var training = await _trainingService.GetTrainingAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            return Ok(training);
        }

        [HttpGet]
        [Route("GetCurrentPosition")]
        public async Task<ActionResult<DbGeography>> GetCurrentPosition(Guid traineedId)
        {
            var position = await _trainingService.GetCurrentPosition(traineedId);
            if (position == null)
            {
                return NotFound();
            }
            return Ok(position);
        }

        [HttpGet]
        [Route("GetTrainingData")]
        public async Task<ActionResult<List<DbGeography>>> GetTrainingData(Guid traineedId)
        {
            var positions = await _trainingService.GetTrainingData(traineedId);
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }
    }
}
