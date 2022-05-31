using CloudComputingProject.Model;
using CloudComputingProject.Model.Dto;
using CloudComputingProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrains()
        {
            return Ok(await _stationService.GetAllStations());
        }

        [HttpPost]
        public async Task<IActionResult> PostTrain([FromBody] Station station)
        {
            return Ok(await _stationService.InsertStation(station));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrain([FromRoute] Guid id, [FromBody] Station station)
        {
            return Ok(await _stationService.UpdateStation(id, station));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain([FromRoute] Guid id)
        {
            return Ok(await _stationService.DeleteStation(id));
        }

    }
}
