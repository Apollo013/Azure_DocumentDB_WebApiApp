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
        #region PRIVATE VARS
        private DocumentClient client;
        private ClientModelFactory modelFactory;
        #endregion

        #region PROPERTIES
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
        /// Create a new DocumentDB Client
        /// </summary>
        private void CreateDocumentClient()
        {
            try
            {
                string endpointUri = ConfigurationManager.AppSettings["endPointUri"];
                string authKey = ConfigurationManager.AppSettings["authKey"];
                Uri endpoint = new Uri(endpointUri);
                client = new DocumentClient(endpoint, authKey);
            }
            catch(Exception ex)
            {
                throw;
            }            
        }

        #endregion

        #region DATABASE METHODS

        /// <summary>
        /// Creates a new Database if it does not already exist
        /// </summary>
        /// <param name="databaseId">The Id of the datasbase to create</param>
        /// <returns></returns>
        public async Task CreateDatabaseAsync(string databaseId)
        {
            // Try to get the database first, which is assigned to the datasbe property above
            Task<Database> db = Task<Database>.Factory.StartNew(() => GetDatabase(databaseId));
            Database = db.Result;

            // Create if it does not already exist
            if (Database == null)
            {
                Database = await Client.CreateDatabaseAsync(new Database { Id = databaseId });                
            }
        }

        /// <summary>
        /// Deletes a Database if it exists
        /// </summary>
        /// <param name="databaseId">The Id of the datasbase to delete</param>
        /// <returns></returns>
        public async Task DeleteDatabaseAsync(string databaseId)
        {
            // Try to get the database first
            Task<Database> db = Task<Database>.Factory.StartNew(() => GetDatabase(databaseId));
            Database = db.Result;

            // Delete the database if it exists
            if(Database != null)
            {
                await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(Database.Id));
                Database = null;
            }
        }

        /// <summary>
        /// Gets a specified database
        /// </summary>
        /// <param name="databaseId">The Id of the datasbase to get</param>
        /// <returns>A Database object if it exists, null otherwise</returns>
        private Database GetDatabase(string databaseId)
        {
            if (String.IsNullOrEmpty(databaseId))
            {
                throw new ArgumentNullException("Please specify a database name");
            }
            // Query databases that match the id and return the first
            return Client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
        }

        /// <summary>
        /// Gets a list of databases for this account
        /// </summary>
        /// <returns>IEnumerable List of DatabaseVM objects</returns>
        public IEnumerable<DatabaseVM> GetDatabases()
        {
            // Query all databases and use the model factory to convert them DTO's
            return Client.CreateDatabaseQuery().ToList().Select((d) => ModelFactory.Create(d));
        }

        /// <summary>
        /// Gets all collections for a specified database
        /// </summary>
        /// <param name="databaseId">The name of the database to get the collections for</param>
        /// <returns>IEnumerable List of colections</returns>
        public IEnumerable<CollectionVM> GetDocumentCollections(string databaseId)
        {
            Task<Database> db = Task.Factory.StartNew(() => this.GetDatabase(databaseId));
            return Client.CreateDocumentCollectionQuery(db.Result.SelfLink).ToList().Select(c => ModelFactory.Create(c));
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
                    if(client == null)
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