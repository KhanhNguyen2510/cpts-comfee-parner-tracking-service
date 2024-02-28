using Domain.Extensions;
using Infrastructure.Data.EFs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        private static readonly string ConnectString = EnvironmentExtension.GetAppConnectionString();

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Database context
            _ = services.AddDbContext<CPtsDbContext>(options =>
             {
                 options.UseSqlServer(ConnectString,
                     b =>
                     {
                         b.CommandTimeout(1200);
                     }
                 );
                 options.ConfigureWarnings(
                     config =>
                     {
                         config.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning);
                         config.Ignore(RelationalEventId.BoolWithDefaultWarning);
                     }
                 );
             }, ServiceLifetime.Transient);

            return services;
        }
    }
}
