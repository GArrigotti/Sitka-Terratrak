using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Api.Architecture.DataLayer.Contexts
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        #region Constructor:

        public DbContextFactory(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        #endregion

        public IDbContext Create() => new DbContext(configuration, logger);
    }

    #region Interface:

    public interface IDbContextFactory
    {
        IDbContext Create();
    }

    #endregion
}
