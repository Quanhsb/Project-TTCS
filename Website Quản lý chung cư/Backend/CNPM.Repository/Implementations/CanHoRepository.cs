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
using System.ComponentModel.DataAnnotations;
namespace CNPM.Repository.Implementations
{
    public class CanHoRepository : ICanHoRepository
    {
        public List<CanHoEntity> GetListCanHo(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<CanHoEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.CanHo!.Where(
                    o => o.Delete == Constant.NOT_DELETE).ToList();
                }
                else arr = _dbcontext.CanHo!.Where(
                    o => o.Delete == Constant.NOT_DELETE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CanHoEntity GetCanHo(int maCanHo)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                CanHoEntity xe = _dbcontext.CanHo!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaCanHo == maCanHo).FirstOrDefault();
                return xe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CanHoEntity GetCanHoByHoKhau(string maHoKhau)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                CanHoEntity xe = _dbcontext.CanHo!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaHoKhau == maHoKhau).FirstOrDefault();
                return xe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CreateCanHo(CanHoEntity canHo)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.CanHo.Add(canHo);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateCanHo(CanHoEntity newCanHo)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var canHo = _dbcontext.CanHo!.FirstOrDefault(
                    o => o.MaCanHo == newCanHo.MaCanHo && o.Delete == Constant.NOT_DELETE);
                if (canHo != null)
                {
                    canHo.UserUpdate = newCanHo.UserUpdate;
                    canHo.UpdateTime = newCanHo.UpdateTime;
                    canHo.TenCanHo = newCanHo.TenCanHo;
                    canHo.Tang = newCanHo.Tang;
                    canHo.DienTich = newCanHo.DienTich;
                    canHo.MaHoKhau = newCanHo.MaHoKhau;
                    canHo.MoTa = newCanHo.MoTa;
                    canHo.Version = newCanHo.Version;
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
        public bool DeleteCanHo(int maCanHo, string userNameUpdate)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var canHo = _dbcontext.CanHo!.FirstOrDefault(
                    o => o.MaCanHo == maCanHo && o.Delete == Constant.NOT_DELETE);
                if (canHo != null)
                {
                    canHo.Delete = Constant.DELETE;
                    canHo.MaHoKhau = null;
                    canHo.UserUpdate = userNameUpdate;
                    canHo.UpdateTime = DateTime.Now;
                    canHo.Version++;
                    var listTamTru = _dbcontext.TamTru!.Where(o => o.DiaChiTamTru == canHo.TenCanHo && o.Delete == Constant.NOT_DELETE).ToList();
                    for (int i = 0; i < listTamTru.Count; i++)
                    {
                        listTamTru[i].DiaChiTamTru = "";
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
    }
}
