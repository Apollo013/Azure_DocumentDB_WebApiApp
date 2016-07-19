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

        private CollectionRepository _collectionClient;
        protected CollectionRepository CollectionClient
        {
            get
            {
                if (_collectionClient == null)
                {
                    _collectionClient = new CollectionRepository(Client);
                }
                return _collectionClient;
            }
        }

        private DocumentRepository _documentClient;
        protected DocumentRepository DocumentClient
        {
            get
            {
                if (_documentClient == null)
                {
                    _documentClient = new DocumentRepository(Client);
                }
                return _documentClient;
            }
            set { _documentClient = value; }
        }

        private UserRepository _userClient;
        public UserRepository UserClient
        {
            get
            {
                if (_userClient == null)
                {
                    _userClient = new UserRepository(Client);
                }
                return _userClient;
            }
            set { _userClient = value; }
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