using CNPM.Core.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
namespace CNPM.Core.Utils
{
    public class Helpers
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static bool CheckEmptyOrNullUserData(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) return true;
            return false;
        }
        
        public static bool CheckEmptyOrNullUserData(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return true;
            return false;
        }
        public static bool CheckValidUserData(UserDto1005 userData)
        {
            if (string.IsNullOrEmpty(userData.UserName)
              || string.IsNullOrEmpty(userData.FirstName)
              || string.IsNullOrEmpty(userData.LastName)
               ) return true;
            return false;   
        }
        public static string GetHashPassword(string password)
        {
            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHashed;
        }
        public static bool IsValidPassWord(string oPassword, string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(oPassword, password);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static string DecodeJwt(string jwt, string type)
        {
            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var tokenS = jsonToken as JwtSecurityToken;
            var data = tokenS.Claims.First(claim => claim.Type == type).Value;
            return data;
        }
        public static object SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}