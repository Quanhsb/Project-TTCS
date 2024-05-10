﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CNPM.Service.Implementations;
using CNPM.Service.Interfaces;
using CNPM.Core.Utils;
namespace CNPM.Controllers.Authorize
{
    public class VerifyToken : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _userservice = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                try
                {
                    var token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value.ToString().Replace("Bearer ", string.Empty);
                    ObjectResult res = (ObjectResult)_userservice.VerifyToken(token);
                    if (res.StatusCode == 400) context.Result = new BadRequestObjectResult(new
                    {
                        message = Constant.INVALID_TOKEN
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}