using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Azure_DocumentDB_WebApiApp.Repository.Abstract;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Azure_DocumentDB_WebApiApp.Repository
{
    /// <summary>
    /// Handles all operations in relation to DocumentCollection's
    /// </summary>
    public class CollectionRepository : RepositoryBase
    {
        #region CONSTRUCTORS
        public CollectionRepository(DocumentClient client) : base(client) { }
        #endregion

        #region COLLECTION METHODS

        /// <summary>
        /// Creates a new collection if it does not already exist
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        public async Task CreateCollectionAsync(string dbid, string colid)
        {
            // Check parameters
            dbid.Check("No valid database id provided");
            colid.Check("No valid collection id provided");

            try
            {
                await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await Client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(dbid),
                        new DocumentCollection { Id = colid },
                        new RequestOptions { OfferThroughput = 400 });
                }
            }
        }

        /// <summary>
        /// Deletes a collection if it already exists
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        public async Task DeleteCollectionAsync(string dbid, string colid)
        {
            // Check parameters
            dbid.Check("No valid database id provided");
            colid.Check("No valid collection id provided");

            try
            {
                await Client.DeleteDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid));
            }
            catch (DocumentClientException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the details of collection for a specified collection id
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        public async Task<CollectionVM> GetCollectionDetailsAsync(string dbid, string colid)
        {
            // Check parameters
            dbid.Check("No valid database id provided");
            colid.Check("No valid collection id provided");

            try
            {
                var col = await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid));
                return ModelFactory.Create(col);
            }
            catch (DocumentClientException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a List of collections within a database by calling the ReadFeed (scan) API.
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task<IEnumerable<CollectionVM>> GetCollectionDetailsAsync(string dbid)
        {
            // Check parameters
            dbid.Check("No valid database id provided");

            List<CollectionVM> colls = new List<CollectionVM>();
            foreach (var coll in await Client.ReadDocumentCollectionFeedAsync(UriFactory.CreateDatabaseUri(dbid)))
            {
                colls.Add(ModelFactory.Create(coll));
            }

            return colls;
        }

        #endregion
    }
}