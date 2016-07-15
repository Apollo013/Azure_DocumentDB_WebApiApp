using Azure_DocumentDB_WebApiApp.Helpers;
using Azure_DocumentDB_WebApiApp.Models;
using Azure_DocumentDB_WebApiApp.Repository;
using System.Net.Http;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers.Abstract
{
    public class BaseController : ApiController
    {
        #region PROPERTIES
        private DatabaseRepository databaseClient;
        protected DatabaseRepository DataBaseClient
        {
            get
            {
                if (databaseClient == null)
                {
                    databaseClient = new DatabaseRepository();
                }
                return databaseClient;
            }
        }

        private CollectionRepository collectionClient;
        protected CollectionRepository CollectionClient
        {
            get
            {
                if(collectionClient == null)
                {
                    collectionClient = new CollectionRepository();
                }
                return collectionClient;
            }
        }

        private DocumentRepository<Family> familyClient;
        protected DocumentRepository<Family> FamilyClient
        {
            get
            {
                if (familyClient == null)
                {
                    familyClient = new DocumentRepository<Family>();
                }
                return familyClient;
            }
        }
        #endregion

        #region ACTION RESULTS

        protected static IHttpActionResult Created(HttpRequestMessage request, string message)
        {
            return new CreatedActionResult(request, message);
        }

        #endregion

    }
}