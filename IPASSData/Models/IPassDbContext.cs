using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IPASSData.Models
{
    public class IPassDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration _configuration;
        public IPassDbContext(IConfiguration configuration, DbContextOptions<IPassDbContext> options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用者
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Id).HasComment("使用者 Id").HasDefaultValueSql("NEWID()");
                entity.Property(u => u.Username).HasComment("姓名");
                entity.Property(u => u.Ac).HasComment("帳號");
                entity.Property(u => u.Sw).HasComment("密碼");
                entity.Property(u => u.Email).HasComment("信箱");
                entity.Property(u => u.Phone).HasComment("手機");
                entity.Property(u => u.AuthenticatorUserId).HasComment("驗證器 使用者 Id");
                entity.Property(u => u.SourceId).HasComment("平台來源Id");
                entity.Property(u => u.Is_SuperUser).HasComment("超級使用者");
                entity.Property(u => u.Is_Active).HasComment("帳號狀態");
                entity.Property(u => u.Last_Login).HasComment("上次登入時間");
                entity.Property(u => u.CreateBy).HasComment("建立者 Id").HasDefaultValueSql("NEWID()");
                entity.Property(u => u.DeleteBy).HasComment("刪除者 Id");
                entity.Property(u => u.UpdateBy).HasComment("更新者 Id");
                entity.Property(u => u.IsDelete).HasComment("是否刪除").HasDefaultValue(false);
                entity.Property(u => u.CreateAt).HasComment("創建日期").HasDefaultValueSql("GetDate()");
                entity.Property(u => u.UpdateAt).HasComment("更新日期").HasDefaultValueSql("GetDate()");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
