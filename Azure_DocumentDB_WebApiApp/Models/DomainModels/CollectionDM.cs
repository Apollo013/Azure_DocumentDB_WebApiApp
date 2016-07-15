using System.ComponentModel.DataAnnotations;

namespace Azure_DocumentDB_WebApiApp.Models.DomainModels
{
    /// <summary>
    /// Contains the bare necessities to create and delete a DocumentCollection.
    /// This is also used as a validation model for when the client sends a request to the server.
    /// </summary>
    public class CollectionDM
    {
        [Required]
        public string CollectionId { get; set; }
        [Required]
        public string DatabaseId { get; set; }
    }
}