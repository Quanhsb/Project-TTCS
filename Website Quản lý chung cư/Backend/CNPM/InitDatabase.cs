using Microsoft.EntityFrameworkCore;
using CNPM.Repository;
using CNPM.Core.Models;
using CNPM.Core.Entities;
using CNPM.Core.Utils;
namespace CNPM
{
    public class InitDatabase
    {
        public static async Task CreateDatabase()
        {
            using var dbcontext = new MyDbContext();
            var result = await dbcontext.Database.EnsureCreatedAsync();
            string resultstring = result ? "Create database successfully" : "Database is already";
            Console.WriteLine(resultstring);
        }
        public static async Task DeleteDatabase()
        {
            using (var context = new MyDbContext())
            {
                bool deleted = await context.Database.EnsureDeletedAsync();
                string deletionInfo = deleted ? "Database is deleted" : "Can not delete database";
                Console.WriteLine(deletionInfo);
            }
        }
        public static async void ResetDb()
        {
            await DeleteDatabase();
            await CreateDatabase();
            Init();
        }
        public static void Init()
        {
            using var dbcontext = new MyDbContext();
            var listPermissionEntity = new List<RoleEntity>()
            {
                new RoleEntity("Administrator"),
                new RoleEntity("Manager"),
                new RoleEntity("Stocker")
            };
            DateTime today = DateTime.Now;
            listPermissionEntity.ForEach(o => dbcontext.Add(o));
            var superAdmin = new UserEntity
            {
                UserName = "admin",
                Password = "$2a$11$ew1SDZWnBOiPna6ZHaTWHuhEELiDGAan8/6cvBI6gCgWZ17vJB0oG", //123456
                FirstName = "Đạt",
                LastName = "Trần",
                Email = "ttdat2112@gmail.com",
                RoleId = 1,
                CreateTime = today,
                UpdateTime = today,
                UserCreate = "admin",
                UserUpdate = "admin",
                Version = 0,
                Delete = 0
            };
            dbcontext.Add(superAdmin);
            var listLoaiXe = new List<LoaiXeEntity>()
            {
                new LoaiXeEntity
                {
                    MaLoaiXe = "LX001",
                    LoaiXe = "Xe máy",
                    CreateTime = today,
                    UpdateTime = today,
                    UserCreate = "admin",
                    UserUpdate = "admin",
                    Version = 0,
                    Delete = 0
                },
                new LoaiXeEntity
                {
                    MaLoaiXe = "LX002",
                    LoaiXe = "Xe ô tô",
                    CreateTime = today,
                    UpdateTime = today,
                    UserCreate = "admin",
                    UserUpdate = "admin",
                    Version = 0,
                    Delete = 0
                }
            };
            listLoaiXe.ForEach(o => dbcontext.Add(o));
            dbcontext.SaveChanges();
        }
    }
}
