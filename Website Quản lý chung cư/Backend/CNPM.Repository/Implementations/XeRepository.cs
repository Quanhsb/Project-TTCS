﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Repository.Interfaces;
using CNPM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CNPM.Core.Utils;
using CNPM.Core.Models;
using System.ComponentModel.DataAnnotations;
namespace CNPM.Repository.Implementations
{
    public class XeRepository : IXeRepository
    {
        public List<XeEntity> GetListXe(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<XeEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.Xe!.Where(
                    o => o.Delete == Constant.NOT_DELETE).ToList();
                }
                else arr = _dbcontext.Xe!.Where(
                    o => o.Delete == Constant.NOT_DELETE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<XeEntity> GetListXeByHoKhau(string maHoKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<XeEntity> arr = _dbcontext.Xe!.Where(
                    o => o.Delete == Constant.NOT_DELETE & o.MaHoKhau == maHoKhau).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public XeEntity GetXe(int maXe)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                XeEntity xe = _dbcontext.Xe!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaXe == maXe).FirstOrDefault();
                return xe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public XeEntity GetXeByBienKiemSoat(string bienKhiemSoat)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                XeEntity xe = _dbcontext.Xe!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.BienKiemSoat == bienKhiemSoat).FirstOrDefault();
                return xe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateXe(XeEntity xe)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.Xe.Add(xe);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return -1;
                XeEntity xeCreated = _dbcontext.Xe.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.BienKiemSoat == xe.BienKiemSoat).FirstOrDefault();
                return xeCreated.MaXe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateXe(XeEntity newXe)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var xe = _dbcontext.Xe!.FirstOrDefault(
                    o => o.MaXe == newXe.MaXe && o.Delete == Constant.NOT_DELETE);
                if (xe != null)
                {
                    xe.UserUpdate = newXe.UserUpdate;
                    xe.UpdateTime = newXe.UpdateTime;
                    xe.BienKiemSoat = newXe.BienKiemSoat;
                    xe.MaLoaiXe = newXe.MaLoaiXe;
                    xe.MoTa = newXe.MoTa;
                    xe.TenXe = newXe.TenXe;
                    xe.Version = newXe.Version + 1;
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
        public bool DeleteXe(int maXe, string userNameUpdate)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var xe = _dbcontext.Xe!.FirstOrDefault(
                    o => o.MaXe == maXe && o.Delete == Constant.NOT_DELETE);
                if (xe != null)
                {
                    xe.Delete = Constant.DELETE;
                    xe.UserUpdate = userNameUpdate;
                    xe.UpdateTime = DateTime.Now;
                    xe.Version++;
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