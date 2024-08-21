using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using AdminGestaoApi.Infra.Data.Repository;
using AdminGestaoApi.Domain.Services.Usuario;
using Microsoft.Extensions.DependencyInjection;
using AdminGestaoApi.Infra.CrossCutting.Logging;
using AdminGestaoApi.Infra.Data.Repository.Usuario;
using AdminGestaoApi.Infra.Data.Repository.Interfaces;
using AdminGestaoApi.Domain.Services.Interfaces.Usuario;
using AdminGestaoApi.ApplicationService.Services.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;
using AdminGestaoApi.Infra.CrossCutting.IoC.Profiles.Usuario;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Usuario;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Data;

namespace AdminGestaoApi.Infra.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region ApplicationService
            services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
            #endregion

            #region Domain
            services.AddScoped<IUsuarioService, UsuarioService>();
            #endregion

            #region Infra
            services.AddScoped<ILoggingProvider, LoggingProvider>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMySqlRepository, MySqlRepository>();
            services.AddScoped<IMySqlConnection, MySqlRepository>();
            services.AddSingleton<IConexaoMySql, ConexaoMySql>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsuarioProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

        }
    }
}
