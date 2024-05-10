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
    public interface ITamTruRepository
    {
        public List<TamTruEntity> GetListTamTru(int index, int limit);
        public TamTruEntity GetTamTru(int maTamTru);
        public int CreateTamTru(TamTruEntity tamTru);
        public bool CheckExistCanCuocCongDan(string CanCuocCongDan);
        public bool CheckExistCanCuocCongDanTrongNhanKhau(string CanCuocCongDan);
        public bool CheckExistCanCuocCongDanUpdate(string canCuocCongDan, int maTamTru);
        public int UpdateTamTru(TamTruEntity tamTru);
        public bool DeleteTamTru(int maTamTru, string userName);
    }
}