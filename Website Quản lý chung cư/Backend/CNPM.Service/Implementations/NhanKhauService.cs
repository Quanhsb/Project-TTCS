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
using CNPM.Core.Models.NhanKhau;
using CNPM.Repository.Implementations;
using System.Collections.Generic;
namespace CNPM.Service.Implementations
{
    public class NhanKhauService : INhanKhauService
    {
        private readonly INhanKhauRepository _nhanKhauRepository;
        private readonly IHoKhauRepository _hoKhauRepository;
        private readonly ITamVangRepository _tamVangRepository;
        private readonly IMapper _mapper;
        public NhanKhauService(INhanKhauRepository nhanKhauRepository,
            IHoKhauRepository hoKhauRepository,
            ITamVangRepository tamVangRepository)
        {
            _nhanKhauRepository = nhanKhauRepository;
            _hoKhauRepository = hoKhauRepository;
            _tamVangRepository = tamVangRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

        }
        public IActionResult GetListNhanKhau(int index, int limit)
        {
            try
            {
                var listNhanKhau = _nhanKhauRepository.GetListNhanKhau(index, limit);
                var arr = _mapper.Map<List<NhanKhauEntity>, List<NhanKhauDto1003>>(listNhanKhau);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_NHAN_KHAU_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetListNhanKhauAlive(int index, int limit)
        {
            try
            {
                var listNhanKhau = _nhanKhauRepository.GetListNhanKhauAlive(index, limit);
                var arr = _mapper.Map<List<NhanKhauEntity>, List<NhanKhauDto1003>>(listNhanKhau);
                List<NhanKhauDto1003> dataExpected = new List<NhanKhauDto1003>();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (_tamVangRepository.CheckExistCongDanDaDangKiTamVang(arr[i].MaNhanKhau))
                    {
                        dataExpected.Add(arr[i]);
                    }
                }
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_NHAN_KHAU_ALIVE_SUCCESSFULLY,
                        data = dataExpected
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetListNhanKhauNotHaveHoKhau(int index, int limit)
        {
            try
            {
                var listNhanKhau = _nhanKhauRepository.GetListNhanKhauNotHaveHoKhau(index, limit);
                var arr = _mapper.Map<List<NhanKhauEntity>, List<NhanKhauDto1003>>(listNhanKhau);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_NHAN_KHAU_NOT_HAVE_HO_KHAU_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetListNhanKhauInHoKhau(string maHoKhau)
        {
            try
            {
                var hoKhau = _hoKhauRepository.GetHoKhau(maHoKhau);
                if (hoKhau == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_LIST_NHAN_KHAU_FAILED,
                           reason = Constant.MA_HO_KHAU_NOT_EXIST
                       }
                    );
                var listNhanKhau = _nhanKhauRepository.GetListNhanKhauInHoKhau(maHoKhau);
                var arr = _mapper.Map<List<NhanKhauEntity>, List<NhanKhauDto1003>>(listNhanKhau);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_NHAN_KHAU_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetNhanKhau(int maNhanKhau)
        {
            try
            {
                NhanKhauEntity nhanKhau = _nhanKhauRepository.GetNhanKhau(maNhanKhau);
                if (nhanKhau == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_NHAN_KHAU_FAILED,
                           reason = Constant.MA_NHAN_KHAU_NOT_EXIST
                       }
                    );
                var nhanKhau1001 = _mapper.Map<NhanKhauEntity, NhanKhauDto1001>(nhanKhau);
                return new OkObjectResult(new
                {
                    message = Constant.GET_NHAN_KHAU_SUCCESSFULLY,
                    data = nhanKhau1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateNhanKhau(string token, NhanKhauDto1000 nhanKhau1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                bool CCCD = _nhanKhauRepository.CheckExistCanCuocCongDan(nhanKhau1000.CanCuocCongDan!);
                if (!CCCD)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_NHAN_KHAU_FAILED,
                        reason = Constant.REASON_CCCD_EXISTED
                    });
                }
                NhanKhauEntity nhanKhau = _mapper.Map<NhanKhauDto1000, NhanKhauEntity>(nhanKhau1000);
                nhanKhau.CreateTime = DateTime.Now;
                nhanKhau.UpdateTime = DateTime.Now;
                nhanKhau.UserCreate = userName;
                nhanKhau.UserUpdate = userName;
                int maNhanKhau = _nhanKhauRepository.CreateNhanKhau(nhanKhau);
                if (maNhanKhau != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_NHAN_KHAU_SUCCESSFULLY,
                        data = new { maNhanKhau = maNhanKhau }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_NHAN_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateNhanKhau(string token, int maNhanKhau, NhanKhauDto1002 newNhanKhau)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var nhanKhau = _nhanKhauRepository.GetNhanKhau(maNhanKhau);
                if (nhanKhau == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_NHAN_KHAU_FAILED,
                    reason = Constant.MA_NHAN_KHAU_NOT_EXIST
                });
                if (nhanKhau.Version != newNhanKhau.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_NHAN_KHAU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                bool CCCD = _nhanKhauRepository.CheckExistCanCuocCongDanUpdate(nhanKhau.MaNhanKhau, newNhanKhau.CanCuocCongDan!);
                if (!CCCD)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.CREATE_NHAN_KHAU_FAILED,
                        reason = Constant.REASON_CCCD_EXISTED
                    });
                }
                NhanKhauEntity nhanKhauEntity = _mapper.Map<NhanKhauDto1002, NhanKhauEntity>(newNhanKhau);
                nhanKhauEntity.MaNhanKhau = maNhanKhau;
                nhanKhauEntity.UserUpdate = userName;
                nhanKhauEntity.UpdateTime = DateTime.Now;
                nhanKhauEntity.Version += 1;
                int maNhanKhauUpdate = _nhanKhauRepository.UpdateNhanKhau(nhanKhauEntity);
                if (maNhanKhauUpdate != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_NHAN_KHAU_SUCCESSFULLY,
                        data = new { maNhanKhau = maNhanKhauUpdate }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_NHAN_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteNhanKhau(int maNhanKhau, string token, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var nhanKhau = _nhanKhauRepository.GetNhanKhau(maNhanKhau);
                if (nhanKhau == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_NHAN_KHAU_FAILED,
                    reason = Constant.MA_NHAN_KHAU_NOT_EXIST
                });
                if (nhanKhau.Version != version) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_NHAN_KHAU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                bool delete = _nhanKhauRepository.DeleteNhanKhau(maNhanKhau, userName);
                if (delete)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.DELETE_NHAN_KHAU_SUCCESSFULLY,
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_NHAN_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}