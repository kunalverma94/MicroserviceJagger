using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderDetailService.Model;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderDetailService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IHttpClientFactory _factory;

        public OrderDetailController(IHttpClientFactory factory)
        {
            this._factory = factory;
        }
        [HttpGet]
        [Route("{id}")]
        public Object Get(string id)
        {
            var userdata = GetData($"http://{Startup.UserApi}/user/1").GetAwaiter().GetResult();
            dynamic ordersjson = GetData($"http://{Startup.OrderApi}/order/1").GetAwaiter().GetResult();
            JArray ja = ordersjson.order;
            var r = new
            {
                userDetails = (userdata as JObject).ToObject<UserResponse>(),
                orders = ja.Select(a => ja[0].ToObject<Order>()).ToArray()
            };
            return r;
        }

        [HttpGet]
        [Route("/")]
        public string Get()
        {
            return $"{nameof(OrderDetailController)}.{nameof(Get)} Healthy \n cv:{Startup.configVersion}";
        }

        private async Task<object> GetData(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                var client = _factory.CreateClient("Api");
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, url);
                var responseMsg = await client.SendAsync(requestMsg);
                var data = await responseMsg.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject(data);
            }
            catch (Exception)
            {
                return new { error = "Error at " + url };
            }
        }
    }
}
