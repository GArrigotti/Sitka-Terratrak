using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Api.Architecture.Console.Extensions;
using Api.Architecture.ServiceLayer;
using Api.Architecture.DomainLayer.ApiModels;
using System.Threading.Tasks;
using Api.Architecture.Console;

namespace Api
{
    public class Startup
    {
        private static readonly IServiceProvider services;
        private static readonly string path = $@"{Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData)}\Clean Water Services\Terratrak\Logs";

        #region Constructor:

        static Startup() => services = Configure();

        #endregion

        public static async Task Main()
        {
            try
            {
                IDataRetrievalService api = services.GetService<IDataRetrievalService>();
                AuthenticationModel authenticated = await api.GetToken();

                #region Web Request:

                var budget = api.GetBudgetReport(authenticated);
                var site = api.GetSiteReport(authenticated);
                var project = api.GetProjectReport(authenticated);
                var task = api.GetTaskReport(authenticated);
                var workEffort = api.GetWorkEffortReport(authenticated);

                #endregion

                IDatabaseCleanupService cleanup = services.GetService<IDatabaseCleanupService>();

                await Task.WhenAll(
                    api.GetBudgetReport(authenticated),
                    cleanup.PurgeBudget(),

                    api.GetSiteReport(authenticated),
                    cleanup.PurgeSite(),

                    api.GetProjectReport(authenticated),
                    cleanup.PurgeProject(),

                    api.GetTaskReport(authenticated),
                    cleanup.PurgeTask(),

                    api.GetWorkEffortReport(authenticated),
                    cleanup.PurgeWorkEffort()
                 );

                IDatabaseImporterService importer = services.GetService<IDatabaseImporterService>();

                await Task.WhenAll(
                    importer.InsertBudget(await budget),
                    importer.InsertSite(await site),
                    importer.InsertProject(await project),
                    importer.InsertTask(await task),
                    importer.InsertWorkEffort(await workEffort)
                );
            }

            catch (Exception exception)
            {
                exception.Decorate(Log.Logger);
                throw;
            }
        }

        #region Protected:

        public static IServiceProvider Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("application-settings.json", false, true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($@"{path}\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return new ServiceCollection()
                .AddLogging(option => option.AddSerilog())
                .AddSingleton(Log.Logger)
                .AddSingleton(configure => (IConfiguration)configuration)
                .Register()
                .BuildServiceProvider();
        }

        #endregion
    }
}
