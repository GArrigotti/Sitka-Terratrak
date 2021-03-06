using System;
using Api.Architecture.DataLayer.Contexts;
using Api.Architecture.ServiceLayer;
using Api.Architecture.ServiceLayer.Facades;
using Api.Architecture.ServiceLayer.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Architecture.Console.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            /* Facades: */
            services.AddHttpClient<IApiFacadeFactory, ApiFacadeFactory>();
            services.AddSingleton<IApiFacade, ApiFacade>();

            /* Utilities: */
            services.AddSingleton<IEmbeddedResourceUtility, EmbeddedResourceUtility>();

            /* Service Layer: */
            services.AddSingleton<IDataRetrievalService, DataRetrievalService>();
            services.AddSingleton<IDatabaseCleanupService, DatabaseCleanupService>();
            services.AddSingleton<IDatabaseImporterService, DatabaseImporterService>();

            /* Data Layer: */
            services.AddSingleton<IDbContext, DbContext>();
            services.AddSingleton<IDbContextFactory, DbContextFactory>();

            return services;
        }
    }
}
