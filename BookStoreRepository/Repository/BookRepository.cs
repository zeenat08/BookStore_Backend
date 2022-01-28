using BookStoreModel;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        public async Task<bool> AddBook(BookModel bookmodel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {

                    SqlCommand sqlCommand = new SqlCommand("spAddBook", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@BookName", bookmodel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookmodel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookmodel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookmodel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@Quantity", bookmodel.Quantity);
                    sqlCommand.Parameters.AddWithValue("@Price", bookmodel.Price);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookmodel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookmodel.Rating);
                    sqlCommand.Parameters.AddWithValue("@RatingCount", bookmodel.RatingCount);
                    int result = await sqlCommand.ExecuteNonQueryAsync();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

    }
}
