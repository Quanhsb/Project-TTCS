﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;

namespace CNPM.Repository.Interfaces
{
    public interface IXeRepository
    {
        public List<XeEntity> GetListXe(int index, int limit);
        public XeEntity GetXe(int maXe);
        public XeEntity GetXeByBienKiemSoat(string bienKiemSoat);
        public int CreateXe(XeEntity xe);
        public bool UpdateXe(XeEntity xe);
        public bool DeleteXe(int maXe, string userName);
    }
}