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
    public class CartController : ControllerBase
    {
        private readonly ICartManager manager;
        private readonly IConfiguration configuration;
        public CartController(ICartManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart([FromBody] CartModel cartModel)
        {
            try
            {
                var result = this.manager.AddToCart(cartModel);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book is added to cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Adding to bag failed ! try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(int cartId, int Quantity)
        {
            try
            {
                var result = this.manager.UpdateCart(cartId, Quantity);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "cart updated" });
                }
                else
                {
                    return this.BadRequest(new  { Status = false, Message = "Failed TryAgain" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

    }

}
