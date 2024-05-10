﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;
namespace CNPM.Repository.Interfaces
{
    public interface IUserRepository
    {
        public UserEntity GetUser(string userName);
        public UserEntity CreateUser(UserEntity userData);
        public bool DeleteUser(UserEntity userName);
        public bool UpdateUser(UserEntity newUserData);
        public List<UserEntity> GetAllUsers();
        public List<RoleEntity> GetListPermissions();
        public bool ChangePassWord(string userName, string newPassWord);
        public void SaveToken(string userName, string accessToken);
        public bool CheckToken(string userName, string accessToken);
        public bool DeleteToken(string userName, string accessToken);
    }
}