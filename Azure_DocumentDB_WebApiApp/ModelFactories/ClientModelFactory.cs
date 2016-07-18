using Azure_DocumentDB_WebApiApp.Models.ViewModels;
using Microsoft.Azure.Documents;

namespace Azure_DocumentDB_WebApiApp.ModelFactories
{
    public class ClientModelFactory
    {
        public ItemVM Create(Database model)
        {
            return new ItemVM
            {
                Id = model.Id,
                ResourceId = model.ResourceId
            };
        }

        public ItemVM Create(DocumentCollection model)
        {
            return new ItemVM
            {
                Id = model.Id,
                ResourceId = model.ResourceId
            };
        }

        public ItemVM Create(Document model)
        {
            return new ItemVM
            {
                Id = model.Id,
                ResourceId = model.ResourceId
            };
        }
    }
}