using Azure_DocumentDB_WebApiApp.Helpers.ActionResults;
using Azure_DocumentDB_WebApiApp.Repository;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;

namespace Azure_DocumentDB_WebApiApp.Controllers.Abstract
{
    public class BaseController : ApiController
    {
        #region PROPERTIES

        private DocumentClient _client;
        public DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    string endpointUri = ConfigurationManager.AppSettings["endPointUri"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    _client = new DocumentClient(new Uri(endpointUri), authKey);
                }
                return _client;
            }
            set { _client = value; }
        }

        private DatabaseRepository _databaseClient;
        protected DatabaseRepository DataBaseClient
        {
            get
            {
                if (_databaseClient == null)
                {
                    _databaseClient = new DatabaseRepository(Client);
                }
                return _databaseClient;
            }
        }

        private CollectionRepository collectionClient;
        protected CollectionRepository CollectionClient
        {
            get
            {
                if (collectionClient == null)
                {
                    collectionClient = new CollectionRepository(Client);
                }
                return collectionClient;
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