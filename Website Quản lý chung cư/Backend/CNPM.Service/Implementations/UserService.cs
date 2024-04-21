using CNPM.Core.Models;
using CNPM.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CNPM.Core.Entities;
using CNPM.Core.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using CNPM.Service.Interfaces;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
namespace CNPM.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _iconfiguration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IConfiguration iconfiguration)
        {
            _userRepository = userRepository;
            _iconfiguration = iconfiguration;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }
        public IActionResult VerifyToken(string token)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var roleId = Int32.Parse(Helpers.DecodeJwt(token, "role"));
                var userVersion = Int32.Parse(Helpers.DecodeJwt(token, "version"));
                var user = _userRepository.GetUser(userName);
                var checkToken = _userRepository.CheckToken(userName, token);
                if (user == null || user.RoleId != roleId || !checkToken)
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.INVALID_TOKEN
                    });
                UserDto1002 userDto1002 = _mapper.Map<UserEntity, UserDto1002>(user);
                userDto1002.Token = token;
                return new OkObjectResult(new
                {
                    message = Constant.VALID_TOKEN,
                    data = userDto1002
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                List<UserDto1001> arr = _mapper.Map<List<UserEntity>, List<UserDto1001>>(users);
                if (arr == null)
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.GET_LIST_USERS_FAILED
                    });
                return new OkObjectResult(new
                {
                    message = Constant.GET_LIST_USERS_SUCCESSFULLY,
                    data = arr
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Authenticate(UserDto1004 userLogin)
        {
            try
            {
                var user = _userRepository.GetUser(userLogin.UserName!);
                if (user == null) return new BadRequestObjectResult(new
                {
                    message = Constant.USERNAME_NOT_EXIST
                });
                bool isValidPassWord = Helpers.IsValidPassWord(userLogin.Password!, user.Password!);
                if (!isValidPassWord) return new BadRequestObjectResult(new
                {
                    message = Constant.INVALID_PASSWORD
                });
                var claims = new[]
                {
                new Claim("username", user.UserName!),
                new Claim("firstname", user.FirstName!),
                new Claim("lastname", user.LastName!),
                new Claim("version", user.Version.ToString()),
                new Claim("role", user.RoleId.ToString()),
            };
                var token = new JwtSecurityToken
                (
                    issuer: _iconfiguration["Jwt:Issuer"],
                    audience: _iconfiguration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha256)
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                UserDto1002 userDto1002 = _mapper.Map<UserEntity, UserDto1002>(user);
                userDto1002.Token = tokenString;
                userDto1002.RoleId = user.RoleId;
                _userRepository.SaveToken(userLogin.UserName!, tokenString);
                return new OkObjectResult(new
                {
                    message = Constant.LOGIN_SUCCESSFULLY,
                    data = userDto1002
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Logout(string accessToken)
        {
            try
            {
                var userName = Helpers.DecodeJwt(accessToken, "username");
                _userRepository.DeleteToken(userName, accessToken);
                return new OkObjectResult(new
                {
                    message = Constant.LOGOUT_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetUser(string userName)
        {
            UserEntity user = _userRepository.GetUser(userName);
            if (user == null) return new BadRequestObjectResult(
                   new
                   {
                       message = Constant.USERNAME_NOT_EXIST
                   }
                );
            var userDto1003 = _mapper.Map<UserEntity, UserDto1003>(user);
            return new OkObjectResult(new
            {
                message = Constant.GET_USER_SUCCESSFULLY,
                data = userDto1003
            });
        }
        public IActionResult CreateUser(UserDto1005 user)
        {
            try
            {
                var userTmp = _userRepository.GetUser(user.UserName!);
                if (userTmp != null) return null;
                UserEntity userEntity;
                UserDto1003 userDto1003;
                userEntity = _mapper.Map<UserDto1005, UserEntity>(user);
                userDto1003 = _mapper.Map<UserDto1005, UserDto1003>(user);
                string password = "123456";
                userEntity.Password = Helpers.GetHashPassword(password);
                userEntity.UserUpdate = user.UserCreate;
                userEntity.CreateTime = DateTime.Now;
                userEntity.UpdateTime = DateTime.Now;
                userEntity.Delete = Constant.NOT_DELETE;
                userEntity.Version = 0;
                var userResponse = _userRepository.CreateUser(userEntity);
                if (userResponse != null)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_USER_SUCCESSFULLY,
                        data = userDto1003
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_USER_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateUser(UserDto1006 newUserData)
        {
            try
            {
                var userTmp = _userRepository.GetUser(newUserData.UserName!);
                if (userTmp == null) return new BadRequestObjectResult(new
                {
                    message = Constant.USERNAME_NOT_EXIST
                });
                if (userTmp.Version != newUserData.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.DATA_UPDATED_BEFORE
                });
                UserEntity userEntity;
                userEntity = _mapper.Map<UserDto1006, UserEntity>(newUserData);
                userEntity.UserUpdate = newUserData.UserUpdate;
                userEntity.UpdateTime = DateTime.Now;
                userEntity.Version += 1;
                var kt = _userRepository.UpdateUser(userEntity);
                if (kt) return new OkObjectResult(new
                {
                    message = Constant.UPDATE_USER_SUCCESSFULLY
                });
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_USER_FAILED
                }); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteUser(UserDto1007 user)
        {
            try
            {
                var userTmp = _userRepository.GetUser(user.UserName!);
                if (userTmp == null) return new BadRequestObjectResult(new
                {
                    message = Constant.USERNAME_NOT_EXIST
                });
                if (userTmp.Version != user.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.DATA_UPDATED_BEFORE
                });
                UserEntity userEntity;
                userEntity = _mapper.Map<UserDto1007, UserEntity>(user);
                userEntity.UserUpdate = user.UserUpdate;
                userEntity.UpdateTime = DateTime.Now;
                userEntity.Version += 1;
                var kt = _userRepository.DeleteUser(userEntity);
                if (kt) return new OkObjectResult(
                    new
                    {
                        message = Constant.DELETE_USER_SUCCESSFULLY
                    });
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_USER_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetListPermissions()
        {
            try
            {
                List<RoleEntity> arrPermissionEntity = _userRepository.GetListPermissions();
                List<RoleDto> arrPermissionDto = _mapper.Map<List<RoleEntity>, List<RoleDto>>(arrPermissionEntity);
                if (arrPermissionDto == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.GET_LIST_PERMISSIONS_FAILED
                    });
                }
                return new OkObjectResult(new
                {
                    message = Constant.GET_LIST_PERMISSIONS_SUCCESSFULLY,
                    data = arrPermissionDto
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult ChangePassWord(UserDto1000 userData)
        {
            try
            {
                var user = _userRepository.GetUser(userData.UserName!);
                if (user == null) return new BadRequestObjectResult(new
                {
                    message = Constant.USERNAME_NOT_EXIST
                });
                bool isValidPassWord = Helpers.IsValidPassWord(userData.OldPassword!, user.Password!);
                if (!isValidPassWord) return new BadRequestObjectResult(new
                {
                    message = Constant.INVALID_PASSWORD
                });
                var newHashPassword = Helpers.GetHashPassword(userData.NewPassword!);
                if (Helpers.IsValidPassWord(userData.NewPassword!, user.Password!))
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CHANGE_PASSWORD_FAILED,
                        reason = Constant.NEWPASSWORD_SAME_OLDPASSWORD
                    }); ;
                }
                bool kt = _userRepository.ChangePassWord(user.UserName!, newHashPassword);
                if (kt)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CHANGE_PASSWORD_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CHANGE_PASSWORD_FAILED
                }); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}