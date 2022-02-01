using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository
{
    public interface IOrderRepository
    {
        bool AddOrder(List<CartModel> orderdetails);
    }
}
