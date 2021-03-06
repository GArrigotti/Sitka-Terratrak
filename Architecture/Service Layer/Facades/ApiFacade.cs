using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Architecture.DomainLayer.ApiModels;
using Newtonsoft.Json;
using Serilog;
using System.Net.Http.Headers;
using Api.Architecture.Console;

namespace Api.Architecture.ServiceLayer.Facades
{
    public class ApiFacade : IApiFacade
    {
        private bool disposed = false;
        private readonly ILogger logger;
        private readonly HttpClient client;

        #region Constructor:

        public ApiFacade(HttpClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;
        }

        #endregion

        public async Task<TEntity> TokenAcquisition<TEntity>(string endpoint, FormUrlEncodedContent form)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(endpoint, form);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                TEntity entity = JsonConvert.DeserializeObject<TEntity>(content);

                return entity;
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw new Exception("Unable to acquire token with vendor.");
            }
        }

        public async Task<IEnumerable<TEntity>> Retrieve<TEntity>(string endpoint, AuthenticationModel authentication)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.Access_Token);
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<TEntity> entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(content);

                return entities;
            }

            catch(Exception exception)
            {
                exception.Decorate(logger);
                throw new Exception("An error in model retrieval from endpoint.");
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

    public interface IApiFacade : IDisposable
    {
        Task<TEntity> TokenAcquisition<TEntity>(string endpoint, FormUrlEncodedContent form);

        Task<IEnumerable<TEntity>> Retrieve<TEntity>(string endpoint, AuthenticationModel authentication);
    }

    #endregion
}
