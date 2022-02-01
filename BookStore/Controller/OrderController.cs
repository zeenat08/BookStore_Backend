using BookStoreManager.Interface;
using BookStoreModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager manager;
        private readonly IConfiguration configuration;
        public OrderController(IOrderManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("PlaceOrders")]
        public IActionResult AddOrder([FromBody] List<CartModel> orderDetails)
        {
            try
            {
                var result = this.manager.AddOrder(orderDetails);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Order placed successfully" });
                }
                else
                {
                    return this.BadRequest(new  { Status = false, Message = "Failed to place order, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

    }
}
