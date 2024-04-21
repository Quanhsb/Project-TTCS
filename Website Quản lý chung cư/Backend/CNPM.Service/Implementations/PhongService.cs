using CNPM.Core.Models;
using CNPM.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CNPM.Core.Entities;
using CNPM.Core.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using CNPM.Service.Interfaces;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using CNPM.Repository.Implementations;
using System.Collections.Generic;
using CNPM.Core.Models.Phong;

namespace CNPM.Service.Implementations
{

    public class PhongService : IPhongService
    {
        private readonly IPhongRepository _phongRepository;
        private readonly IMapper _mapper;
        public PhongService(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }
        public IActionResult GetListPhong(int index, int limit)
        {
            try
            {
                var listPhong = _phongRepository.GetListPhong(index, limit);
                var arr = _mapper.Map<List<PhongEntity>, List<PhongDto1003>>(listPhong);
                return new OkObjectResult(
                    new
                    {
                        message = Constant.GET_LIST_PHONG_SUCCESSFULLY,
                        data = arr
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IActionResult GetPhong(int maPhong)
        {
            try { 
                PhongEntity phong = _phongRepository.GetPhong(maPhong);

                if (phong == null) return new BadRequestObjectResult(
                       new
                       {
                           message = Constant.GET_PHONG_FAILED,
                           reason = Constant.MA_PHONG_NOT_EXIST
                       }
                    );

                var phong1001 = _mapper.Map<PhongEntity, PhongDto1001>(phong);

                return new OkObjectResult(new {
                    message = Constant.GET_TAM_VANG_SUCCESSFULLY,
                    data = phong1001
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreatePhong(string token, PhongDto1000 phong1000)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");

                PhongEntity phong = _mapper.Map<PhongDto1000, PhongEntity>(phong1000);

                phong.CreateTime = DateTime.Now;
                phong.UpdateTime = DateTime.Now;
                phong.UserCreate = userName;
                phong.UserUpdate = userName;
                if (phong1000.MaHoKhau == "") phong.MaHoKhau = null;
                bool created = _phongRepository.CreatePhong(phong);

                if (created)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.CREATE_PHONG_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.CREATE_PHONG_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult UpdatePhong(string token, int maPhong, PhongDto1002 newPhong)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var phong = _phongRepository.GetPhong(maPhong);
                if (phong == null) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_PHONG_FAILED,
                    reason = Constant.MA_PHONG_NOT_EXIST
                });

                if (phong.Version != newPhong.Version) return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_PHONG_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });



                PhongEntity phongEntity = _mapper.Map<PhongDto1002, PhongEntity>(newPhong);
                if (newPhong.MaHoKhau == "") phongEntity.MaHoKhau = null;
                phongEntity.MaPhong = maPhong;
                phongEntity.UserUpdate = userName;
                phongEntity.UpdateTime = DateTime.Now;
                phongEntity.Version += 1;
                bool updated = _phongRepository.UpdatePhong(phongEntity);
                if (updated)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.UPDATE_PHONG_SUCCESSFULLY
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.UPDATE_PHONG_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeletePhong(int maPhong, string token, int version)
        {
            try
            {
                var userName = Helpers.DecodeJwt(token, "username");
                var phong = _phongRepository.GetPhong(maPhong);


                if (phong == null) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_PHONG_FAILED,
                    reason = Constant.MA_PHONG_NOT_EXIST
                });


                if (phong.Version != version) return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_PHONG_FAILED,
                    reason = Constant.DATA_UPDATED_BEFORE
                });

                bool delete = _phongRepository.DeletePhong(maPhong, userName);
                if (delete)
                {
                    return new OkObjectResult(new
                    {
                        message = Constant.DELETE_PHONG_SUCCESSFULLY,
                    });
                }
                return new BadRequestObjectResult(new
                {
                    message = Constant.DELETE_PHONG_FAILED
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}