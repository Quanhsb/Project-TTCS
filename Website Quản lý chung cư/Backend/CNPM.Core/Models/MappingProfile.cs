﻿using AutoMapper;
using CNPM.Core.Entities;
using CNPM.Core.Models.HoKhau;
using CNPM.Core.Models.NhanKhau;
using CNPM.Core.Models.KhoanThu;
using CNPM.Core.Models.TamTru;
using CNPM.Core.Models.TamVang;
using CNPM.Core.Models.HoaDon;
using CNPM.Core.Models.Xe;
using CNPM.Core.Models.CanHo;
namespace CNPM.Core.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDto1002>();
            CreateMap<UserEntity, UserDto1003>();
            CreateMap<UserEntity, UserDto1001>();
            CreateMap<UserDto1005, UserEntity>();
            CreateMap<UserDto1005, UserDto1003>();
            CreateMap<UserDto1006, UserDto1003>();
            CreateMap<UserDto1006, UserEntity>();
            CreateMap<UserDto1007, UserEntity>();
            CreateMap<NhanKhauDto1000, NhanKhauEntity>();
            CreateMap<NhanKhauEntity, NhanKhauDto1001>();
            CreateMap<NhanKhauDto1002, NhanKhauEntity>();
            CreateMap<NhanKhauEntity, NhanKhauDto1003>();
            CreateMap<KhoanThuDto1000, KhoanThuEntity>();
            CreateMap<KhoanThuEntity, KhoanThuDto1001>();
            CreateMap<KhoanThuDto1002, KhoanThuEntity>();
            CreateMap<KhoanThuEntity, KhoanThuDto1003>();
            CreateMap<KhoanThuTheoHoEntity, KhoanThuDto1004>();
            CreateMap<HoaDonDto1000, HoaDonEntity>();
            CreateMap<HoKhauDto1000, HoKhauEntity>();
            CreateMap<HoKhauEntity, HoKhauDto1001>();
            CreateMap<HoKhauDto1002, HoKhauEntity>();
            CreateMap<HoKhauEntity, HoKhauDto1003>();
            CreateMap<TamTruDto1000, TamTruEntity>();
            CreateMap<TamTruEntity, TamTruDto1001>();
            CreateMap<TamTruDto1002, TamTruEntity>();
            CreateMap<TamTruEntity, TamTruDto1003>();
            CreateMap<TamVangDto1000, TamVangEntity>();
            CreateMap<TamVangEntity, TamVangDto1001>();
            CreateMap<TamVangDto1002, TamVangEntity>();
            CreateMap<TamVangEntity, TamVangDto1003>();
            CreateMap<XeDto1000, XeEntity>();
            CreateMap<XeEntity, XeDto1001>();
            CreateMap<XeDto1002, XeEntity>();
            CreateMap<XeEntity, XeDto1003>();
            CreateMap<CanHoDto1000, CanHoEntity>();
            CreateMap<CanHoEntity, CanHoDto1001>();
            CreateMap<CanHoDto1002, CanHoEntity>();
            CreateMap<CanHoEntity, CanHoDto1003>();
            CreateMap<RoleEntity, RoleDto>();
        }
    }
}