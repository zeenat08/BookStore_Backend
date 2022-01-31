using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repository;
        public AddressManager(IAddressRepository repository)
        {
            this.repository = repository;
        }


        public bool AddAddress(AddressModel addressDetails)
        {
            try
            {
                return this.repository.AddAddress(addressDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
