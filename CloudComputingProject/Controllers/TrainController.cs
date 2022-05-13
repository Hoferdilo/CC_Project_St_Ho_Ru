using CloudComputingProject.Model.Dto;
using CloudComputingProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrains()
        {
            return Ok(await _trainService.GetTrains());
        }

        [HttpPost]
        public async Task<IActionResult> PostTrain([FromBody] TrainDto train)
        {
            return Ok(await _trainService.AddTrain(train));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrain([FromRoute] Guid id, [FromBody] TrainDto train)
        {
            return Ok(await _trainService.UpdateTrain(id, train));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain([FromRoute] Guid id, [FromBody] TrainDto train)
        {
            return Ok(await _trainService.UpdateTrain(id, train));
        }
    }
}
