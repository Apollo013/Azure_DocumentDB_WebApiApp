using Azure_DocumentDB_WebApiApp.ModelFactories;
using Microsoft.Azure.Documents.Client;
using System;

namespace Azure_DocumentDB_WebApiApp.Repository.Abstract
{
    public class RepositoryBase : IDisposable
    {
        #region PROPERTIES
        private ClientModelFactory modelFactory;
        protected ClientModelFactory ModelFactory
        {
            get
            {
                if (modelFactory == null)
                {
                    modelFactory = new ClientModelFactory();
                }
                return modelFactory;
            }
        }
        protected DocumentClient Client { get; set; }
        #endregion

        #region CONSTRUCTORS
        public RepositoryBase(DocumentClient client)
        {
            Client = client;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Client != null)
                    {
                        Client.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoryBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}