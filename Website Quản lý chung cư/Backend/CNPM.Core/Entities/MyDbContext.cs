using Microsoft.EntityFrameworkCore;
using CNPM.Core.Utils;
using Microsoft.Extensions.Configuration;
namespace CNPM.Core.Entities
{

    public class MyDbContext : DbContext
    {
        // Khởi tạo bảng
        //private readonly IConfiguration _configuration;

        public DbSet<UserEntity> Users { set; get; }
        public DbSet<RoleEntity> Roles { set; get; }
        public DbSet<LoginInfoEntity> LoginInfos { set; get; }
        public DbSet<HoKhauEntity> HoKhau { set; get; }
        public DbSet<NhanKhauEntity> NhanKhau { set; get; }
        public DbSet<TamTruEntity> TamTru { set; get; }
        public DbSet<TamVangEntity> TamVang { set; get; }
        public DbSet<LichSuEntity> LichSu { set; get; }
        public DbSet<KhoanThuEntity> KhoanThu { set; get; }
        public DbSet<KhoanThuTheoHoEntity> KhoanThuTheoHo { set; get; }
        public DbSet<HoaDonEntity> HoaDon { set; get; }


        // Chuỗi kết nối tới CSDL (MS SQL Server)

        private const string connectionString = Constant.CONNECTION_STRING;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //Console.WriteLine(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<OutputProductEntity>().HasKey(table => new
            //{
            //    table.OutputInfoId,
            //    table.ProductId,
            //    table
            //});
            //builder.Entity<ProductBatchProductEntity>().HasKey(table => new
            //{
            //    table.ProductBatchId,
            //    table.ProductId
            //});
        }

    }
}