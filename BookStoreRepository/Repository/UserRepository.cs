using BookStoreModel;
using BookStoreRepository.Interface;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        public string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public async Task<RegisterModel> Register(RegisterModel userData)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_AddUser", sqlConnection);
                    userData.Password = EncryptPassword(userData.Password);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@UserName", userData.UserName);
                    sqlCommand.Parameters.AddWithValue("@Email", userData.Email);
                    sqlCommand.Parameters.AddWithValue("@PhoneNo", userData.PhoneNo);
                    sqlCommand.Parameters.AddWithValue("@Password", userData.Password);

                    int result = await sqlCommand.ExecuteNonQueryAsync();

                    if (result > 0)
                        return userData;
                    else
                        return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public async Task<LoginModel> Login(LoginModel loginDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUserLogin", sqlConnection);
                    loginDetails.Password = EncryptPassword(loginDetails.Password);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@Email", loginDetails.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", loginDetails.Password);
                    sqlCommand.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;

                    await sqlCommand.ExecuteNonQueryAsync();
                    var result = sqlCommand.Parameters["@user"].Value;
                    if (!(result is DBNull))
                    {
                        if (Convert.ToInt32(result) == 2)
                        {
                            GetUserDetails(loginDetails.Email);
                            return loginDetails;
                        }
                        return null;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void GetUserDetails(string email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "SELECT * FROM RegUser WHERE Email = @Email";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Email", email);
                    sqlConnection.Open();

                    RegisterModel registerModel = new RegisterModel();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.Read())
                    {
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(this.Configuration["RedisPort"]);
                        IDatabase database = connectionMultiplexer.GetDatabase();
                        database.StringSet(key: "UserName", sqlData.GetString("UserName"));
                        database.StringSet(key: "PhoneNo", sqlData.GetString("PhoneNo"));
                        database.StringSet(key: "UserId", sqlData.GetInt32("UserId").ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public async Task<ResetPasswordModel> ResetPassword(ResetPasswordModel resetPassword)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("spUserReset", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    var password = this.EncryptPassword(resetPassword.NewPassword);
                    sqlCommand.Parameters.AddWithValue("@Email", resetPassword.Email);
                    sqlCommand.Parameters.AddWithValue("@UserId", resetPassword.UserId);
                    sqlCommand.Parameters.AddWithValue("@NewPassword", resetPassword.NewPassword);

                    await sqlCommand.ExecuteNonQueryAsync();

                    return resetPassword;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        public async Task<bool> ForgotPassword(string Email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUserForgot", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@Email", Email);
                    sqlCommand.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;

                    await sqlCommand.ExecuteNonQueryAsync();
                    var result = sqlCommand.Parameters["@user"].Value;

                    if (!(result is DBNull))
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mail.From = new MailAddress(this.Configuration["Credentials:Email"]);
                        mail.To.Add(Email);
                        mail.Subject = "To Test Out Mail";
                        SendMSMQ();
                        mail.Body = RecieveMSMQ();

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:Email"], this.Configuration["Credentials:Password"]);
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                    }
                    return true;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\Bookstore"))
            {
                msgqueue = new MessageQueue(@".\Private$\Bookstore");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\Bookstore");
            }

            msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "Go to this link for reseting your password.(Password reset link http://localhost:4200/reset)";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }

        public string RecieveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Bookstore");
            var recievemsg = msgqueue.Receive();
            recievemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return recievemsg.Body.ToString();
        }

    }
}
