using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Api.Architecture.ServiceLayer.Facades
{
    public class ApiFacadeFactory : IApiFacadeFactory
    {
        private readonly ILogger logger;
        private readonly HttpClient client;

        #region Constructor:

        public ApiFacadeFactory(HttpClient client, IConfiguration configuration, ILogger logger)
        {
            this.client = client;
            this.logger = logger;
        }

        #endregion

        public IApiFacade Connect() => new ApiFacade(client, logger);
    }

    #region Interface:

    public interface IApiFacadeFactory
    {
        IApiFacade Connect();
    }

    #endregion
}
