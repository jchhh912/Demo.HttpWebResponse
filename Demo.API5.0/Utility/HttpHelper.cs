using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.API5._0.Utility
{
    public class HttpHelper
    {
        public readonly  IHttpClientFactory _httpClientFactory;
        public const string _clientName = "HttpHelper";  //定义客户端名称

        public HttpHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Get()
        {
            var client = _httpClientFactory.CreateClient(_clientName); //使用客户端名称获取客户端

            //使用client发起HTTP请求,这里使用相对路径来访问
            return await client.GetStringAsync("/std/service");
        }
    }
}
