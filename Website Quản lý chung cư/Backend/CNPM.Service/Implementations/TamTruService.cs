﻿using CNPM.Core.Models;
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
using CNPM.Core.Models.TamTru;
namespace CNPM.Service.Implementations
{
    public class TamTruService : ITamTruService
    {
        private readonly ITamTruRepository _tamTruRepository;
        private readonly IMapper _mapper;
        public TamTruService(ITamTruRepository tamTruRepository)
        {
            _tamTruRepository = tamTruRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }
        public IActionResult GetListTamTru(int index, int limit)
        {
            try
            {
                var listTamTru = _tamTruRepository.GetListTamTru(index, limit);
                var arr = _mapper.Map<List<TamTruEntity>, List<TamTruDto1003>>(listTamTru);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_TAM_TRU_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetTamTru(int maTamTru)
        {
            try
            {
                TamTruEntity tamTru = _tamTruRepository.GetTamTru(maTamTru);
                if (tamTru == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_TAM_TRU_FAILED,
                           reason = Constant.MA_TAM_TRU_NOT_EXIST
                       }
                    );
                var tamTru1001 = _mapper.Map<TamTruEntity, TamTruDto1001>(tamTru);
                return new OkObjectResult(new
                {
                    message = Constant.GET_TAM_TRU_SUCCESSFULLY,
                    data = tamTru1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateTamTru(string token, TamTruDto1000 tamTru1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                bool CCCD = _tamTruRepository.CheckExistCanCuocCongDan(tamTru1000.CanCuocCongDan!);
                if (!CCCD)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_TAM_TRU_FAILED,
                        reason = Constant.REASON_CCCD_TAM_TRU_EXISTED
                    });
                }
                CCCD = _tamTruRepository.CheckExistCanCuocCongDanTrongNhanKhau(tamTru1000.CanCuocCongDan!);
                if (!CCCD)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_TAM_TRU_FAILED,
                        reason = Constant.REASON_CCCD_TAM_TRU_EXISTED_IN_NHAN_KHAU
                    });
                }
                TamTruEntity tamTru = _mapper.Map<TamTruDto1000, TamTruEntity>(tamTru1000);
                tamTru.CreateTime = DateTime.Now;
                tamTru.UpdateTime = DateTime.Now;
                tamTru.UserCreate = userName;
                tamTru.UserUpdate = userName;
                int maTamTru = _tamTruRepository.CreateTamTru(tamTru);
                if (maTamTru != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_TAM_TRU_SUCCESSFULLY,
                        data = new { maTamTru = maTamTru }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_TAM_TRU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateTamTru(string token, int maTamTru, TamTruDto1002 newTamTru)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var tamTru = _tamTruRepository.GetTamTru(maTamTru);
                if (tamTru == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_TAM_TRU_FAILED,
                    reason = Constant.MA_TAM_TRU_NOT_EXIST
                });
                if (!_tamTruRepository.CheckExistCanCuocCongDanUpdate(newTamTru.CanCuocCongDan!, maTamTru))
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_TRU_FAILED,
                        reason = Constant.REASON_CCCD_TAM_TRU_EXISTED
                    });
                }
                if (!_tamTruRepository.CheckExistCanCuocCongDanTrongNhanKhau(newTamTru.CanCuocCongDan!))
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_TRU_FAILED,
                        reason = Constant.REASON_CCCD_TAM_TRU_EXISTED_IN_NHAN_KHAU
                    });
                }
                if (tamTru.Version != newTamTru.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_TAM_TRU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                TamTruEntity tamTruEntity = _mapper.Map<TamTruDto1002, TamTruEntity>(newTamTru);
                tamTruEntity.MaTamTru = maTamTru;
                tamTruEntity.UserUpdate = userName;
                tamTruEntity.UpdateTime = DateTime.Now;
                tamTruEntity.Version += 1;
                int maTamTruUpdate = _tamTruRepository.UpdateTamTru(tamTruEntity);
                if (maTamTruUpdate != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_TRU_SUCCESSFULLY,
                        data = new { maTamTru = maTamTruUpdate }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_TAM_TRU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteTamTru(int maTamTru, string token, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var tamTru = _tamTruRepository.GetTamTru(maTamTru);
                if (tamTru == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_TAM_TRU_FAILED,
                    reason = Constant.MA_TAM_TRU_NOT_EXIST
                });
                if (tamTru.Version != version) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_TAM_TRU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                bool delete = _tamTruRepository.DeleteTamTru(maTamTru, userName);
                if (delete)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.DELETE_TAM_TRU_SUCCESSFULLY,
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_TAM_TRU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}