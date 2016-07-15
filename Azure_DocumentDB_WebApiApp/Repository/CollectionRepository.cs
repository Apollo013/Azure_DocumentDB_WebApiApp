using Azure_DocumentDB_WebApiApp.Models.DomainModels;
using Microsoft.Azure.Documents;
using System;
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
        public CollectionRepository() : base() {}
        #endregion

        #region COLLECTION METHODS

        /// <summary>
        /// Creates a new document collection if it does not already exist
        /// </summary>
        /// <param name="collectionName">The name for the document collection</param>
        /// <returns>true if collection was created or already exists, otherwise false</returns>
        public async Task CreateCollectionAsync(CollectionDM collectionModel)
        {
            // Check if document collection already exists
            Task<DocumentCollection> dc = Task<DocumentCollection>.Factory.StartNew(() => GetDocumentCollection(collectionModel));
            Collection = dc.Result;

            // Create it if it does not already exist
            if (Collection == null)
            {
                Collection = await Client.CreateDocumentCollectionAsync(Database.SelfLink, new DocumentCollection { Id = collectionModel.CollectionId });
            }
        }

        /// <summary>
        /// Creates a new document collection if it does not already exist
        /// </summary>
        /// <param name="collectionName">The name for the document collection</param>
        /// <returns>true if collection was created or already exists, otherwise false</returns>
        public async Task DeleteCollectionAsync(CollectionDM collectionModel)
        {
            // Check if document collection already exists
            Task<DocumentCollection> dc = Task<DocumentCollection>.Factory.StartNew(() => GetDocumentCollection(collectionModel));
            Collection = dc.Result;

            // Delete if it exists
            if (Collection != null)
            {
                await Client.DeleteDocumentCollectionAsync(Collection.SelfLink);
                Collection = null;
            }
        }

        /// <summary>
        /// Gets a document collection for a specified collection id and database id
        /// </summary>
        /// <param name="collectionModel">The model class containing the database & collection name</param>
        /// <returns>DocumentCollection object</returns>
        private DocumentCollection GetDocumentCollection(CollectionDM collectionModel)
        {
            if (collectionModel == null)
            {
                throw new ArgumentNullException("Please specify valid document collection properties");
            }
            else if (String.IsNullOrEmpty(collectionModel.CollectionId))
            {
                throw new ArgumentNullException("Please specify valid name for the collection");
            }
            else if (String.IsNullOrEmpty(collectionModel.DatabaseId))
            {
                throw new ArgumentNullException("Please specify valid datasbase name for the collection");
            }

            // Make sure the database exists
            Task.Run(() => CreateDatabaseAsync(collectionModel.DatabaseId)).Wait();
           
            // Get the document collection
            return Client.CreateDocumentCollectionQuery(Database.SelfLink).Where(c => c.Id == collectionModel.CollectionId).ToArray().FirstOrDefault();
        }

        #endregion
    }
}