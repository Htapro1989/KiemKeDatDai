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
        public virtual DbSet<Bieu02KKSL> Bieu02KKSL { get; set; }
        public virtual DbSet<Bieu02KKSL_Vung> Bieu02KKSL_Vung { get; set; }
        public virtual DbSet<Bieu02KKSL_Tinh> Bieu02KKSL_Tinh { get; set; }
        public virtual DbSet<Bieu02KKSL_Huyen> Bieu02KKSL_Huyen { get; set; }
        public virtual DbSet<Bieu02KKSL_Xa> Bieu02KKSL_Xa { get; set; }
        public virtual DbSet<Bieu01aKKNLT> Bieu01aKKNLT { get; set; }
        public virtual DbSet<Bieu01aKKNLT_Vung> Bieu01aKKNLT_Vung { get; set; }
        public virtual DbSet<Bieu01aKKNLT_Tinh> Bieu01aKKNLT_Tinh { get; set; }
        public virtual DbSet<Bieu01aKKNLT_Huyen> Bieu01aKKNLT_Huyen { get; set; }
        public virtual DbSet<Bieu01aKKNLT_Xa> Bieu01aKKNLT_Xa { get; set; }
        public virtual DbSet<Bieu01bKKNLT> Bieu01bKKNLT { get; set; }
        public virtual DbSet<Bieu01bKKNLT_Vung> Bieu01bKKNLT_Vung { get; set; }
        public virtual DbSet<Bieu01bKKNLT_Tinh> Bieu01bKKNLT_Tinh { get; set; }
        public virtual DbSet<Bieu01bKKNLT_Huyen> Bieu01bKKNLT_Huyen { get; set; }
        public virtual DbSet<Bieu01bKKNLT_Xa> Bieu01bKKNLT_Xa { get; set; }
        public virtual DbSet<Bieu01cKKNLT> Bieu01cKKNLT { get; set; }
        public virtual DbSet<Bieu01cKKNLT_Vung> Bieu01cKKNLT_Vung { get; set; }
        public virtual DbSet<Bieu01cKKNLT_Tinh> Bieu01cKKNLT_Tinh { get; set; }
        public virtual DbSet<Bieu01cKKNLT_Huyen> Bieu01cKKNLT_Huyen { get; set; }
        public virtual DbSet<Bieu01cKKNLT_Xa> Bieu01cKKNLT_Xa { get; set; }
        public virtual DbSet<Bieu01TKKK> Bieu01TKKK { get; set; }
        public virtual DbSet<Bieu01TKKK_Vung> Bieu01TKKK_Vung { get; set; }
        public virtual DbSet<Bieu01TKKK_Tinh> Bieu01TKKK_Tinh { get; set; }
        public virtual DbSet<Bieu01TKKK_Huyen> Bieu01TKKK_Huyen { get; set; }
        public virtual DbSet<Bieu01TKKK_Xa> Bieu01TKKK_Xa { get; set; }
        public virtual DbSet<DM_LoaiDat> DM_LoaiDat { get; set; }

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


