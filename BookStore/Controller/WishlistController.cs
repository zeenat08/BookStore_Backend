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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistManager manager;
        private readonly IConfiguration configuration;

        public WishlistController(IWishlistManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("AddToWishList")]
        public IActionResult AddToWishList([FromBody] WishlistModel wishListmodel)
        {
            try
            {
                var result = this.manager.AddToWishList(wishListmodel);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added To wish list Successfully !" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add to wish list, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getwishlist")]
        public IActionResult GetWishList(int userId)
        {
            var result = this.manager.GetWishList(userId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Wish List successfully retrived", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No WishList available" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteWishlist")]
        public IActionResult DeleteWishlist(int WishlistId)
        {
            try
            {
                var result = this.manager.DeleteWishlist(WishlistId);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed wishlist item Successfully !" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Remove wishlist item, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }

}
