using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.InterFace;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivController : ControllerBase
    {
        private readonly IDiv _div;
        private readonly ILogger<DivController> _logger;

        public DivController(IDiv div, ILogger<DivController> logger)
        {
            _div = div;
            _logger = logger;
        }
        // GET: api/<DivController>
        [HttpGet]
        public async Task<Div> GetDiv()
        {
            StringBuilder inform = new StringBuilder(DateTime.Now.ToString());
            inform.Append("TextController的GetDot方法被呼叫");

            _logger.LogWarning(2001, inform.ToString());

            var result = await _div.GetDiv();

            return result;
        }
    }
}
