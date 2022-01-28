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
                if (result != null)
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginDetails)
        {
            try
            {
                var result = await this.manager.Login(loginDetails);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LoginModel>() { Status = true, Message = "Login Successfully!", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Login, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("resetPassword")]
        public async Task<IActionResult> Resetpassword([FromBody] ResetPasswordModel resetPassword)
        {
            try
            {
                var result = await this.manager.ResetPassword(resetPassword);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<ResetPasswordModel> { Status = true, Message = " Reset password successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to Reset Your Password, Try again" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
