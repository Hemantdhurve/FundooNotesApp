using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration iconfiguration;
       
        public UserRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration=iconfiguration;
        }

        public static string Key = "hheemmaanntt123987";

        public static string ConvertToEncrypt(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return "";
                }
                password += Key;

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordBytes);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static string ConvertToDecrypt(string base64EncodedData)
        {
            try
            {
                if (string.IsNullOrEmpty(base64EncodedData))
                {
                    return "";
                }
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

                var result = Encoding.UTF8.GetString(base64EncodedBytes);
                result = result.Substring(0, result.Length - Key.Length);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            //It should have try catch block to catch any exception occured while execution
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = UserRL.ConvertToEncrypt(userRegistrationModel.Password);

                fundooContext.Usertable.Add(userEntity);    //used to create dependencies

                int result = fundooContext.SaveChanges();

                if(result!=0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //For login
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                //query to check only for email and password
                var resultLog = fundooContext.Usertable.Where(x => x.Email == userLoginModel.Email && x.Password==ConvertToEncrypt(userLoginModel.Password)).FirstOrDefault(); ;
                var decryptPass = ConvertToDecrypt(resultLog.Password);

                if (resultLog != null && decryptPass==userLoginModel.Password)                       
                {
                    //taken userLoginModel to get the stored data used for login

                    //userLoginModel.Email = resultLog.Email;
                    //userLoginModel.Password = (resultLog.Password);
                    var token = GenerateSecurityToken(resultLog.Email,resultLog.UserId);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string GenerateSecurityToken(string email,long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = fundooContext.Usertable.FirstOrDefault(x => x.Email == email);
                if (emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.Email,emailcheck.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string ResetPassword(string email,string newPassword,string confirmPassword)
        {
            try
            {
                if (newPassword.Equals(confirmPassword))
                {
                    var user = fundooContext.Usertable.FirstOrDefault(x => x.Email == email);
                    user.Password = ConvertToEncrypt( newPassword);
                    fundooContext.SaveChanges();
                    return user.Password;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
