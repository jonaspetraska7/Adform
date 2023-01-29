using Microsoft.AspNetCore.Mvc;

namespace Adform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly ILogger<PointsController> _logger;

        public PointsController(ILogger<PointsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Import()
        {

        }

        [HttpGet]
        public void Add()
        {

        }

        [HttpGet]
        public void Delete()
        {

        }

        [HttpGet]
        public void GetSquares()
        {

        }
    }
}