using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Azure_DocumentDB_WebApiApp.Repository.Abstract;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Azure_DocumentDB_WebApiApp.Repository
{
    /// <summary>
    /// Handles all operations relating to databases.
    /// </summary>
    public class DatabaseRepository : RepositoryBase
    {
        #region CONSTRUCTORS
        public DatabaseRepository(DocumentClient client) : base(client) { }
        #endregion

        #region DATABASE METHODS
        /// <summary>
        /// Creates a new Database if it does not already exist
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task CreateDatabaseAsync(string dbid)
        {
            dbid.Check("No valid database id provided");

            try
            {
                await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(dbid));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await Client.CreateDatabaseAsync(new Database { Id = dbid });
                }
            }
        }

        /// <summary>
        /// Deletes a Database if it exists
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        public async Task DeleteDatabaseAsync(string dbid)
        {
            dbid.Check("No valid database id provided");

            try
            {
                await Client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(dbid));
            }
            catch (DocumentClientException ex)
            {
                throw ex;
            }
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
    }
}