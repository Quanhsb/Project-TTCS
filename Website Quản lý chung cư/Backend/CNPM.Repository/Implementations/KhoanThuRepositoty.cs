﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CNPM.Repository.Interfaces;
using CNPM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CNPM.Core.Utils;
using CNPM.Core.Models;
using CNPM.Core.Models.NhanKhau;
using System.ComponentModel.DataAnnotations;
using CNPM.Core.Models.KhoanThu;
using Newtonsoft.Json.Linq;
namespace CNPM.Repository.Implementations
{
    public class KhoanThuRepository : IKhoanThuRepository
    {
        public List<KhoanThuEntity> GetListKhoanThu(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<KhoanThuEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.KhoanThu.Where(
                    o => o.Delete == Constant.NOT_DELETE).ToList();
                }
                else arr = _dbcontext.KhoanThu.Where(
                    o => o.Delete == Constant.NOT_DELETE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public KhoanThuEntity GetKhoanThu(int maKhoanThu)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var khoanThu = _dbcontext.KhoanThu.Where(
                    o => o.MaKhoanThu == maKhoanThu && o.Delete == Constant.NOT_DELETE).FirstOrDefault();
                return khoanThu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateKhoanThu(KhoanThuEntity khoanThu)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.KhoanThu.Add(khoanThu);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return -1;
                var listHoKhau = _dbcontext.HoKhau.Where(o => o.Delete == Constant.NOT_DELETE).ToList();
                List<PhiSinhHoat> dsPhiSinhHoat = null;
                if (khoanThu.LoaiKhoanThu == 1) dsPhiSinhHoat = JObject.Parse(khoanThu.ChiTiet).ToObject<List<PhiSinhHoat>>();
                foreach (var hoKhau in listHoKhau)
                {
                    KhoanThuTheoHoEntity khoanThuTheoHo = new KhoanThuTheoHoEntity();
                    khoanThuTheoHo.MaHoKhau = hoKhau.MaHoKhau;
                    khoanThuTheoHo.MaKhoanThu = khoanThu.MaKhoanThu;
                    khoanThuTheoHo.UserCreate = khoanThu.UserCreate;
                    khoanThuTheoHo.UserUpdate = khoanThu.UserUpdate;
                    khoanThuTheoHo.CreateTime = DateTime.Now;
                    khoanThuTheoHo.UpdateTime = DateTime.Now;
                    
                    PhiDichVu phiDichVu = JObject.Parse(khoanThu.ChiTiet).ToObject<PhiDichVu>();
                    PhiGuiXe phiGuiXe = JObject.Parse(khoanThu.ChiTiet).ToObject<PhiGuiXe>();
                    PhiQuanLy phiQuanLy = JObject.Parse(khoanThu.ChiTiet).ToObject<PhiQuanLy>();
                    // Access the deserialized object
                    if (khoanThu.LoaiKhoanThu == 1)
                    {
                        var phiSinhHoat = dsPhiSinhHoat.Find(o => o.MaHoKhau == hoKhau.MaHoKhau);
                        khoanThuTheoHo.SoTien = phiSinhHoat.Dien + phiSinhHoat.Nuoc + phiSinhHoat.Internet;
                    }
                    else if (khoanThu.LoaiKhoanThu == 2)
                    {
                        var phong = _dbcontext.Phong.Where(o => o.Delete == Constant.NOT_DELETE && o.MaHoKhau == hoKhau.MaHoKhau).FirstOrDefault();
                        if (phong == null) continue;
                        khoanThuTheoHo.SoTien = (int)Math.Ceiling(phiDichVu.DichVu.SoTien * phong.DienTich);
                    }
                    else if (khoanThu.LoaiKhoanThu == 3)
                    {
                        var phong = _dbcontext.Phong.Where(o => o.Delete == Constant.NOT_DELETE && o.MaHoKhau == hoKhau.MaHoKhau).FirstOrDefault();
                        if (phong == null) continue;
                        khoanThuTheoHo.SoTien = (int)Math.Ceiling(phiQuanLy.QuanLy.SoTien * phong.DienTich);
                    }
                    else if (khoanThu.LoaiKhoanThu == 4)
                    {
                        var xeMay = _dbcontext.Xe.Where(o => o.Delete == Constant.NOT_DELETE && o.MaLoaiXe == "LX001" && o.MaHoKhau == hoKhau.MaHoKhau).ToArray();
                        var xeOto = _dbcontext.Xe.Where(o => o.Delete == Constant.NOT_DELETE && o.MaLoaiXe == "LX002" && o.MaHoKhau == hoKhau.MaHoKhau).ToArray();
                        khoanThuTheoHo.SoTien = phiGuiXe.XeMay.SoTien * xeMay.Length + phiGuiXe.XeOto.SoTien * xeOto.Length;
                    }
                    _dbcontext.KhoanThuTheoHo.Add(khoanThuTheoHo);
                    _dbcontext.SaveChanges();
                }
                return khoanThu.MaKhoanThu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateKhoanThuTheoHo(int maKhoanThu, string userName)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var khoanThu = _dbcontext.KhoanThu.Where(
                   o => o.MaKhoanThu == maKhoanThu && o.Delete == Constant.NOT_DELETE).FirstOrDefault();
                var listHoKhau = _dbcontext.HoKhau.Where(o => o.Delete == Constant.NOT_DELETE).ToList();
                foreach (var hoKhau in listHoKhau)
                {
                    KhoanThuTheoHoEntity khoanThuTheoHo = new KhoanThuTheoHoEntity();
                    khoanThuTheoHo.MaHoKhau = hoKhau.MaHoKhau;
                    khoanThuTheoHo.MaKhoanThu = maKhoanThu;
                    khoanThuTheoHo.UserCreate = userName;
                    khoanThuTheoHo.UserUpdate = userName;
                    khoanThuTheoHo.CreateTime = DateTime.Now;
                    khoanThuTheoHo.UpdateTime = DateTime.Now;
                    if (khoanThu.LoaiKhoanThu == 1)
                    {
                        // check người đã đăng ký tạm vắng
                        var listNhanKhau = _dbcontext.NhanKhau.Where(
                            o => o.MaHoKhau == hoKhau.MaHoKhau 
                            && o.TrangThai == Constant.ALIVE
                            && o.Delete == Constant.NOT_DELETE).ToList();
                        // tinh tien phi ve sinh
                        khoanThuTheoHo.SoTien = 6000 * listNhanKhau.Count();
                    }
                    _dbcontext.KhoanThuTheoHo.Add(khoanThuTheoHo);
                    _dbcontext.SaveChanges();
                }
                return maKhoanThu;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<KhoanThuTheoHoEntity> GetKhoanThuTheoHo(int maKhoanThu)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var khoanThuTheoHo = _dbcontext.KhoanThuTheoHo.Where(
                   o => o.MaKhoanThu == maKhoanThu && o.Delete == Constant.NOT_DELETE).ToList();
                
                return khoanThuTheoHo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<KhoanThuTheoHoEntity> GetCacKhoanThuCuaHo(string maHoKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var khoanThuTheoHo = _dbcontext.KhoanThuTheoHo.Where(
                   o => o.MaHoKhau == maHoKhau && o.Delete == Constant.NOT_DELETE).ToList();
                return khoanThuTheoHo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateKhoanThu(KhoanThuEntity newKhoanThu)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var khoanThu = _dbcontext.KhoanThu.FirstOrDefault(
                    o => o.MaKhoanThu == newKhoanThu.MaKhoanThu && o.Delete == Constant.NOT_DELETE);
                if (khoanThu != null)
                {
                    khoanThu.UserUpdate = newKhoanThu.UserUpdate;
                    khoanThu.UpdateTime = newKhoanThu.UpdateTime;
                    khoanThu.TenKhoanThu = newKhoanThu.TenKhoanThu;
                    khoanThu.ThoiGianBatDau = newKhoanThu.ThoiGianBatDau;
                    khoanThu.ThoiGianKetThuc = newKhoanThu.ThoiGianKetThuc;
                    khoanThu.GhiChu = newKhoanThu.GhiChu;
                    khoanThu.Version = newKhoanThu.Version;
                    _dbcontext.SaveChanges();
                    return newKhoanThu.MaKhoanThu;
                }
                else return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteKhoanThu(int maKhoanThu, string userNameUpdate)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var khoanThu = _dbcontext.KhoanThu.FirstOrDefault(
                    o => o.MaKhoanThu == maKhoanThu && o.Delete == Constant.NOT_DELETE);
                if (khoanThu != null)
                {
                    khoanThu.Delete = Constant.DELETE;
                    khoanThu.UserUpdate = userNameUpdate;
                    khoanThu.UpdateTime = DateTime.Now;
                    khoanThu.Version++;
                    // xóa các khoản thu theo hộ ứng với mã khoản thu
                    var listKhoanThuTheoHo = _dbcontext.KhoanThuTheoHo.Where(o => o.MaKhoanThu == maKhoanThu && o.Delete == Constant.NOT_DELETE).ToList();
                    foreach (var khoanThuTheoHo in listKhoanThuTheoHo)
                    {
                        khoanThuTheoHo.Delete = Constant.DELETE;
                        khoanThuTheoHo.UserUpdate = userNameUpdate;
                        khoanThuTheoHo.UpdateTime = DateTime.Now;
                        khoanThuTheoHo.Version++;
                    }
                    _dbcontext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ThanhToan(HoaDonEntity hoaDon)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.HoaDon.Add(hoaDon);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return -1;
                return hoaDon.MaHoaDon;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<HoaDonEntity> GetHoaDonKhoanThuTheoHo(int maKhoanThuTheoHo)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var hoaDon = _dbcontext.HoaDon.Where(
                   o => o.MaKhoanThuTheoHo == maKhoanThuTheoHo && o.Delete == Constant.NOT_DELETE).ToList();
                return hoaDon;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}