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
using CNPM.Repository.Implementations;
using System.Collections.Generic;
using CNPM.Core.Models.CanHo;
namespace CNPM.Service.Implementations
{
    public class CanHoService : ICanHoService
    {
        private readonly ICanHoRepository _canHoRepository;
        private readonly IMapper _mapper;
        public CanHoService(ICanHoRepository canHoRepository)
        {
            _canHoRepository = canHoRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }
        public IActionResult GetListCanHo(int index, int limit)
        {
            try
            {
                var listCanHo = _canHoRepository.GetListCanHo(index, limit);
                var arr = _mapper.Map<List<CanHoEntity>, List<CanHoDto1003>>(listCanHo);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_CAN_HO_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetCanHo(int maCanHo)
        {
            try
            {
                CanHoEntity canHo = _canHoRepository.GetCanHo(maCanHo);
                if (canHo == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_CAN_HO_FAILED,
                           reason = Constant.MA_CAN_HO_NOT_EXIST
                       }
                    );
                var canHoDto1001 = _mapper.Map<CanHoEntity, CanHoDto1001>(canHo);
                return new OkObjectResult(new
                {
                    message = Constant.GET_TAM_VANG_SUCCESSFULLY,
                    data = canHoDto1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateCanHo(string token, CanHoDto1000 canHo1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                CanHoEntity canHo = _mapper.Map<CanHoDto1000, CanHoEntity>(canHo1000);
                canHo.CreateTime = DateTime.Now;
                canHo.UpdateTime = DateTime.Now;
                canHo.UserCreate = userName;
                canHo.UserUpdate = userName;
                if (canHo1000.MaHoKhau == "") canHo.MaHoKhau = null;
                bool created = _canHoRepository.CreateCanHo(canHo);
                if (created)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_CAN_HO_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_CAN_HO_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateCanHo(string token, int maCanHo, CanHoDto1002 newCanHo)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var canHo = _canHoRepository.GetCanHo(maCanHo);
                if (canHo == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_CAN_HO_FAILED,
                    reason = Constant.MA_CAN_HO_NOT_EXIST
                });
                if (canHo.Version != newCanHo.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_CAN_HO_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                CanHoEntity canHoEntity = _mapper.Map<CanHoDto1002, CanHoEntity>(newCanHo);
                if (newCanHo.MaHoKhau == "") canHoEntity.MaHoKhau = null;
                canHoEntity.MaCanHo = maCanHo;
                canHoEntity.UserUpdate = userName;
                canHoEntity.UpdateTime = DateTime.Now;
                canHoEntity.Version += 1;
                bool updated = _canHoRepository.UpdateCanHo(canHoEntity);
                if (updated)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_CAN_HO_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_CAN_HO_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteCanHo(int maCanHo, string token, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var canHo = _canHoRepository.GetCanHo(maCanHo);
                if (canHo == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_CAN_HO_FAILED,
                    reason = Constant.MA_CAN_HO_NOT_EXIST
                });
                if (canHo.Version != version) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_CAN_HO_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                bool delete = _canHoRepository.DeleteCanHo(maCanHo, userName);
                if (delete)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.DELETE_CAN_HO_SUCCESSFULLY,
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_CAN_HO_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}