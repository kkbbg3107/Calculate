using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.InterFace;
using Microsoft.Extensions.Logging;
using System.Text;
using WebApi.Models;

namespace WebApi.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly IGetData _getData;
        private readonly ILogger<TextController> _logger;

        public TextController(IGetData getData, ILogger<TextController> logger)
        {
            _getData = getData;
            _logger = logger;
        }

        // GET api/<TextController>/5
        [HttpGet("{text}")]
        public async Task<Num> GetText(string text)
        {
            StringBuilder inform = new StringBuilder(DateTime.Now.ToString());
            inform.Append($"TextController的GetText方法被呼叫,傳入參數為{text}");

            _logger.LogWarning(2001, inform.ToString());

            var result = await _getData.GetText(text);

            return result;
        }        
    }
}
