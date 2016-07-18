using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/db/{dbid}/colls/{colid}/docs")]
    public class DocumentController : BaseController
    {
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(string dbid, string colid, [FromBody] Document document)
        {
            try
            {
                await DocumentClient.CreateDocumentAsync(dbid, colid, document);
                return Created(Request, "Document Created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put(string dbid, string colid, [FromBody] Document document)
        {
            try
            {
                await DocumentClient.ReplaceDocumentAsync(dbid, colid, document);
                return Ok("Document Replaced");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{docid}")]
        public async Task<IHttpActionResult> Delete(string dbid, string colid, string docid)
        {
            try
            {
                await DocumentClient.DeleteDocumentAsync(dbid, colid, docid);
                return Ok("Document Removed");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{docid}")]
        public async Task<IHttpActionResult> Get(string dbid, string colid, string docid)
        {
            try
            {
                return Ok(await DocumentClient.GetDocumentAsync(dbid, colid, docid));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ItemVM>> Get(string dbid, string colid)
        {
            try
            {
                return await DocumentClient.GetDocumentDetailsAsync(dbid, colid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
