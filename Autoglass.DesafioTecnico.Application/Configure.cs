using Autoglass.DesafioTecnico.Application.AutoMapper;
using Autoglass.DesafioTecnico.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Autoglass.DesafioTecnico.Application
{
    public static class Configure
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.ConfigureAutoMapper();
            services.ConfigureService();

            return services;

        }

        private static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ProdutoService>();

            return services;
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProdutoAutoMapper));

            return services;
        }
    }
}
