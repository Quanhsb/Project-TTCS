﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;
namespace CNPM.Repository.Interfaces
{
    public interface IHoKhauRepository
    {
        public List<HoKhauEntity> GetListHoKhau(int index, int limit);
        public HoKhauEntity GetHoKhau(string maHoKhau);
        public string CreateHoKhau(HoKhauEntity hoKhau);
        public string UpdateHoKhau(HoKhauEntity hoKhau);
        public bool CheckMaHoKhauExisted(string maHoKhau);
        public bool DeleteHoKhau(string maHoKhau, string userName);
        public bool AddNhanKhauToHoKhau(List<int> danhSachNhanKhau, string maHoKhau, string userName);
        public bool AddCanHoToHoKhau(string maHoKhau, int maCanHo, string userName);
        public bool RemoveCanHoFromHoKhau(string maHoKhau, string userName);
        public bool RemoveNhanKhauFromHoKhau(string maHoKhau, string userName);
    }
}