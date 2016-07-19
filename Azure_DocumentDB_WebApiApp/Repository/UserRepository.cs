using Azure_DocumentDB_WebApiApp.Repository.Abstract;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Threading.Tasks;

namespace Azure_DocumentDB_WebApiApp.Repository
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository(DocumentClient client) : base(client) { }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="userid"> user id</param>
        /// <returns></returns>
        public async Task CreateUserAsync(string dbid, string userid)
        {
            try
            {
                Database db = await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(dbid));
                await Client.CreateUserAsync(db.UsersLink, new User { Id = userid });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="userid"> user id</param>
        /// <returns></returns>
        public async Task DeleteUserAsync(string dbid, string userid)
        {
            try
            {
                Database db = await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(dbid));
                await Client.DeleteUserAsync(UriFactory.CreateUserUri(dbid, userid));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates /assigns permission for a user
        /// </summary>
        /// <param name="dbid">database id</param>
        /// <param name="colid">collection id</param>
        /// <param name="userid"> user id</param>
        /// <param name="permissionMode"></param>
        /// <returns></returns>
        public async Task<Permission> CreatePermissionAsync(string dbid, string userid, string colid, int permissionMode)
        {
            DocumentCollection dc;
            User user;
            ResourceResponse<Permission> response;
            Permission permission;

            try
            {
                dc = await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(dbid, colid));
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Cannot find collection");
            }


            try
            {
                user = await Client.ReadUserAsync(UriFactory.CreateUserUri(dbid, userid));
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Cannot find user");
            }


            try
            {
                permission = new Permission
                {
                    Id = Guid.NewGuid().ToString("N"),
                    PermissionMode = (PermissionMode)permissionMode,
                    ResourceLink = dc.SelfLink
                };
            }
            catch (Exception)
            {
                throw new ArgumentNullException("error creating permission");
            }


            try
            {
                response = await ExecuteWithRetries<ResourceResponse<Permission>>(() => Client.CreatePermissionAsync(user.SelfLink, permission));
            }
            catch (Exception)
            {
                throw new ArgumentNullException("error getting response");
            }


            return response.Resource;
        }

        /// <summary>
        /// Executes a function with retries on throttle.
        /// </summary>
        /// <typeparam name="V">The type of return value from the execution</typeparam>
        /// <param name="function">The function to execute</param>
        /// <returns></returns>
        public async Task<V> ExecuteWithRetries<V>(Func<Task<V>> function)
        {
            TimeSpan sleepTime = TimeSpan.Zero;

            while (true)
            {
                try
                {
                    return await function();
                }
                catch (DocumentClientException ex)
                {
                    var statusCode = (int)ex.StatusCode;
                    if (statusCode != 429 && statusCode != 449)
                    {
                        throw;
                    }

                    sleepTime = ex.RetryAfter;
                }
                catch (AggregateException ae)
                {
                    if (!(ae.InnerException is DocumentClientException))
                    {
                        throw;
                    }

                    DocumentClientException de = (DocumentClientException)ae.InnerException;
                    if ((int)de.StatusCode != 429)
                    {
                        throw;
                    }

                    sleepTime = de.RetryAfter;
                    if (sleepTime < TimeSpan.FromMilliseconds(10))
                    {
                        sleepTime = TimeSpan.FromMilliseconds(10);
                    }
                }

                await Task.Delay(sleepTime);
            }
        }
    }
}