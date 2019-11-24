using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.DTOs
{
    [DataContract]
    public class HubProjectDTO
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
    }
}