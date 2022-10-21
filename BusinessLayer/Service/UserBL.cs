using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.Login(userLoginModel);
            }
            catch(Exception ex)
            {
                
                throw;
            }
        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            //just calling dependencies here
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
