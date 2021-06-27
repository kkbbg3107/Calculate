using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;
using WebApi.Models;

namespace WebApi.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<CalculateController> _logger; 

        /// <summary>
        /// 建立相依
        /// </summary>
        /// <param name="dataService">抽象servie邏輯</param>
        /// <param name="logger">檢查api輸入正確性</param>
        public CalculateController(IDataService dataService, ILogger<CalculateController> logger)
        {
            this._dataService = dataService;
            this._logger = logger;
        }
       
        // POST api/<CalculateController>
        [HttpPost]
        public async Task<NumGroup.NumSingleResult> PostResult([FromBody] string data) 
        {
            StringBuilder inform = new StringBuilder(DateTime.Now.ToString());
            inform.Append($"CalculateController的PostResult方法被呼叫,傳入的參數為{data}");

            this._logger.LogWarning(2001, inform.ToString());

            var result = await this._dataService.PostNumber(data);
            return result;
        }       
    }
}
