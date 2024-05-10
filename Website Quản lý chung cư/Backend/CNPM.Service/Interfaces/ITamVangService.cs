﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Models;
using CNPM.Core.Models.TamVang;
using Microsoft.AspNetCore.Mvc;
namespace CNPM.Service.Interfaces
{
    public interface ITamVangService
    {
        public IActionResult GetListTamVang(int index, int limit);
        public IActionResult GetTamVang(int maTamVang);
        public IActionResult CreateTamVang(string token, TamVangDto1000 tamVang);
        public IActionResult UpdateTamVang(string token, int maTamVang, TamVangDto1002 tamVang);
        public IActionResult DeleteTamVang(int maTamVang, string token, int version);
    }
}