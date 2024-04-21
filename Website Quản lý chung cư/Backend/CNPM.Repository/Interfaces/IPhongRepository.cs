using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;

namespace CNPM.Repository.Interfaces
{
    public interface IPhongRepository
    {
        public List<PhongEntity> GetListPhong(int index, int limit);
        public PhongEntity GetPhong(int maPhong);
        public bool CreatePhong(PhongEntity phong);
        public bool UpdatePhong(PhongEntity phong);
        public bool DeletePhong(int maPhong, string userName);
    }
}