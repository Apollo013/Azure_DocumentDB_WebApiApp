using Azure_DocumentDB_WebApiApp.ModelFactories;
using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_DocumentDB_WebApiApp.Repository
{
    /// <summary>
    /// Handles all operations relating to databases.
    /// </summary>
    public class DatabaseRepository : IDisposable
    {
        #region PROPERTIES
        private DocumentClient client;
        protected DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    Task.Run(() => CreateDocumentClient()).Wait();
                }
                return client;
            }
        }

        protected Database Database { get; set; }

        private ClientModelFactory modelFactory;
        protected ClientModelFactory ModelFactory
        {
            get
            {
                if (modelFactory == null)
                {
                    modelFactory = new ClientModelFactory();
                }
                return modelFactory;
            }
        }

        #endregion

        #region CONSTRUCTORS
        public DatabaseRepository()
        {
            Task.Run(() => CreateDocumentClient()).Wait();
        }
        #endregion

        #region DOCUMENT CLIENT

        /// <summary>
        /// Create a DocumentDB Client object
        /// </summary>
        private void CreateDocumentClient()
        {
            string endpointUri = ConfigurationManager.AppSettings["endPointUri"];
            string authKey = ConfigurationManager.AppSettings["authKey"];
            Uri endpoint = new Uri(endpointUri);
            client = new DocumentClient(endpoint, authKey);
        }

        #endregion

        #region DATABASE METHODS

        /// <summary>
        /// Creates a new Database if it does not already exist
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task CreateDatabaseAsync(string dbid)
        {
            // Try to get the database first
            Database = await GetDatabaseAsync(dbid);

            // Create if it does not already exist
            if (Database == null)
            {
                Database = await Client.CreateDatabaseAsync(new Database { Id = dbid });
            }
        }

        /// <summary>
        /// Deletes a Database if it exists
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task DeleteDatabaseAsync(string dbid)
        {
            // Try to get the database first
            Database = await GetDatabaseAsync(dbid);

            // Delete the database if it exists
            if (Database != null)
            {
                await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(Database.Id));
                Database = null;
            }
        }

        /// <summary>
        /// Gets a specified database
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns>A Database object if it exists, null otherwise</returns>
        protected async Task<Database> GetDatabaseAsync(string dbid)
        {
            dbid.Check("No valid database id provided");
            return await Task.Run(() => Client.CreateDatabaseQuery().Where(db => db.Id == dbid).AsEnumerable().FirstOrDefault());
        }

        /// <summary>
        /// Gets the details for a single database
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task<DatabaseVM> GetDatabaseDetailsAsync(string dbid)
        {
            dbid.Check("No valid database id provided");
            var dbase = await Task.Run(() => Client.CreateDatabaseQuery().Where(db => db.Id == dbid).ToList().Select((d) => ModelFactory.Create(d)));
            return dbase.FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of databases for this account
        /// </summary>
        /// <returns>IEnumerable List of DatabaseVM objects</returns>
        public async Task<IEnumerable<DatabaseVM>> GetDatabaseDetailsAsync()
        {
            return await Task.Run(() => Client.CreateDatabaseQuery().ToList().Select((d) => ModelFactory.Create(d)));
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (client == null)
                    {
                        client.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DocDBDatabaseClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}