using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class WishlistManager : IWishlistManager
    {
        private readonly IWishlistRepository repository;
        public WishlistManager(IWishlistRepository repository)
        {
            this.repository = repository;
        }

        public bool AddToWishList(WishlistModel wishListmodel)
        {
            try
            {
                return this.repository.AddToWishList(wishListmodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<WishlistModel> GetWishList(int userId)
        {
            try
            {
                return this.repository.GetWishList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteWishlist(int WishlistId)
        {
            try
            {
                return this.repository.DeleteWishlist(WishlistId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
