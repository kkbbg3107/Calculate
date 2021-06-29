using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.InterFace;
using Microsoft.Extensions.Logging;
using System.Text;
using WebApi.Models;

namespace WebApi.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlankController : ControllerBase
    {
        private readonly IClear _clear;
        private readonly ILogger<BlankController> _logger;

        public BlankController(IClear clear, ILogger<BlankController> logger)
        {
            _clear = clear;
            _logger = logger;
        }

        // GET api/<BlankController>/5
        [HttpGet("{text}")]
        public async Task<Blank> GetBlank(string text)
        {
            StringBuilder inform = new StringBuilder(DateTime.Now.ToString());
            inform.Append($"BlankController的GetBlank被呼叫,傳入參數為{text}");

            _logger.LogWarning(2001, inform.ToString());

            var result = await _clear.GetBlank(text);
            return result;
        }
    }
}
