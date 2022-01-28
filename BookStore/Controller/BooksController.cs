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
    public class BooksController : ControllerBase
    {
        private readonly IBookManager manager;
        private readonly IConfiguration configuration;
        public BooksController(IBookManager manager, IConfiguration configuration)
        {
            this.manager = manager;
            this.configuration = configuration;
        }


        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookModel bookmodel)
        {
            try
            {
                var result = await this.manager.AddBook(bookmodel);
                if (result)
                {

                    return this.Ok(new ResponseModel<BookModel>() { Status = true, Message = "Added New Book Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { Status = false, Message = "Failed to add new book" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message });

            }
        }

        [HttpGet]
        [Route("GetBook")]
        public IActionResult GetBook(int bookId)
        {
            var result = this.manager.GetBook(bookId);
            try
            {
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book is retrived", data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(BookModel Bookmodel)
        {
            try
            {
                var result = this.manager.UpdateBook(Bookmodel);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book updated Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to updated Book" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

    }
}
