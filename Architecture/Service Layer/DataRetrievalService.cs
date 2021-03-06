using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Architecture.DomainLayer.ApiModels;
using Api.Architecture.ServiceLayer.Facades;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net.Http;
using Api.Architecture.DomainLayer.ApiModels.Reports;
using Api.Architecture.Console;

namespace Api.Architecture.ServiceLayer
{
    public class DataRetrievalService : IDataRetrievalService
    {
        private readonly IApiFacadeFactory factory;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        #region Constructor:

        public DataRetrievalService(IApiFacadeFactory factory, IConfiguration configuration, ILogger logger)
        {
            this.factory = factory;
            this.configuration = configuration;
            this.logger = logger;
        }

        #endregion

        public async Task<AuthenticationModel> GetToken()
        {
            try
            {
                using IApiFacade api = factory.Connect();
                return await api.TokenAcquisition<AuthenticationModel>(
                    configuration.GetSection("Endpoints")["Login"],
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>()
                        {
                        { "grant_type", "client_credentials" },
                        { "client_id", configuration.GetSection("Credentials")["Id"] },
                        { "client_secret", configuration.GetSection("Credentials")["Secret"] },
                        { "scope", "keystone" }
                        }));
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        public async Task<IEnumerable<BudgetReportModel>> GetBudgetReport(AuthenticationModel authentication)
        {
            try
            {
                using IApiFacade api = factory.Connect();
                return await api.Retrieve<BudgetReportModel>(
                    configuration.GetSection("Endpoints:Reports")["Budget"],
                    authentication);
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        public async Task<IEnumerable<SiteReportModel>> GetSiteReport(AuthenticationModel authentication)
        {
            try
            {
                using IApiFacade api = factory.Connect();
                return await api.Retrieve<SiteReportModel>(
                    configuration.GetSection("Endpoints:Reports")["Site"],
                    authentication);
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        public async Task<IEnumerable<ProjectReportModel>> GetProjectReport(AuthenticationModel authentication)
        {
            try
            {
                using IApiFacade api = factory.Connect();
                return await api.Retrieve<ProjectReportModel>(
                    configuration.GetSection("Endpoints:Reports")["Project"],
                    authentication);
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        public async Task<IEnumerable<WorkEffortModel>> GetWorkEffortReport(AuthenticationModel authentication)
        {
            try
            {
                using IApiFacade api = factory.Connect();
                return await api.Retrieve<WorkEffortModel>(
                    configuration.GetSection("Endpoints:Reports")["Work-Effort"],
                    authentication);
            }

            catch (Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        public async Task<IEnumerable<TaskReportModel>> GetTaskReport(AuthenticationModel authentication)
        {
            try
            {
                using IApiFacade api = factory.Connect();
                return await api.Retrieve<TaskReportModel>(
                    configuration.GetSection("Endpoints:Reports")["Task"],
                    authentication);
            }

            catch (Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

    }

    #region Interface:

    public interface IDataRetrievalService
    {
        Task<AuthenticationModel> GetToken();

        Task<IEnumerable<BudgetReportModel>> GetBudgetReport(AuthenticationModel authentication);

        Task<IEnumerable<SiteReportModel>> GetSiteReport(AuthenticationModel authentication);

        Task<IEnumerable<ProjectReportModel>> GetProjectReport(AuthenticationModel authentication);

        Task<IEnumerable<WorkEffortModel>> GetWorkEffortReport(AuthenticationModel authentication);

        Task<IEnumerable<TaskReportModel>> GetTaskReport(AuthenticationModel authentication);
    }

    #endregion
}