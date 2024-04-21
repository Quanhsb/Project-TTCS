using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Core.Entities;
using CNPM.Core.Models;
using CNPM.Core.Models.NhanKhau;
namespace CNPM.Repository.Interfaces
{
    public interface INhanKhauRepository
    {
        public List<NhanKhauEntity> GetListNhanKhauInHoKhau(string maHoKhau);
        public List<NhanKhauEntity> GetListNhanKhau(int index, int limit);
        public List<NhanKhauEntity> GetListNhanKhauAlive(int index, int limit);
        public List<NhanKhauEntity> GetListNhanKhauNotHaveHoKhau(int index, int limit);
        public NhanKhauEntity GetNhanKhau(int maNhanKhau);
        public int CreateNhanKhau(NhanKhauEntity nhanKhau);
        public bool CheckExistCanCuocCongDan(string CanCuocCongDan);
        public bool CheckExistCanCuocCongDanUpdate(int maNhanKhau, string canCuocCongDan);
        public int UpdateNhanKhau(NhanKhauEntity nhanKhau);
        public bool DeleteNhanKhau(int maNhanKhau, string userName);
    }
}
