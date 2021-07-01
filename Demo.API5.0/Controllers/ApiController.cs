using Demo.API5._0.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.API5._0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {

        private readonly ILogger<ApiController> _logger;
        private readonly HttpHelper _httpHelper;
        public ApiController(ILogger<ApiController> logger,HttpHelper httpHelper)
        {
            _logger = logger;
            _httpHelper = httpHelper;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await _httpHelper.Get();
        }
    }
}
