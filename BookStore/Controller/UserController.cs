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
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly IConfiguration configuration;
        public UserController(IUserManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData)
        {
            try
            {
                var result = await this.manager.Register(userData);
                if (result !=null)
                {
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Added New User Successfully !", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add new user, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
