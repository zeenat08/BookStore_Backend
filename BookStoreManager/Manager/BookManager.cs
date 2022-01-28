using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository repository;
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddBook(BookModel bookmodel)
        {
            try
            {
                return await this.repository.AddBook(bookmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public BookModel GetBook(int bookId)
        {
            try
            {
                return this.repository.GetBook(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateBook(BookModel bookmodel)
        {
            try
            {
                return this.repository.UpdateBook(bookmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.repository.DeleteBook(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
