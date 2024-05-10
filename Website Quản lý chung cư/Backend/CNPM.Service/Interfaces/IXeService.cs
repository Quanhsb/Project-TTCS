﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Models;
using CNPM.Core.Models.TamTru;
using Microsoft.AspNetCore.Mvc;
namespace CNPM.Service.Interfaces
{
    public interface IXeService
    {
        public IActionResult GetListXe(int index, int limit);
        public IActionResult GetXe(int maXe);
        public IActionResult CreateXe(string token, TamTruDto1000 tamTru);
        public IActionResult UpdateTamTru(string token, int maTamTru, TamTruDto1002 tamTru);
        public IActionResult DeleteTamTru(int maTamTru, string token, int version);
    }
}