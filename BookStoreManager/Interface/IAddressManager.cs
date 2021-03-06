using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        bool AddAddress(AddressModel addressDetails);
        bool EditAddress(AddressModel addressDetails);
        List<AddressModel> GetUserAddress(int userId);
    }
}
