using Abp.Zero.EntityFrameworkCore;
using KiemKeDatDai.Authorization.Roles;
using KiemKeDatDai.Authorization.Users;
using KiemKeDatDai.EntitiesDb;
using KiemKeDatDai.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace KiemKeDatDai.EntityFrameworkCore
{
    public class KiemKeDatDaiDbContext : AbpZeroDbContext<Tenant, Role, User, KiemKeDatDaiDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<ConfigSystem> ConfigSystem { get; set; }
        public virtual DbSet<DonViHanhChinh> DonViHanhChinh { get; set; }
        public virtual DbSet<Data_Commune> Data_Commune { get; set; }
        public virtual DbSet<Data_District> Data_District { get; set; }
        public virtual DbSet<Data_Province> Data_Province { get; set; }
        public virtual DbSet<Data_Target> Data_Target { get; set; }
        public virtual DbSet<DM_BieuMau> DM_BieuMau { get; set; }
        public virtual DbSet<CapDVHC> CapDVHC { get; set; }
        public virtual DbSet<DM_DVCH_BM> DM_DVCH_BM { get; set; }
        public virtual DbSet<KyThongKeKiemKe> KyThongKeKiemKe { get; set; }
        public virtual DbSet<Bieu01KKSL> Bieu01KKSL { get; set; }
        public virtual DbSet<Bieu01KKSL_Vung> Bieu01KKSL_Vung { get; set; }
        public virtual DbSet<Bieu01KKSL_Tinh> Bieu01KKSL_Tinh { get; set; }
        public virtual DbSet<Bieu01KKSL_Huyen> Bieu01KKSL_Huyen { get; set; }
        public virtual DbSet<Bieu01KKSL_Xa> Bieu01KKSL_Xa { get; set; }

        public KiemKeDatDaiDbContext(DbContextOptions<KiemKeDatDaiDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }
    }
}


