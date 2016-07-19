using Azure_DocumentDB_WebApiApp.Controllers.Abstract;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers
{
    [RoutePrefix("api/db/{dbid}/users")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="userid">user id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{userid}")]
        public async Task<IHttpActionResult> Post(string dbid, string userid)
        {
            try
            {
                await UserClient.CreateUserAsync(dbid, userid);
                return Created(Request, "User Created");
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
        /// Deletes a new user
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="userid">user id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{userid}")]
        public async Task<IHttpActionResult> Delete(string dbid, string userid)
        {
            try
            {
                await UserClient.DeleteUserAsync(dbid, userid);
                return Ok("User Removed");
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
        /// Deletes a new user
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="userid">user id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userid}")]
        public async Task<IHttpActionResult> Get(string dbid, string userid)
        {
            try
            {
                var user = await UserClient.GetUserAsync(dbid, userid);
                return Ok(user);
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
        /// Assigns permission for a user to a collection
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="userid">user id</param>
        /// <param name="colid">collection id</param>
        /// <param name="permissionMode">permission mode</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{userid}/colls/{colid}/{permissionMode:int}")]
        public async Task<IHttpActionResult> Post(string dbid, string userid, string colid, int permissionMode)
        {
            try
            {
                await UserClient.CreatePermissionAsync(dbid, userid, colid, permissionMode);
                return Created(Request, "Permission Granted");
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

    }
}
