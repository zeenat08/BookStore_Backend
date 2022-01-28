using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<RegisterModel> Register(RegisterModel userData)
        {
            try
            {
                return await this.repository.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoginModel> Login(LoginModel loginDetails)
        {
            try
            {
                return await this.repository.Login(loginDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                return await this.repository.ResetPassword(resetPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
