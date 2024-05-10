﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CNPM.Core.Utils
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TrangThaiAttribute : ValidationAttribute
    {
        public static ValidationResult IsTrangThai(int trangThai)
        {
            if (trangThai == 0 || trangThai == 1)
                return ValidationResult.Success;
            else
                return new ValidationResult("Invalid");
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LoaiKhoanThuAttribute : ValidationAttribute
    {
        public static ValidationResult IsKhoanThu(int khoanThu)
        {
            if (khoanThu == 0 || khoanThu == 1 || khoanThu == 2 || khoanThu == 3 || khoanThu == 4)
                return ValidationResult.Success;
            else
                return new ValidationResult("Invalid");
        }
    }
}