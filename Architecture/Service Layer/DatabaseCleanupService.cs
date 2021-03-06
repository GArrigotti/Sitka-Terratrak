using System.Threading.Tasks;
using Api.Architecture.DataLayer.Contexts;
using Api.Architecture.ServiceLayer.Utilities;

namespace Api.Architecture.ServiceLayer
{
    public class DatabaseCleanupService : IDatabaseCleanupService
    {
        private readonly IDbContextFactory factory;
        private readonly IEmbeddedResourceUtility utility;

        #region Constructor:

        public DatabaseCleanupService(IDbContextFactory factory, IEmbeddedResourceUtility utility)
        {
            this.factory = factory;
            this.utility = utility;
        }

        #endregion

        public async Task PurgeBudget()
        {
            using IDbContext context = factory.Create();
            await context.Delete(
                await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Budget.DeleteBudgetQuery.txt"));
        }

        public async Task PurgeSite()
        {
            using IDbContext context = factory.Create();
            await context.Delete(
                await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Site.DeleteSiteQuery.txt"));
        }

        public async Task PurgeProject()
        {
            using IDbContext context = factory.Create();
            await context.Delete(
                await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Project.DeleteProjectQuery.txt"));
        }

        public async Task PurgeTask()
        {
            using IDbContext context = factory.Create();
            await context.Delete(
                await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Task.DeleteTaskQuery.txt"));
        }

        public async Task PurgeWorkEffort()
        {
            using IDbContext context = factory.Create();
            await context.Delete(
                await utility.Read("Api.Architecture.Domain Layer.Database.Queries.Work Effort.DeleteWorkEffortQuery.txt"));
        }
    }

    #region Interface:

    public interface IDatabaseCleanupService
    {
        Task PurgeBudget();

        Task PurgeSite();

        Task PurgeProject();

        Task PurgeTask();

        Task PurgeWorkEffort();
    }

    #endregion
}
