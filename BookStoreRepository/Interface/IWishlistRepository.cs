using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Interface
{
    public interface IWishlistRepository
    {
        bool AddToWishList(WishlistModel wishListmodel);
        List<WishlistModel> GetWishList(int userId);
    }
}
