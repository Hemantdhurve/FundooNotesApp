using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        //Data taken from UserEntity class
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);

       
    }
}
