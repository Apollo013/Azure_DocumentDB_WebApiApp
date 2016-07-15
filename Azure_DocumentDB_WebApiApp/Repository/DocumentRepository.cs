using Azure_DocumentDB_WebApiApp.Models.DomainModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Azure_DocumentDB_WebApiApp.Repository
{
    /// <summary>
    /// Handles all operations in relation to Document's
    /// </summary>
    /// <typeparam name="T">The Type/schema of document we are dealing with</typeparam>
    public class DocumentRepository<T> : CollectionRepository where T : DocumentDM
    {
        #region CONSTRUCTORS
        public DocumentRepository() : base() { }
        #endregion

        #region DOCUMENT METHODS
        /// <summary>
        /// Creates a new Document
        /// </summary>
        /// <param name="document">The document to save</param>
        /// <returns></returns>
        public async Task CreateDocumentAsync(T document)
        {
            // Check that the document doe's not already exists
            var doc = Task.Factory.StartNew(() => { return GetDocument(document); });

            if (doc.Result == null)
            {
                await Client.CreateDocumentAsync(Collection.SelfLink, document);
            }
            else
            {
                // We could make a call here to update the doc if it pre-exists
                throw new Exception("Document Already Exists");
            }            
        }

        /// <summary>
        /// Repalces a Document
        /// </summary>
        /// <param name="document">The document to repalce</param>
        /// <returns></returns>
        public async Task ReplaceDocumentAsync(T document)
        {
            // Check that the document already exists
            var doc = Task.Factory.StartNew(() => { return GetDocument(document); });

            if (doc.Result != null)
            {
                await Client.ReplaceDocumentAsync(Collection.SelfLink, document);
            }
            else
            {
                // We could make a call here to insert the doc if it does not pre-exist
                throw new Exception("Document Does' Not Exist");
            }
        }

        public async Task DeleteDocumentAsync(DocumentDM documentDetails)
        {
            // Check that the document already exists
            var doc = Task.Factory.StartNew(() => { return GetDocument(documentDetails); });

            if (doc.Result != null)
            {
                await Client.DeleteDocumentAsync(doc.Result.SelfLink);
            }
        }

        /// <summary>
        /// Get a list of T, with an optional predicate
        /// </summary>
        /// <param name="predicate">The linq expression Where clause</param>
        /// <returns>An IEnumerable of T</returns>
        public async Task<IEnumerable<T>> GetAsync(DocumentDM documentDetails, Expression<Func<T, bool>> predicate = null)
        {
            // Make sure the collection exists (Will also ensure that the database exists)
            Task.Run(() => CreateCollectionAsync(documentDetails.Collection)).Wait();

            IDocumentQuery<T> query;

            // Check if a predicate was provided to filter results
            if (predicate != null)
            {
                query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                    .Where(predicate)
                    .AsDocumentQuery();
            }
            else
            {
                query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                    .AsDocumentQuery();
            }

            // Process results
            List<T> results = new List<T>();

            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            // Return
            return results;
        }

        /// <summary>
        /// Gets a document
        /// </summary>
        /// <param name="documentDetails">Details necessary to retrieve the document</param>
        /// <returns>A Document object</returns>
        public Document GetDocument(DocumentDM documentDetails)
        {
            if (documentDetails == null)
            {
                throw new ArgumentNullException("Please specify valid document collection properties");
            }
            else if (String.IsNullOrEmpty(documentDetails.Id))
            {
                throw new ArgumentNullException("Please specify valid name for the document");
            }

            // Make sure the collection exists (Will also ensure that the database exists)
            Task.Run(() => CreateCollectionAsync(documentDetails.Collection)).Wait();

            return Client.CreateDocumentQuery<Document>(
                Collection.SelfLink,
                $"SELECT * FROM c WHERE c.id = '{documentDetails.Id}'")
                .AsEnumerable()
                .FirstOrDefault();
        }
        #endregion
    }
}