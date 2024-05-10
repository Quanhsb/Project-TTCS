﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;
namespace CNPM.Repository.Interfaces
{
    public interface ICanHoRepository
    {
        public List<CanHoEntity> GetListCanHo(int index, int limit);
        public CanHoEntity GetCanHo(int maCanHo);
        public CanHoEntity GetCanHoByHoKhau(string maHoKhau);
        public bool CreateCanHo(CanHoEntity canHo);
        public bool UpdateCanHo(CanHoEntity canHo);
        public bool DeleteCanHo(int maCanHo, string userName);
    }
}