﻿using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository repository;
        public OrderManager(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public bool AddOrder(List<CartModel> orderdetails)
        {
            try
            {
                return this.repository.AddOrder(orderdetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<OrderModel> GetOrderList(int userId)
        {
            try
            {
                return this.repository.GetOrderList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
