using Common.Entities;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Adform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointsController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpPost("Import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ImportAsync(List<Point> points)
        {
            if (!ModelState.IsValid) return BadRequest();

            var id = await _pointService.InsertPointList(points);

            return Ok(id);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] Point point, Guid pointListId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var status = await _pointService.InsertPoint(point, pointListId);

            return Ok(status);
        }

        [HttpPost("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromBody] Point point, Guid pointListId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var status = await _pointService.DeletePoint(point, pointListId);

            return Ok(status);
        }

        [HttpGet("GetSquares")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSquaresAsync(Guid pointListId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var squares = await _pointService.GetSquares(pointListId);

            return Ok(squares);
        }
    }
}