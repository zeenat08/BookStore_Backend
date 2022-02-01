using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        bool AddToCart(CartModel cartModel);
        bool UpdateCart(int cartId, int Quantity);
        List<CartModel> GetCart(int userId);
    }
}
