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
using System.ComponentModel.DataAnnotations;
namespace CNPM.Repository.Implementations
{
    public class TamTruRepository : ITamTruRepository
    {
        public List<TamTruEntity> GetListTamTru(int index, int limit)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                List<TamTruEntity> arr;
                if (index == 0 && limit == 0)
                {
                    arr = _dbcontext.TamTru!.Where(
                    o => o.Delete == Constant.NOT_DELETE).ToList();
                }
                else arr = _dbcontext.TamTru!.Where(
                    o => o.Delete == Constant.NOT_DELETE).Skip(limit * (index - 1)).Take(limit).ToList();
                return arr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public TamTruEntity GetTamTru(int maTamTru)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                TamTruEntity tamTru = _dbcontext.TamTru!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.MaTamTru == maTamTru).FirstOrDefault();
                return tamTru;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CreateTamTru(TamTruEntity tamTru)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                _dbcontext.TamTru.Add(tamTru);
                int number_rows = _dbcontext.SaveChanges();
                if (number_rows <= 0) return -1;
                TamTruEntity tamTruCreated = _dbcontext.TamTru.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.CanCuocCongDan == tamTru.CanCuocCongDan).FirstOrDefault();
                return tamTruCreated.MaTamTru;
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
                TamTruEntity tamTru = _dbcontext.TamTru!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.CanCuocCongDan == canCuocCongDan).FirstOrDefault();
                if (tamTru == null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckExistCanCuocCongDanTrongNhanKhau(string canCuocCongDan)
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
        public bool CheckExistCanCuocCongDanUpdate(string canCuocCongDan, int maTamTru)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                TamTruEntity tamTru = _dbcontext.TamTru!.Where(
                    o => o.Delete == Constant.NOT_DELETE && o.CanCuocCongDan == canCuocCongDan).FirstOrDefault();
                if (tamTru == null || tamTru.MaTamTru == maTamTru) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateTamTru(TamTruEntity newTamTru)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var tamTru = _dbcontext.TamTru!.FirstOrDefault(
                    o => o.MaTamTru == newTamTru.MaTamTru && o.Delete == Constant.NOT_DELETE);
                if (tamTru != null)
                {
                    tamTru.UserUpdate = newTamTru.UserUpdate;
                    tamTru.UpdateTime = newTamTru.UpdateTime;
                    tamTru.HoTen = newTamTru.HoTen;
                    tamTru.DiaChiTamTru = newTamTru.DiaChiTamTru;
                    tamTru.DiaChiThuongTru = newTamTru.DiaChiThuongTru;
                    tamTru.CanCuocCongDan = newTamTru.CanCuocCongDan;
                    tamTru.Version = newTamTru.Version;
                    _dbcontext.SaveChanges();
                    return newTamTru.MaTamTru;
                }
                else return -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteTamTru(int maTamTru, string userNameUpdate)
        {
            try
            {
                var _dbcontext = new MyDbContext();
                var tamTru = _dbcontext.TamTru!.FirstOrDefault(
                    o => o.MaTamTru == maTamTru && o.Delete == Constant.NOT_DELETE);
                if (tamTru != null)
                {
                    tamTru.Delete = Constant.DELETE;
                    tamTru.UserUpdate = userNameUpdate;
                    tamTru.UpdateTime = DateTime.Now;
                    tamTru.Version++;
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