﻿using Microsoft.EntityFrameworkCore;
using CNPM.Core.Utils;
using Microsoft.Extensions.Configuration;
namespace CNPM.Core.Entities
{
    public class MyDbContext : DbContext
    {
        public DbSet<UserEntity>? Users { set; get; }
        public DbSet<RoleEntity>? Roles { set; get; }
        public DbSet<LoginInfoEntity>? LoginInfos { set; get; }
        public DbSet<HoKhauEntity>? HoKhau { set; get; }
        public DbSet<NhanKhauEntity>? NhanKhau { set; get; }
        public DbSet<TamTruEntity>? TamTru { set; get; }
        public DbSet<TamVangEntity>? TamVang { set; get; }
        public DbSet<KhoanThuEntity>? KhoanThu { set; get; }
        public DbSet<KhoanThuTheoHoEntity>? KhoanThuTheoHo { set; get; }
        public DbSet<HoaDonEntity>? HoaDon { set; get; }
        public DbSet<CanHoEntity>? CanHo { set; get; }
        public DbSet<LoaiXeEntity>? LoaiXe { set; get; }
        public DbSet<XeEntity>? Xe { set; get; }
        private const string connectionString = Constant.CONNECTION_STRING;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}