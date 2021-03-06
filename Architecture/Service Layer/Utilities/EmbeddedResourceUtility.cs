using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Api.Architecture.Console;
using Serilog;

namespace Api.Architecture.ServiceLayer.Utilities
{
    public class EmbeddedResourceUtility : IEmbeddedResourceUtility
    {
        private readonly ILogger logger;

        #region Constructor:

        public EmbeddedResourceUtility(ILogger logger) => this.logger = logger;

        #endregion

        public async Task<string> Read(string resource)
        {
            try
            {
                if (resource.Contains(" "))
                    resource = String.Join("_", resource.Split(' '));

                using Stream stream = typeof(EmbeddedResourceUtility)
                    .GetTypeInfo()
                    .Assembly
                    .GetManifestResourceStream(resource);

                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw;
            }
        }
    }

    #region Interface:

    public interface IEmbeddedResourceUtility
    {
        Task<string> Read(string resource);
    }

    #endregion
}
