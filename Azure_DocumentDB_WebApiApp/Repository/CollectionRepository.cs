using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_DocumentDB_WebApiApp.Repository
{
    /// <summary>
    /// Handles all operations in relation to DocumentCollection's
    /// </summary>
    public class CollectionRepository : DatabaseRepository
    {
        #region PROPERTIES
        protected DocumentCollection Collection { get; set; }
        #endregion

        #region CONSTRUCTORS
        public CollectionRepository() : base() { }
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
            // Check if document collection already exists
            Collection = await GetCollectionAsync(dbid, colid);

            if (Collection == null)
            {
                // Setup collection with custom index policy (lazy indexing)
                var collectionDefinition = new DocumentCollection();
                collectionDefinition.Id = colid;
                collectionDefinition.IndexingPolicy.IndexingMode = IndexingMode.Lazy;
                var requestOptions = new RequestOptions { OfferThroughput = 400 };

                // Create the collection
                Collection = await Client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(dbid), collectionDefinition, requestOptions);
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
            // Check if document collection already exists
            Collection = await GetCollectionAsync(dbid, colid);

            // Delete if it exists
            if (Collection != null)
            {
                await Client.DeleteDocumentCollectionAsync(Collection.SelfLink);
            }

            Collection = null;
        }

        /// <summary>
        /// Gets a document collection object for a specified collection id
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        private async Task<DocumentCollection> GetCollectionAsync(string dbid, string colid)
        {
            dbid.Check("No valid database id provided");
            colid.Check("No valid collection id provided");

            return await Task.Run(() => Client.CreateDocumentCollectionQuery(UriFactory.CreateDatabaseUri(dbid)).Where(c => c.Id == colid).ToArray().FirstOrDefault());
            //return Client.CreateDocumentCollectionQuery(UriFactory.CreateDatabaseUri(dbid)).Where(c => c.Id == colid).ToArray().FirstOrDefault();
            //return await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid));
        }

        /// <summary>
        /// Gets the details of collection for a specified collection id
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        public async Task<CollectionVM> GetCollectionDetailsAsync(string dbid, string colid)
        {
            var col = await GetCollectionAsync(dbid, colid);
            return ModelFactory.Create(col);
        }

        /// <summary>
        /// Gets a List of collections within a database by calling the ReadFeed (scan) API.
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task<IEnumerable<CollectionVM>> GetCollectionsDetailsAsync(string dbid)
        {
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