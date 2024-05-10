﻿﻿using CNPM.Core.Models;
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
using CNPM.Core.Models.HoKhau;
using CNPM.Core.Models.Xe;
using CNPM.Core.Models.CanHo;
namespace CNPM.Service.Implementations
{
    public class HoKhauService : IHoKhauService
    {
        private readonly IHoKhauRepository _hoKhauRepository;
        private readonly INhanKhauRepository _nhanKhauRepository;
        private readonly IXeRepository _xeRepository;
        private readonly ICanHoRepository _canHoRepository;
        private readonly IMapper _mapper;
        public HoKhauService(IHoKhauRepository hoKhauRepository, INhanKhauRepository nhanKhauRepository, IXeRepository xeRepository, ICanHoRepository canHoRepository)
        {
            _hoKhauRepository = hoKhauRepository;
            _nhanKhauRepository = nhanKhauRepository;
            _xeRepository = xeRepository;
            _canHoRepository = canHoRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }
        public IActionResult GetListHoKhau(int index, int limit)
        {
            try
            {
                var listHoKhau = _hoKhauRepository.GetListHoKhau(index, limit);
                var arr = _mapper.Map<List<HoKhauEntity>, List<HoKhauDto1003>>(listHoKhau);
                foreach (HoKhauDto1003 hoKhau in arr)
                {
                    var listNhanKhauEntity = _nhanKhauRepository.GetListNhanKhauInHoKhau(hoKhau.MaHoKhau!);
                    hoKhau.SoThanhVien = listNhanKhauEntity.FindAll(o => o.TrangThai == Constant.ALIVE).ToList().Count();
                }
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_HO_KHAU_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetHoKhau(string maHoKhau)
        {
            try
            {
                HoKhauEntity hoKhau = _hoKhauRepository.GetHoKhau(maHoKhau);
                if (hoKhau == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_HO_KHAU_FAILED,
                           reason = Constant.MA_HO_KHAU_NOT_EXIST
                       }
                    );
                var hoKhau1001 = _mapper.Map<HoKhauEntity, HoKhauDto1001>(hoKhau);
                var listNhanKhauEntity = _nhanKhauRepository.GetListNhanKhauInHoKhau(maHoKhau);
                var listNhanKhauDto = _mapper.Map<List<NhanKhauEntity>, List<NhanKhauDto1001>>(listNhanKhauEntity);
                var phongEntity = _canHoRepository.GetCanHoByHoKhau(maHoKhau);
                var phong1001 = _mapper.Map<CanHoEntity, CanHoDto1001>(phongEntity);
                var listXeEntity = _xeRepository.GetListXeByHoKhau(maHoKhau);
                var listXe1001 = _mapper.Map<List<XeEntity>, List<XeDto1001>>(listXeEntity);
                if (phong1001 != null)
                {
                    hoKhau1001.MaCanHo = phong1001.MaCanHo;
                }
                hoKhau1001.DanhSachXe = listXe1001;
                hoKhau1001.DanhSachNhanKhau = listNhanKhauDto;
                hoKhau1001.SoThanhVien = listNhanKhauDto.FindAll(o => o.TrangThai == Constant.ALIVE).ToList().Count();
                return new OkObjectResult(new
                {
                    message = Constant.GET_HO_KHAU_SUCCESSFULLY,
                    data = hoKhau1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateHoKhau(string token, HoKhauDto1000 hoKhau1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                if (!(hoKhau1000.MaHoKhau == null || hoKhau1000.MaHoKhau == ""))
                {
                    bool kt = _hoKhauRepository.CheckMaHoKhauExisted(hoKhau1000.MaHoKhau);
                    if (!kt)
                    {
                        return new BadRequestObjectResult(new
                        {
                            message = Constant.CREATE_HO_KHAU_FAILED,
                            reason = Constant.REASON_MA_HO_KHAU_EXISTED
                        });
                    }
                }
                HoKhauEntity hoKhau = _mapper.Map<HoKhauDto1000, HoKhauEntity>(hoKhau1000);
                hoKhau.CreateTime = DateTime.Now;
                hoKhau.UpdateTime = DateTime.Now;
                hoKhau.UserCreate = userName;
                hoKhau.UserUpdate = userName;
                string maHoKhau = _hoKhauRepository.CreateHoKhau(hoKhau);
                if (maHoKhau != "")
                {
                    bool addNKToHK = _hoKhauRepository.AddNhanKhauToHoKhau(hoKhau1000.DanhSachNhanKhau!, maHoKhau, userName);
                    if (addNKToHK) return new OkObjectResult(new
                    {
                        message = Constant.CREATE_HO_KHAU_SUCCESSFULLY,
                        data = new { maHoKhau = maHoKhau }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_HO_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateHoKhau(string token, string maHoKhau, HoKhauDto1002 newHoKhau)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var hoKhau = _hoKhauRepository.GetHoKhau(maHoKhau);
                if (hoKhau == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_HO_KHAU_FAILED,
                    reason = Constant.MA_HO_KHAU_NOT_EXIST
                });
                if (hoKhau.Version != newHoKhau.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_HO_KHAU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                HoKhauEntity hoKhauEntity = _mapper.Map<HoKhauDto1002, HoKhauEntity>(newHoKhau);
                hoKhauEntity.MaHoKhau = maHoKhau;
                hoKhauEntity.UserUpdate = userName;
                hoKhauEntity.UpdateTime = DateTime.Now;
                hoKhauEntity.Version += 1;
                string maHoKhauUpdate = _hoKhauRepository.UpdateHoKhau(hoKhauEntity);
                if (maHoKhauUpdate != "")
                {
                    bool remove = _hoKhauRepository.RemoveNhanKhauFromHoKhau(maHoKhau, userName);
                    if (remove)
                    {
                        bool updateNK = _hoKhauRepository.AddNhanKhauToHoKhau(newHoKhau.DanhSachNhanKhau!, maHoKhauUpdate, userName);
                        if (updateNK) return new OkObjectResult(new
                        {
                            message = Constant.UPDATE_HO_KHAU_SUCCESSFULLY,
                            data = new { maHoKhau = maHoKhauUpdate }
                        });
                    }
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_HO_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult AddCanHoToHoKhau(string token, string maHoKhau, int maCanHo)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var hoKhau = _hoKhauRepository.GetHoKhau(maHoKhau);
                if (hoKhau == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_HO_KHAU_FAILED,
                    reason = Constant.MA_HO_KHAU_NOT_EXIST
                });
                bool add = _hoKhauRepository.AddCanHoToHoKhau(maHoKhau, maCanHo, userName);
                if (add)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_HO_KHAU_SUCCESSFULLY,
                        data = new { maHoKhau = maHoKhau }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_HO_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult AddXeToHoKhau(string token, XeDto1000 xe)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var hoKhau = _hoKhauRepository.GetHoKhau(xe.MaHoKhau!);
                if (hoKhau == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_HO_KHAU_FAILED,
                    reason = Constant.MA_HO_KHAU_NOT_EXIST
                });
                XeEntity xeExist = _xeRepository.GetXeByBienKiemSoat(xe.BienKiemSoat!);
                if (xeExist != null)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.ADD_XE_FAILED,
                        reason = Constant.BIEN_SO_XE_EXISTED
                    });
                }
                XeEntity xeEntity = _mapper.Map<XeDto1000, XeEntity>(xe);
                xeEntity.UserCreate = userName;
                xeEntity.UserUpdate = userName;
                xeEntity.CreateTime = DateTime.Now;
                xeEntity.UpdateTime = DateTime.Now;
                int maXe = _xeRepository.CreateXe(xeEntity);
                if (maXe != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.ADD_XE_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.ADD_XE_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateXe(string token, int maXe, XeDto1002 newXe)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                XeEntity xe = _xeRepository.GetXe(maXe);
                if (xe == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_XE_FAILED,
                        reason = Constant.XE_IS_NOT_EXIST
                    });
                }
                XeEntity newXeByBienSoXe = _xeRepository.GetXeByBienKiemSoat(newXe.BienKiemSoat!);
                if (newXeByBienSoXe != null && newXeByBienSoXe.MaXe != maXe)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.UPDATE_XE_FAILED,
                        reason = Constant.BIEN_SO_XE_EXISTED
                    });
                }
                XeEntity xeEntity = _mapper.Map<XeDto1002, XeEntity>(newXe);
                xeEntity.MaXe = maXe;
                xeEntity.UserCreate = userName;
                xeEntity.UserUpdate = userName;
                xeEntity.CreateTime = DateTime.Now;
                xeEntity.UpdateTime = DateTime.Now;
                bool update = _xeRepository.UpdateXe(xeEntity);
                if (update)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_XE_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_XE_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult RemoveXeFromHoKhau(string token, int maXe)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                bool remove = _xeRepository.DeleteXe(maXe, userName);
                if (remove)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.REMOVE_XE_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.REMOVE_XE_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteHoKhau(string token, string maHoKhau, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var hoKhau = _hoKhauRepository.GetHoKhau(maHoKhau);
                if (hoKhau == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_HO_KHAU_FAILED,
                    reason = Constant.MA_HO_KHAU_NOT_EXIST
                });
                _hoKhauRepository.AddCanHoToHoKhau(maHoKhau, -1, userName);
                bool remove = _hoKhauRepository.RemoveNhanKhauFromHoKhau(maHoKhau, userName);
                if (remove)
                {
                    bool delete = _hoKhauRepository.DeleteHoKhau(maHoKhau, userName);
                    if (delete)
                    {
                        return new OkObjectResult(new
                        {
                            message = Constant.DELETE_HO_KHAU_SUCCESSFULLY,
                        });
                    }
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_HO_KHAU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}