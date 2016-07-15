using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Azure_DocumentDB_WebApiApp.Models.DomainModels
{
    /// <summary>
    /// Contains the bare necessities to create and delete a documents.
    /// This is also used as a validation model for when the client sends a request to the server.
    /// </summary>
    public class DocumentDM
    {
        public CollectionDM Collection { get; set; }
        [JsonProperty(PropertyName = "id")]
        [Required]
        public string Id { get; set; }
    }
}