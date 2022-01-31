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
    }
}
