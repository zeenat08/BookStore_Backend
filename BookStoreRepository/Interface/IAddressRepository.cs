using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        bool AddAddress(AddressModel addressDetails);
        bool EditAddress(AddressModel addressDetails);
        List<AddressModel> GetUserAddress(int userId);

    }
}
