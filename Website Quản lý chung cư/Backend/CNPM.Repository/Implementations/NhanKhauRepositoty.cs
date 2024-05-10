﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Repository.Interfaces;
using CNPM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CNPM.Core.Utils;
using CNPM.Core.Models;
using CNPM.Core.Models.NhanKhau;
namespace CNPM.Repository.Implementations
{
    public class NhanKhauRepository : INhanKhauRepository
    {
        public List<NhanKhauEntity> GetListNhanKhau(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<NhanKhauEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE).ToList();
                }
                else arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<NhanKhauEntity> GetListNhanKhauNotHaveHoKhau(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<NhanKhauEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaHoKhau == null).ToList();
                }
                else arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaHoKhau == null).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<NhanKhauEntity> GetListNhanKhauAlive(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<NhanKhauEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.TrangThai == Constant.ALIVE).ToList();
                }
                else arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.TrangThai == Constant.ALIVE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<NhanKhauEntity> GetListNhanKhauInHoKhau(string maHoKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<NhanKhauEntity> arr = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE
                    && o.MaHoKhau == maHoKhau
                ).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public NhanKhauEntity GetNhanKhau(int maNhanKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                NhanKhauEntity nhanKhau = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaNhanKhau == maNhanKhau).FirstOrDefault();
                return nhanKhau;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateNhanKhau(NhanKhauEntity nhanKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.NhanKhau.Add(nhanKhau);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return -1;
                NhanKhauEntity nhanKhauCreated = _dbcontext.NhanKhau.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.CanCuocCongDan == nhanKhau.CanCuocCongDan).FirstOrDefault();
                return nhanKhauCreated.MaNhanKhau;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckExistCanCuocCongDan(string canCuocCongDan)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                NhanKhauEntity nhanKhau = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.CanCuocCongDan == canCuocCongDan).FirstOrDefault();
                if (nhanKhau == null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckExistCanCuocCongDanUpdate(int maNhanKhau, string canCuocCongDan)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                NhanKhauEntity nhanKhau = _dbcontext.NhanKhau!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.CanCuocCongDan == canCuocCongDan).FirstOrDefault();
                if (nhanKhau == null || nhanKhau.MaNhanKhau == maNhanKhau) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateNhanKhau(NhanKhauEntity newNhanKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var nhanKhau = _dbcontext.NhanKhau!.FirstOrDefault(
                    o => o.MaNhanKhau == newNhanKhau.MaNhanKhau && o.Delete == Constant.NOT_DELETE);
                if (nhanKhau != null)
                {
                    nhanKhau.UserUpdate = newNhanKhau.UserUpdate;
                    nhanKhau.UpdateTime = newNhanKhau.UpdateTime;
                    nhanKhau.HoTen = newNhanKhau.HoTen;
                    nhanKhau.CanCuocCongDan = newNhanKhau.CanCuocCongDan;
                    nhanKhau.NgaySinh = newNhanKhau.NgaySinh;
                    nhanKhau.NoiSinh = newNhanKhau.NoiSinh;
                    nhanKhau.DanToc = newNhanKhau.DanToc;
                    nhanKhau.NgheNghiep = newNhanKhau.NgheNghiep;
                    nhanKhau.TrangThai = newNhanKhau.TrangThai;
                    nhanKhau.QuanHe = newNhanKhau.QuanHe;
                    nhanKhau.GhiChu = newNhanKhau.GhiChu;
                    nhanKhau.Version = newNhanKhau.Version;
                    _dbcontext.SaveChanges();
                    return newNhanKhau.MaNhanKhau;
                }
                else return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteNhanKhau(int maNhanKhau, string userNameUpdate)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var nhanKhau = _dbcontext.NhanKhau!.FirstOrDefault(
                    o => o.MaNhanKhau == maNhanKhau && o.Delete == Constant.NOT_DELETE);
                if (nhanKhau != null)
                {
                    nhanKhau.Delete = Constant.DELETE;
                    nhanKhau.UserUpdate = userNameUpdate;
                    nhanKhau.UpdateTime = DateTime.Now;
                    nhanKhau.Version++;
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
    }
}