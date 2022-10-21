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
        
       
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
            
        }

        public static UserEntity userEntity = new UserEntity();
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            //It should have try catch block to catch any exception occured while execution
            try
            {
                //UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;

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
                throw;
            }
        }

        //For login
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                //query to check only for email and password
                var resultLog = fundooContext.Usertable.Where(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password).FirstOrDefault(); ;

                if (resultLog != null)                      
                {
                    //taken userLoginModel to get the stored data used for login
                    //userLoginModel.Email = resultLog.Email;
                    //userLoginModel.Password = resultLog.Password;
                    var token = GenerateSecurityToken(resultLog.Email, resultLog.UserId);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception e)
            {
                throw;
            }
        }

       

    }
}
