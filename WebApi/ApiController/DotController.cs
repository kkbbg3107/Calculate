using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using WebApi.InterFace;
using WebApi.Models;

namespace WebApi.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotController : ControllerBase
    {
        private readonly IDot _dot;
        private readonly ILogger<DotController> _logger;

        public DotController(IDot dot, ILogger<DotController> logger)
        {
            _dot = dot;
            _logger = logger;
        }
        // GET: api/<DotController>
        [HttpGet]
        public async Task<Dot> GetDot()
        {
            StringBuilder inform = new StringBuilder(DateTime.Now.ToString());
            inform.Append("TextController的GetDot方法被呼叫");

            _logger.LogWarning(2001, inform.ToString());

            var result = await _dot.GetDot();

            return result;
        }
    }
}
