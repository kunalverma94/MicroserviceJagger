using Microsoft.AspNetCore.Mvc;
using System;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("/")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public Object Get(string id = "1")
        {
            return new
            {
                Order = new object[] {
         new   {
      orderId =1,
      orderAmount =250,
      orderDate= "14-Apr-2020"
    },     new   {
      orderId =2,
      orderAmount =450,
      orderDate= "15-Apr-2020"
    }
            }
            };

        }


        [HttpGet]
        public string Get()
        {
            return $"{nameof(OrderController)}.{nameof(Get)} Healthy \n cv:{Startup.configVersion}";
        }
    }
}
