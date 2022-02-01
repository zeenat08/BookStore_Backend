using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        bool AddToCart(CartModel cartModel);
        bool UpdateCart(int cartId, int Quantity);
    }
}
