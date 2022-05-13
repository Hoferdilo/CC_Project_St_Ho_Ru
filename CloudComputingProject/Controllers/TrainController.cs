using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTrains()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostTrain()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrain([FromRoute] Guid id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrain([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}
