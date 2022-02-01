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
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager manager;
        private readonly IConfiguration configuration;
        public AddressController(IAddressManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("AddUserAddress")]
        public IActionResult AddAddress([FromBody] AddressModel addressDetails)
        {
            try
            {
                var result = this.manager.AddAddress(addressDetails);
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New User Address Successfully !" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add user address, Try again!" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("EditAddress")]
        public IActionResult EditAddress([FromBody] AddressModel addressDetails)
        {
            var result = this.manager.EditAddress(addressDetails);
            try
            {
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Address updated successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to update address" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("GetUserAddress")]
        public IActionResult GetUserAddress(int userId)
        {
            var result = this.manager.GetUserAddress(userId);
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Address successfully retrived", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No address available" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
    }
}
