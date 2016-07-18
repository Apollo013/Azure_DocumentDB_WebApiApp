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
    /// Handles all operations in relation to Document's
    /// </summary>
    /// <typeparam name="T">The Type/schema of document we are dealing with</typeparam>
    public class DocumentRepository : RepositoryBase
    {
        #region CONSTRUCTORS
        public DocumentRepository(DocumentClient client) : base(client) { }
        #endregion

        #region DOCUMENT METHODS
        /// <summary>
        /// Creates a new Document
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <param name="document">The document to persist</param>
        /// <returns></returns>
        public async Task CreateDocumentAsync(string dbid, string colid, Document document)
        {
            Check(dbid, colid, document.Id);

            try
            {
                await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri(dbid, colid, document.Id));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid), document);
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Replaces a document
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <param name="document">The document to replace</param>
        /// <returns></returns>
        public async Task ReplaceDocumentAsync(string dbid, string colid, Document document)
        {
            Check(dbid, colid, document.Id);
            await Client.ReplaceDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid), document);
        }

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <param name="docid">The id of the document to delete</param>
        /// <returns></returns>
        public async Task DeleteDocumentAsync(string dbid, string colid, string docid)
        {
            Check(dbid, colid, docid);

            try
            {
                await Client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(dbid, colid, docid));
            }
            catch (DocumentClientException)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <param name="docid">The id of the document to get</param>
        /// <returns></returns>
        public async Task<Document> GetDocumentAsync(string dbid, string colid, string docid)
        {
            Check(dbid, colid, docid);
            try
            {
                return await Client.ReadDocumentAsync(UriFactory.CreateDocumentUri(dbid, colid, docid));
            }
            catch (DocumentClientException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a list of documents (limited to ten docs)
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemVM>> GetDocumentDetailsAsync(string dbid, string colid)
        {
            List<ItemVM> list = new List<ItemVM>();

            foreach (var doc in await Client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid), new FeedOptions { MaxItemCount = 10 }))
            {
                list.Add(ModelFactory.Create(doc));
            }
            return list;
        }

        /// <summary>
        /// Check parameters prior to processing
        /// </summary>
        /// <param name="dbid"></param>
        /// <param name="colid"></param>
        /// <param name="docid"></param>
        private void Check(string dbid, string colid, string docid)
        {
            // Check parameters
            dbid.Check("No valid database id provided");
            colid.Check("No valid collection id provided");
            docid.Check("No valid document id provided");
        }

        #endregion
    }
}