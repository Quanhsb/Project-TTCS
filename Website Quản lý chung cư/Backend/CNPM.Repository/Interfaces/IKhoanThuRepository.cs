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
    public interface IKhoanThuRepository
    {
        public List<KhoanThuEntity> GetListKhoanThu(int index, int limit);
        public KhoanThuEntity GetKhoanThu(int maKhoanThu);
        //thêm sửa xóa thông tin khoản thu 
        public int CreateKhoanThu(KhoanThuEntity khoanThu);
        public int UpdateKhoanThu(KhoanThuEntity newKhoanThu);
        public bool DeleteKhoanThu(int maKhoanThu, string userName);
        //tạo tự động khoản thu theo hộ theo mã khoản thu
        public int CreateKhoanThuTheoHo(int maKhoanThu, string userName);
        //lấy danh sách khoản thu theo hộ theo mã khoản thu
        public List<KhoanThuTheoHoEntity> GetKhoanThuTheoHo(int maKhoanThu);
        //lấy danh sách khoản thu theo hộ theo mã hộ khẩu
        public List<KhoanThuTheoHoEntity> GetCacKhoanThuCuaHo(string maHoKhau);
        public int ThanhToan(HoaDonEntity hoaDon);
        public List<HoaDonEntity> GetHoaDonKhoanThuTheoHo(int maKhoanThuTheoHo);
    }
}