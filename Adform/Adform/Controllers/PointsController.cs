using Common.Entities;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Adform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private const bool USE_TIMEOUT = true; // Opt-In to 5 sec rule
        private const int REQUEST_TIMEOUT = 5000; // 5 seconds request limit
        private readonly IPointService _pointService;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public PointsController(IPointService pointService)
        {
            _pointService = pointService;
            _cancellationTokenSource = new CancellationTokenSource(REQUEST_TIMEOUT);
        }

        [HttpPost("Import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ImportAsync(List<Point> points)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var id = await _pointService.InsertPointList(points, USE_TIMEOUT ? _cancellationTokenSource.Token : default);
                return Ok(id);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromBody] Point point, Guid pointListId)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var status = await _pointService.InsertPoint(point, pointListId, USE_TIMEOUT ? _cancellationTokenSource.Token : default);
                return Ok(status);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
        }

        [HttpPost("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromBody] Point point, Guid pointListId)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var status = await _pointService.DeletePoint(point, pointListId, USE_TIMEOUT ? _cancellationTokenSource.Token : default);
                return Ok(status);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
        }

        [HttpGet("GetSquares")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSquaresAsync(Guid pointListId)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var squares = await _pointService.GetSquares(pointListId, USE_TIMEOUT ? _cancellationTokenSource.Token : default);
                return Ok(squares);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
        }
    }
}