using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Interface
{
    public interface IWishlistManager
    {
        bool AddToWishList(WishlistModel wishListmodel);
    }
}
