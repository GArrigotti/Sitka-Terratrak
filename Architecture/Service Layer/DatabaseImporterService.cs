using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Threading.Tasks;
using Api.Architecture.DataLayer.Contexts;
using Api.Architecture.DomainLayer.ApiModels.Reports;
using Api.Architecture.ServiceLayer.Utilities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Api.Architecture.ServiceLayer
{
    public class DatabaseImporterService : IDatabaseImporterService
    {
        private readonly IDbContextFactory factory;
        private readonly IEmbeddedResourceUtility utility;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        #region Constructor:

        public DatabaseImporterService(IDbContextFactory factory, IEmbeddedResourceUtility utility, IConfiguration configuration, ILogger logger)
        {
            this.factory = factory;
            this.utility = utility;
            this.configuration = configuration;
            this.logger = logger;
        }

        #endregion

        public async Task InsertBudget(IEnumerable<BudgetReportModel> budgets)
        {
            using IDbContext context = factory.Create();
            foreach (BudgetReportModel item in budgets)
            {
                var parameters = BuildParameters(item);
                await context.Insert<BudgetReportModel>(
                    await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Budget.InsertBudgetQuery.txt"),
                    parameters);
            }
        }

        public async Task InsertSite(IEnumerable<SiteReportModel> sites)
        {
            using IDbContext context = factory.Create();
            foreach (SiteReportModel item in sites)
            {
                var parameters = new DynamicParameters();
                parameters.Add("SiteId", item.SiteId);
                parameters.Add("TenantSiteIdentifier", item.TenantSiteIdentifier);
                parameters.Add("Name", item.Name);
                parameters.Add("Acres", item?.Acres);
                parameters.Add("SiteOwners", item.SiteOwner == null ? String.Empty : String.Join(",", item.SiteOwner));

                await context.Insert<SiteReportModel>(
                    await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Site.InsertSiteQuery.txt"),
                    parameters);
            }
        }

        public async Task InsertProject(IEnumerable<ProjectReportModel> projects)
        {
            using IDbContext context = factory.Create();
            foreach (ProjectReportModel item in projects)
            {
                var parameters = new DynamicParameters();
                parameters.Add("ProjectId", item.ProjectId);
                parameters.Add("SiteId", item.SiteId);
                parameters.Add("Name", item.Name);
                parameters.Add("TenantProjectIdentifier", item.TenantProjectIdentifier);
                parameters.Add("TenantSiteIdentifier", item.TenantSiteIdentifier);
                parameters.Add("ProgramLead", item.ProgramLead);
                parameters.Add("PeerReviewer", item.PeerReviewer);
                parameters.Add("PrimaryOrganization", item.PrimaryOrganization);
                parameters.Add("PrimaryOrganizationContact", item.PrimaryOrganizationContact);
                parameters.Add("OtherContacts", item.OtherContacts == null ? String.Empty : String.Join(",", item.OtherContacts));
                parameters.Add("ProjectStatus", item.ProjectStatus);
                parameters.Add("ProjectType", item.ProjectType);
                parameters.Add("StartDate", item.StartDate);
                parameters.Add("EndDate", item.EndDate);
                parameters.Add("Description", item?.Description);
                parameters.Add("Acres", item?.Acres);

                await context.Insert<ProjectReportModel>(
                    await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Project.InsertProjectQuery.txt"),
                    parameters);
            }
        }

        public async Task InsertTask(IEnumerable<TaskReportModel> tasks)
        {
            using IDbContext context = factory.Create();
            foreach (TaskReportModel item in tasks)
            {
                var parameters = BuildParameters(item);
                await context.Insert<TaskReportModel>(
                    await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Task.InsertTaskQuery.txt"),
                    parameters);
            }
        }

        public async Task InsertWorkEffort(IEnumerable<WorkEffortModel> workEfforts)
        {
            using IDbContext context = factory.Create();
            foreach (WorkEffortModel item in workEfforts)
            {
                var parameters = BuildParameters(item);
                await context.Insert<WorkEffortModel>(
                    await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Work Effort.InsertWorkEffortQuery.txt"),
                    parameters);
            }
        }

        #region Private:

        private DynamicParameters BuildParameters<TEntity>(TEntity entity)
        {
            var parameters = new DynamicParameters();
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (Attribute.IsDefined(property, typeof(NotMappedAttribute)))
                    continue;

                parameters.Add(property.Name, property.GetValue(entity, null));
            }

            return parameters;
        }

        #endregion
    }

    #region Interface:

    public interface IDatabaseImporterService
    {
        Task InsertBudget(IEnumerable<BudgetReportModel> budgets);

        Task InsertSite(IEnumerable<SiteReportModel> sites);

        Task InsertProject(IEnumerable<ProjectReportModel> projects);

        Task InsertTask(IEnumerable<TaskReportModel> tasks);

        Task InsertWorkEffort(IEnumerable<WorkEffortModel> workEfforts);
    }

    #endregion
}
