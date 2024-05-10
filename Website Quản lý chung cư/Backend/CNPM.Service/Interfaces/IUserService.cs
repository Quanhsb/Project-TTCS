﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Models;
using Microsoft.AspNetCore.Mvc;
namespace CNPM.Service.Interfaces
{
    public interface IUserService
    {
        public IActionResult VerifyToken(string token);
        public IActionResult Authenticate(UserDto1004 userLogin);
        public IActionResult Logout(string accessToken);
        public IActionResult CreateUser(UserDto1005 user);
        public IActionResult UpdateUser(UserDto1006 newUserData);
        public IActionResult DeleteUser(UserDto1007 user);
        public IActionResult GetUser(string userName);
        public IActionResult ChangePassWord(UserDto1000 userData);
        public IActionResult GetListPermissions();
        public IActionResult GetAllUsers();
    }
}