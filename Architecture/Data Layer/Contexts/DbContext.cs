using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Architecture.Console;
using Dapper;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Api.Architecture.DataLayer.Contexts
{
    public class DbContext : IDbContext
    {
        private bool disposed = false;
        private readonly ILogger logger;
        private readonly IDbConnection dbConnection;

        #region Constructor:

        public DbContext(IConfiguration configuration, ILogger logger)
        {
            this.logger = logger;

            dbConnection = new SqlConnection(
                configuration.GetConnectionString("Stage"));
        }

        #endregion

        public async Task Delete(string query, DynamicParameters parameters = null)
        {
            try
            {
                int records = parameters != null ?
                    await dbConnection.ExecuteAsync(query, parameters) :
                    await dbConnection.ExecuteAsync(query);

                if (records <= 0)
                    logger.Error("Failed to delete record...");
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        public async Task Insert<TEntity>(string query, DynamicParameters parameters)
        {
            try
            {
                int records = await dbConnection.ExecuteAsync(query, parameters);

                if (records <= 0)
                    logger.Error("Failed to insert records...");
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }

        #region Dispose:

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
                disposed = true;
        }

        public void Dispose() => Dispose(true);

        #endregion
    }

    #region Interface:

    public interface IDbContext : IDisposable
    {
        Task Delete(string query, DynamicParameters parameters = null);

        Task Insert<TEntity>(string query, DynamicParameters parameters);
    }

    #endregion
}
