using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        Task <bool> AddBook(BookModel bookmodel);
        BookModel GetBook(int bookId);
    }
}
