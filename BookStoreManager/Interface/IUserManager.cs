using BookStoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel userData);
        Task<LoginModel> Login(LoginModel loginDetails);
        Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword);
        Task<bool> ForgotPassword(string email);
    }
}
