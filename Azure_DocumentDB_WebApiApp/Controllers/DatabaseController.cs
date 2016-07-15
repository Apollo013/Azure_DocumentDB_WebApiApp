using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/database")]
    public class DatabaseController : BaseController
    {
        /// <summary>
        /// Creates a database
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{databaseId}")]
        public async Task<IHttpActionResult> Post(string databaseId)
        {
            if (String.IsNullOrEmpty(databaseId))
            {
                return BadRequest("No valid database name provided");
            }

            try
            {
                await DataBaseClient.CreateDatabaseAsync(databaseId);
                return Created(Request, "Database Created");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a database
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{databaseId}")]
        public async Task<IHttpActionResult> Delete(string databaseId)
        {
            if (String.IsNullOrEmpty(databaseId))
            {
                return BadRequest("No valid database name provided");
            }

            try
            {
                await DataBaseClient.DeleteDatabaseAsync(databaseId);
                return Ok("Database Removed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of databases
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var dbList = Task.Factory.StartNew(() => DataBaseClient.GetDatabases());
                return Ok(dbList.Result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of collections for a given database
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{databaseName}/collections")]
        public IHttpActionResult Get(string databaseId)
        {
            try
            {
                var dbList = Task.Factory.StartNew(() => DataBaseClient.GetDocumentCollections(databaseId));
                return Ok(dbList.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
