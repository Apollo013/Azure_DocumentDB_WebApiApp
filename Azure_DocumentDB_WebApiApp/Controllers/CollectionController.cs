using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using Azure_DocumentDB_WebApiApp.Models.DomainModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/collection")]
    public class CollectionController : BaseController
    {

        /// <summary>
        /// Creates a document collection
        /// </summary>
        /// <param name="collection">CollectionDM object containing the collection & database names</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CollectionDM collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure that all relevent information is provided");
            }

            try
            {
                await CollectionClient.CreateCollectionAsync(collection);
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
        /// <param name="collection">CollectionDM object containing the collection & database names</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromBody] CollectionDM collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure that all relevent information is provided");
            }

            try
            {
                await CollectionClient.DeleteCollectionAsync(collection);
                return Ok("Collection Removed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class DocumentBase
    {
    }
}
