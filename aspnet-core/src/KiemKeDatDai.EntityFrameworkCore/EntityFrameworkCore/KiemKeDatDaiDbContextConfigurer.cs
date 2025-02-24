using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace KiemKeDatDai.EntityFrameworkCore;

public static class KiemKeDatDaiDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<KiemKeDatDaiDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<KiemKeDatDaiDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
