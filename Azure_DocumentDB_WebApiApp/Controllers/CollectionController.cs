using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/db/{dbid}/colls")]
    public class CollectionController : BaseController
    {
        /// <summary>
        /// Creates a document collection
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(string dbid, [FromBody] string colid)
        {
            try
            {
                await CollectionClient.CreateCollectionAsync(dbid, colid);
                return Created(Request, "Collection Created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a document collection
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{colid}")]
        public async Task<IHttpActionResult> Delete(string dbid, string colid)
        {
            try
            {
                await CollectionClient.DeleteCollectionAsync(dbid, colid);
                return Ok("Collection Removed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the details of collection for a specified collection id
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{colid}")]
        public async Task<IHttpActionResult> Get(string dbid, string colid)
        {
            try
            {
                return Ok(await CollectionClient.GetCollectionDetailsAsync(dbid, colid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of all collections for a given database
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CollectionVM>> Get(string dbid)
        {
            try
            {
                return await CollectionClient.GetCollectionsDetailsAsync(dbid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
