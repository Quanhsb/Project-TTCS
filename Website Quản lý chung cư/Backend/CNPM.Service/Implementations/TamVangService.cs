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
using CNPM.Core.Models.TamVang;
namespace CNPM.Service.Implementations
{
    public class TamVangService : ITamVangService
    {
        private readonly ITamVangRepository _tamVangRepository;
        private readonly INhanKhauRepository _nhanKhauRepository;
        private readonly IMapper _mapper;
        public TamVangService(ITamVangRepository tamVangRepository, INhanKhauRepository nhanKhauRepository)
        {
            _tamVangRepository = tamVangRepository;
            _nhanKhauRepository = nhanKhauRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }
        public IActionResult GetListTamVang(int index, int limit)
        {
            try
            {
                var listTamVang = _tamVangRepository.GetListTamVang(index, limit);
                var arr = _mapper.Map<List<TamVangEntity>, List<TamVangDto1003>>(listTamVang);
                foreach (var item in arr)
                {
                    var nhanKhau = _nhanKhauRepository.GetNhanKhau(item.MaNhanKhau);
                    item.HoTen = nhanKhau.HoTen;
                    item.CanCuocCongDan = nhanKhau.CanCuocCongDan;
                }
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_TAM_VANG_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetTamVang(int maTamVang)
        {
            try
            {
                TamVangEntity tamVang = _tamVangRepository.GetTamVang(maTamVang);
                if (tamVang == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_TAM_VANG_FAILED,
                           reason = Constant.MA_TAM_VANG_NOT_EXIST
                       }
                    );
                var tamVang1001 = _mapper.Map<TamVangEntity, TamVangDto1001>(tamVang);
                var nhanKhau = _nhanKhauRepository.GetNhanKhau(tamVang.MaNhanKhau);
                tamVang1001.HoTen = nhanKhau.HoTen;
                tamVang1001.CanCuocCongDan = nhanKhau.CanCuocCongDan;
                return new OkObjectResult(new
                {
                    message = Constant.GET_TAM_VANG_SUCCESSFULLY,
                    data = tamVang1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateTamVang(string token, TamVangDto1000 tamVang1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                NhanKhauEntity nhanKhau = _nhanKhauRepository.GetNhanKhau(tamVang1000.MaNhanKhau);
                if (nhanKhau == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_TAM_VANG_FAILED,
                        reason = Constant.MA_NHAN_KHAU_NOT_EXIST
                    });
                }
                else if (nhanKhau.TrangThai == Constant.DIE)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_TAM_VANG_FAILED,
                        reason = Constant.NHAN_KHAU_IS_DIED
                    });
                }
                bool CCCD = _tamVangRepository.CheckExistCongDanDaDangKiTamVang(tamVang1000.MaNhanKhau);
                if (!CCCD)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_TAM_VANG_FAILED,
                        reason = Constant.REASON_NHAN_KHAU_TAM_VANG_EXISTED
                    });
                }
                TamVangEntity tamVang = _mapper.Map<TamVangDto1000, TamVangEntity>(tamVang1000);
                tamVang.CreateTime = DateTime.Now;
                tamVang.UpdateTime = DateTime.Now;
                tamVang.UserCreate = userName;
                tamVang.UserUpdate = userName;
                int maTamVang = _tamVangRepository.CreateTamVang(tamVang);
                if (maTamVang != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_TAM_VANG_SUCCESSFULLY,
                        data = new { maTamVang = maTamVang }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_TAM_VANG_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateTamVang(string token, int maTamVang, TamVangDto1002 newTamVang)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var tamVang = _tamVangRepository.GetTamVang(maTamVang);
                if (tamVang == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_TAM_VANG_FAILED,
                    reason = Constant.MA_TAM_VANG_NOT_EXIST
                });
                NhanKhauEntity nhanKhau = _nhanKhauRepository.GetNhanKhau(newTamVang.MaNhanKhau);
                if (nhanKhau == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_VANG_FAILED,
                        reason = Constant.MA_NHAN_KHAU_NOT_EXIST
                    });
                }
                else if (nhanKhau.TrangThai == Constant.DIE)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_VANG_FAILED,
                        reason = Constant.NHAN_KHAU_IS_DIED
                    });
                }
                bool kt = _tamVangRepository.CheckExistCongDanDaDangKiTamVangUpdate(maTamVang, newTamVang.MaNhanKhau);
                if (!kt)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_VANG_FAILED,
                        reason = Constant.REASON_NHAN_KHAU_TAM_VANG_EXISTED
                    });
                }
                if (tamVang.Version != newTamVang.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_TAM_VANG_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                TamVangEntity tamVangEntity = _mapper.Map<TamVangDto1002, TamVangEntity>(newTamVang);
                tamVangEntity.MaTamVang = maTamVang;
                tamVangEntity.UserUpdate = userName;
                tamVangEntity.UpdateTime = DateTime.Now;
                tamVangEntity.Version += 1;
                int maTamVangUpdate = _tamVangRepository.UpdateTamVang(tamVangEntity);
                if (maTamVangUpdate != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_TAM_VANG_SUCCESSFULLY,
                        data = new { maTamVang = maTamVangUpdate }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_TAM_VANG_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteTamVang(int maTamVang, string token, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var tamVang = _tamVangRepository.GetTamVang(maTamVang);
                if (tamVang == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_TAM_VANG_FAILED,
                    reason = Constant.MA_TAM_VANG_NOT_EXIST
                });
                if (tamVang.Version != version) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_TAM_VANG_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                bool delete = _tamVangRepository.DeleteTamVang(maTamVang, userName);
                if (delete)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.DELETE_TAM_VANG_SUCCESSFULLY,
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_TAM_VANG_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}