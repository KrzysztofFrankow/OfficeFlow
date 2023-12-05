using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeFlow.Domain.Interfaces;
using OfficeFlow.Infrastructure.Persistence;
using OfficeFlow.Infrastructure.Repositories;
using OfficeFlow.Infrastructure.Seeders;

namespace OfficeFlow.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OfficeFlowDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("OfficeFlow")));

            services.AddScoped<UserSeeder>();

            services.AddScoped<IOfficeFlowRepository, OfficeFlowRepository>();
        }
    }
}
