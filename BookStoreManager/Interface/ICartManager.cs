using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        bool AddToCart(CartModel cartModel);
    }
}
