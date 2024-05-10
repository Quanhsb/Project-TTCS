﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;
using CNPM.Core.Models.NhanKhau;
namespace CNPM.Repository.Interfaces
{
    public interface ITamVangRepository
    {
        public List<TamVangEntity> GetListTamVang(int index, int limit);
        public TamVangEntity GetTamVang(int maTamVang);
        public int CreateTamVang(TamVangEntity tamVang);
        public bool CheckExistCongDanDaDangKiTamVang(int maNhanKhau);
        public bool CheckExistCongDanDaDangKiTamVangUpdate(int maTamVang, int maNhanKhau);
        public int UpdateTamVang(TamVangEntity tamVang);
        public bool DeleteTamVang(int maTamVang, string userName);
    }
}