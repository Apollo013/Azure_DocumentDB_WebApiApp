using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using Azure_DocumentDB_WebApiApp.Models;
using Azure_DocumentDB_WebApiApp.Models.DomainModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/db/{dbid}/colls/{colid}/docs")]
    public class FamilyController : BaseController
    {
        /// <summary>
        /// Creates a new document if it does not already exist
        /// </summary>
        /// <param name="family">The document to create</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure all relevant information has been supplied");
            }


            Type type = Type.GetType("Azure_DocumentDB_WebApiApp.Models.Family");
            if (type == null)
            {
                return BadRequest("Type null");
            }
            else
            {
                return Ok(type.Name);
            }

            try
            {
                //return Ok(family);
                await FamilyClient.CreateDocumentAsync(family);
                return Created(Request, "Document Created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates a document if it already exist
        /// </summary>
        /// <param name="family">The document to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure all relevant information has been supplied");
            }

            try
            {
                await FamilyClient.ReplaceDocumentAsync(family);
                return Ok("Document Saved");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <param name="documentDetails">Object containing details about the document</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromBody] DocumentDM documentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please make sure that all relevent information is provided");
            }

            try
            {
                await FamilyClient.DeleteDocumentAsync(documentDetails);
                return Ok("Document Removed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of documents, with an optional predicate
        /// </summary>
        /// <param name="documentDetails">Object containing details about the document</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{docid}")]
        public async Task<IHttpActionResult> Get(string dbid, string colid, string docid)
        {
            try
            {
                //Expression<Func<Family, bool>> predicate = doc => doc.Id == documentDetails.Id;
                //var families = await FamilyClient.GetAsync(documentDetails, predicate);
                return Ok();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
