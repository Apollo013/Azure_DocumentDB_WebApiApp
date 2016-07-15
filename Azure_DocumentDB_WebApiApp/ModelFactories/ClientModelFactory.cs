using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Microsoft.Azure.Documents;

namespace Azure_DocumentDB_WebApiApp.ModelFactories
{
    public class ClientModelFactory
    {
        public DatabaseVM Create(Database model)
        {
            return new DatabaseVM
            {
                Id = model.Id,
                ResourceId = model.ResourceId
            };
        }

        public CollectionVM Create(DocumentCollection model)
        {
            return new CollectionVM
            {
                Id = model.Id,
                ResourceId = model.ResourceId
            };
        }
    }
}