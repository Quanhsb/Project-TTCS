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
using CNPM.Core.Models.KhoanThu;
using CNPM.Repository.Implementations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CNPM.Core.Models.HoaDon;
using System;
namespace CNPM.Service.Implementations
{
    public class KhoanThuService : IKhoanThuService
    {
        private readonly IKhoanThuRepository _khoanThuRepository;
        private readonly IHoKhauRepository _hoKhauRepository;
        private readonly ITamVangRepository _tamVangRepository;
        private readonly IMapper _mapper;
        public KhoanThuService(IKhoanThuRepository khoanThuRepository, 
            IHoKhauRepository hoKhauRepository,
            ITamVangRepository tamVangRepository)
        {
            _khoanThuRepository = khoanThuRepository;
            _hoKhauRepository = hoKhauRepository;
            _tamVangRepository = tamVangRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
            
        }
        public IActionResult GetListKhoanThu(int index, int limit)
        {
            try
            {
                var listKhoanThu = _khoanThuRepository.GetListKhoanThu(index, limit);
                var arr = _mapper.Map<List<KhoanThuEntity>, List<KhoanThuDto1003>>(listKhoanThu);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_KHOAN_THU_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetKhoanThu(int maKhoanThu)
        {
            try { 
                KhoanThuEntity khoanThu = _khoanThuRepository.GetKhoanThu(maKhoanThu);
                if (khoanThu == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_KHOAN_THU_FAILED,
                           reason = Constant.MA_KHOAN_THU_NOT_EXIST
                       }
                    );
                var khoanThu1001 = _mapper.Map<KhoanThuEntity, KhoanThuDto1001>(khoanThu);
                return new OkObjectResult(new {
                    message = Constant.GET_KHOAN_THU_SUCCESSFULLY,
                    data = khoanThu1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateKhoanThu(string token, KhoanThuDto1000 khoanThu1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                // check ho khau
                KhoanThuEntity khoanThu = _mapper.Map<KhoanThuDto1000, KhoanThuEntity>(khoanThu1000);
                khoanThu.CreateTime = DateTime.Now;
                khoanThu.UpdateTime = DateTime.Now;
                khoanThu.UserCreate = userName;
                khoanThu.UserUpdate = userName;
                int maKhoanThu = _khoanThuRepository.CreateKhoanThu(khoanThu);
                if (maKhoanThu != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_KHOAN_THU_SUCCESSFULLY,
                        data = new { maKhoanThu = maKhoanThu }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_KHOAN_THU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetKhoanThuTheoHo(int maKhoanThu)
        {
            try
            {
                var khoanThu = _khoanThuRepository.GetKhoanThu(maKhoanThu);
                if (khoanThu == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        message = Constant.GET_KHOAN_THU_THEO_HO_FAILED,
                        reason = Constant.MA_KHOAN_THU_NOT_EXIST
                    });
                }
                var khoanThuTheoHo = _khoanThuRepository.GetKhoanThuTheoHo(maKhoanThu);
                var arr = _mapper.Map<List<KhoanThuTheoHoEntity>, List<KhoanThuDto1004>>(khoanThuTheoHo);
                var tongCanThu = 0;
                var tongDaThu = 0;
                foreach (var item in arr)
                {
                    var hoaDon = _khoanThuRepository.GetHoaDonKhoanThuTheoHo(item.MaKhoanThuTheoHo);
                    var soTienDaNop = 0;
                    foreach (var iHoaDon in hoaDon)
                    {
                        soTienDaNop += iHoaDon.SoTienDaNop;
                    }
                    item.SoTienDaNop = soTienDaNop;
                    tongDaThu += soTienDaNop;
                    tongCanThu += item.SoTien;
                }
                return new OkObjectResult(new
                {
                    message = Constant.GET_KHOAN_THU_THEO_HO_SUCCESSFULLY,
                    data = new
                    {
                        maKhoanThu = khoanThu.MaKhoanThu,
                        tenKhoanThu = khoanThu.TenKhoanThu,
                        thoiGianBatDau = khoanThu.ThoiGianBatDau,
                        thoiGianKetThuc = khoanThu.ThoiGianKetThuc,
                        loaiKhoanThu = khoanThu.LoaiKhoanThu,
                        ghiChu = khoanThu.GhiChu,
                        tongCanThu = tongCanThu,
                        tongDaThu = tongDaThu,
                        version = khoanThu.Version,
                        data = arr
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateKhoanThuTheoHo(string token, int maKhoanThu)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                // check ho khau
                KhoanThuEntity khoanThu = _khoanThuRepository.GetKhoanThu(maKhoanThu);
                if (khoanThu != null)
                {
                    _khoanThuRepository.CreateKhoanThuTheoHo(maKhoanThu, userName);
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_KHOAN_THU_THEO_HO_SUCCESSFULLY,
                        data = new { maKhoanThu = maKhoanThu }
                    });
                }
                
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_KHOAN_THU_THEO_HO_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult GetCacKhoanThuDaNopCuaHo(string maHoKhau)
        {
            try
            {
                List<KhoanThuTheoHoEntity> listKhoanThuTheoHo = _khoanThuRepository.GetCacKhoanThuCuaHo(maHoKhau);
                List<KhoanThuDto1005> arr = new List<KhoanThuDto1005>();
                foreach (var khoanThuTheoHo in listKhoanThuTheoHo)
                {
                    KhoanThuEntity khoanThuEntity = _khoanThuRepository.GetKhoanThu(khoanThuTheoHo.MaKhoanThu);
                    KhoanThuDto1005 khoanThu = new KhoanThuDto1005();
                    khoanThu.MaHoKhau = maHoKhau;
                    khoanThu.MaKhoanThu = khoanThuEntity.MaKhoanThu;
                    khoanThu.TenKhoanThu = khoanThuEntity.TenKhoanThu;
                    khoanThu.SoTien = khoanThuTheoHo.SoTien;
                    khoanThu.MaKhoanThuTheoHo = khoanThuTheoHo.MaKhoanThuTheoHo;
                    khoanThu.LoaiKhoanThu = khoanThuEntity.LoaiKhoanThu;
                    var hoaDon = _khoanThuRepository.GetHoaDonKhoanThuTheoHo(khoanThuTheoHo.MaKhoanThuTheoHo);
                    var soTienDaNop = 0;
                    foreach (var iHoaDon in hoaDon)
                    {
                        soTienDaNop += iHoaDon.SoTienDaNop;
                    }
                    khoanThu.SoTienDaNop = soTienDaNop;
                    arr.Add(khoanThu);
                }
           
                return new OkObjectResult(new
                {
                    message = Constant.GET_KHOAN_THU_THEO_HO_SUCCESSFULLY,
                    data = arr
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdateKhoanThu(string token, int maKhoanThu, KhoanThuDto1002 newKhoanThu)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var khoanThu = _khoanThuRepository.GetKhoanThu(maKhoanThu);
                if (khoanThu == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_KHOAN_THU_FAILED,
                    reason = Constant.MA_KHOAN_THU_NOT_EXIST
                });
                if (newKhoanThu.Version != khoanThu.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_KHOAN_THU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                // check ho khau ton tai
                KhoanThuEntity khoanThuEntity = _mapper.Map<KhoanThuDto1002, KhoanThuEntity>(newKhoanThu);
                khoanThuEntity.MaKhoanThu = maKhoanThu;
                khoanThuEntity.UserUpdate = userName;
                khoanThuEntity.UpdateTime = DateTime.Now;
                khoanThuEntity.Version += 1;
                int maKhoanThuUpdate = _khoanThuRepository.UpdateKhoanThu(khoanThuEntity);
                if (maKhoanThuUpdate != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_KHOAN_THU_SUCCESSFULLY,
                        data = new { maKhoanThu = maKhoanThuUpdate }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_KHOAN_THU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteKhoanThu(int maKhoanThu, string token, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var khoanThu = _khoanThuRepository.GetKhoanThu(maKhoanThu);
                if (khoanThu == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_KHOAN_THU_FAILED,
                    reason = Constant.MA_KHOAN_THU_NOT_EXIST
                });
                if (khoanThu.Version != version) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_NHAN_KHAU_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });
                bool delete = _khoanThuRepository.DeleteKhoanThu(maKhoanThu, userName);
                if (delete)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.DELETE_KHOAN_THU_SUCCESSFULLY,
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_KHOAN_THU_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult ThanhToan(string token, HoaDonDto1000 hoaDon)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                HoaDonEntity hoaDonEntity = _mapper.Map<HoaDonDto1000, HoaDonEntity>(hoaDon);
                hoaDonEntity.CreateTime = DateTime.Now;
                hoaDonEntity.UpdateTime = DateTime.Now;
                hoaDonEntity.UserCreate = userName;
                hoaDonEntity.UserUpdate = userName;
                hoaDonEntity.Delete = Constant.NOT_DELETE;
                hoaDonEntity.Version = 0;
                var maHoaDon = _khoanThuRepository.ThanhToan(hoaDonEntity);
                if (maHoaDon != -1)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.THANH_TOAN_SUCCESSFULLY,
                        data = new
                        {
                            maHoaDon = maHoaDon
                        }
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.THANH_TOAN_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}