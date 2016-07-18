using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Azure_DocumentDB_WebApiApp.Models


{
    public class Family
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Parent[] Parents { get; set; }
        public Child[] Children { get; set; }
        public Address Address { get; set; }
        public bool IsRegistered { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
