using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/db")]
    public class DatabaseController : BaseController
    {
        /// <summary>
        /// Creates a database
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{dbid}")]
        public async Task<IHttpActionResult> Post(string dbid)
        {
            try
            {
                await DataBaseClient.CreateDatabaseAsync(dbid);
                return Created(Request, "Database Created");
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

        /// <summary>
        /// Deletes a database
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{dbid}")]
        public async Task<IHttpActionResult> Delete(string dbid)
        {
            try
            {
                await DataBaseClient.DeleteDatabaseAsync(dbid);
                return Ok("Database Removed");
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

        /// <summary>
        /// Gets a single database
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{dbid}")]
        public async Task<IHttpActionResult> Get(string dbid)
        {
            try
            {
                return Ok(await DataBaseClient.GetDatabaseDetailsAsync(dbid));
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

        /// <summary>
        /// Gets a list of databases
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ItemVM>> Get()
        {
            try
            {
                return await DataBaseClient.GetDatabaseDetailsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
