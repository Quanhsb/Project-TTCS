using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Repository.Interfaces;
using CNPM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CNPM.Core.Utils;
using CNPM.Core.Models;
namespace CNPM.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        public UserEntity GetUser(string userName)
        {
            try
            {
                using var dbcontext = new MyDbContext();
                var user = new UserEntity();
                user = dbcontext.Users!.FirstOrDefault(o => o.UserName == userName && o.Delete == Constant.NOT_DELETE);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserEntity CreateUser(UserEntity userData)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.Users.Add(userData);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows > 0) return userData;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteUser(UserEntity userData)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var user = _dbcontext.Users!.FirstOrDefault(o => o.UserName == userData.UserName && o.Delete == Constant.NOT_DELETE);
                user.Delete = Constant.DELETE;
                user.UserUpdate = userData.UserName;
                user.UpdateTime = DateTime.Now;
                user.Version++;
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateUser(UserEntity newUserData)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var user = _dbcontext.Users!.FirstOrDefault(o => o.UserName == newUserData.UserName && o.Delete == Constant.NOT_DELETE);
                if (user != null)
                {
                    user.UserUpdate = newUserData.UserUpdate;
                    user.UpdateTime = newUserData.UpdateTime;
                    user.FirstName = newUserData.FirstName;
                    user.LastName = newUserData.LastName;
                    user.RoleId = newUserData.RoleId;
                    user.Version = newUserData.Version;
                    user.Email = newUserData.Email;
                    _dbcontext.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<RoleEntity> GetListPermissions()
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<RoleEntity> arr = _dbcontext.Roles!.ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<UserEntity> GetAllUsers()
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<UserEntity> arr = _dbcontext.Users!.Where(o => o.Delete == Constant.NOT_DELETE).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ChangePassWord(string userName, string newPassword)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var user = _dbcontext.Users!.FirstOrDefault(o => o.UserName == userName && o.Delete == Constant.NOT_DELETE);
                if (user != null)
                {
                    user.Password = newPassword;
                    _dbcontext.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SaveToken(string userName, string accessToken)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var loginInfo = new LoginInfoEntity();
                loginInfo.AccessToken = accessToken;
                loginInfo.UserName = userName;
                loginInfo.UpdateTime = DateTime.Now;
                loginInfo.CreateTime = DateTime.Now;
                loginInfo.UserCreate = userName;
                loginInfo.UserUpdate = userName;
                loginInfo.Delete = Constant.NOT_DELETE;
                loginInfo.Version = 0;
                _dbcontext.LoginInfos.Add(loginInfo);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteToken(string userName, string accessToken)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var loginInfo = _dbcontext.LoginInfos!.FirstOrDefault(o => o.UserName == userName && o.Delete == Constant.NOT_DELETE && o.AccessToken == accessToken);
                if (loginInfo != null)
                {
                    loginInfo.Delete = Constant.DELETE;
                    loginInfo.UpdateTime = DateTime.Now;
                    loginInfo.Version++;
                    _dbcontext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckToken(string userName, string accessToken)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var loginInfo = _dbcontext.LoginInfos!.FirstOrDefault(o => o.UserName == userName && o.Delete == Constant.NOT_DELETE && o.AccessToken == accessToken);
                if (loginInfo != null)
                {
                    return loginInfo.Delete == Constant.NOT_DELETE;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
