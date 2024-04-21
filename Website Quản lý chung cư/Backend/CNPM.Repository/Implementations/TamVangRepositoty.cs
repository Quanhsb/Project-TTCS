using System;
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
using System.ComponentModel.DataAnnotations;
namespace CNPM.Repository.Implementations
{
    public class TamVangRepository : ITamVangRepository
    {
        public List<TamVangEntity> GetListTamVang(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<TamVangEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.TamVang!.Where(
                    o => o.Delete == Constant.NOT_DELETE).ToList();
                }
                else arr = _dbcontext.TamVang!.Where(
                    o => o.Delete == Constant.NOT_DELETE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public TamVangEntity GetTamVang(int maTamVang)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                TamVangEntity tamVang = _dbcontext.TamVang!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaTamVang == maTamVang).FirstOrDefault();
                return tamVang;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateTamVang(TamVangEntity tamVang)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.TamVang.Add(tamVang);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return -1;
                TamVangEntity tamVangCreated = _dbcontext.TamVang.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaNhanKhau == tamVang.MaNhanKhau).FirstOrDefault();
                return tamVangCreated.MaTamVang;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckExistCongDanDaDangKiTamVang(int maNhanKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                TamVangEntity tamVang = _dbcontext.TamVang!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaNhanKhau == maNhanKhau).FirstOrDefault();
                if (tamVang == null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckExistCongDanDaDangKiTamVangUpdate(int maTamVang, int maNhanKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                TamVangEntity tamVang = _dbcontext.TamVang!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaNhanKhau == maNhanKhau).FirstOrDefault();
                if (tamVang == null || tamVang.MaTamVang == maTamVang) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateTamVang(TamVangEntity newTamVang)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var tamVang = _dbcontext.TamVang!.FirstOrDefault(
                    o => o.MaTamVang == newTamVang.MaTamVang && o.Delete == Constant.NOT_DELETE);
                if (tamVang != null)
                {
                    tamVang.UserUpdate = newTamVang.UserUpdate;
                    tamVang.UpdateTime = newTamVang.UpdateTime;
                    tamVang.ThoiHan = newTamVang.ThoiHan;
                    tamVang.LyDo = newTamVang.LyDo;
                    tamVang.MaNhanKhau = newTamVang.MaNhanKhau;
                    tamVang.Version = newTamVang.Version;
                    _dbcontext.SaveChanges();
                    return newTamVang.MaTamVang;
                }
                else return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTamVang(int maTamVang, string userNameUpdate)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var tamVang = _dbcontext.TamVang!.FirstOrDefault(
                    o => o.MaTamVang == maTamVang && o.Delete == Constant.NOT_DELETE);
                if (tamVang != null)
                {
                    tamVang.Delete = Constant.DELETE;
                    tamVang.UserUpdate = userNameUpdate;
                    tamVang.UpdateTime = DateTime.Now;
                    tamVang.Version++;
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
