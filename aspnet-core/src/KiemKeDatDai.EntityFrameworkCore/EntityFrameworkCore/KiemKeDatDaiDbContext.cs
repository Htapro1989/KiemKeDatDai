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
        public virtual DbSet<admin_dvhc> admin_dvhc { get; set; }
        public virtual DbSet<admin_role_group> admin_role_group { get; set; }
        public virtual DbSet<admin_setup> admin_setup { get; set; }
        public virtual DbSet<admin_token> admin_token { get; set; }
        public virtual DbSet<admin_user> admin_user { get; set; }
        public virtual DbSet<category_role> category_role { get; set; }
        public virtual DbSet<category_dvhc> category_dvhc { get; set; }
        public virtual DbSet<data_commune> data_commune { get; set; }
        public virtual DbSet<data_district> data_district { get; set; }
        public virtual DbSet<data_province> data_province { get; set; }
        public virtual DbSet<data_target> data_target { get; set; }

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


