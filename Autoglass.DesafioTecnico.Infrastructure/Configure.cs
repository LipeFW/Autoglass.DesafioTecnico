using Autoglass.DesafioTecnico.Infrastructure.Context;
using Autoglass.DesafioTecnico.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Autoglass.DesafioTecnico.Infrastructure
{
    public static class Configure
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRepository();
            services.ConfigureContext(configuration);

            return services;
        }

        private static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<ProdutoRepository>();

            return services;
        }

        private static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Connection");

            services.AddDbContext<MainContext>(opt => opt.UseSqlServer(connection));

            return services;
        }
    }
}
