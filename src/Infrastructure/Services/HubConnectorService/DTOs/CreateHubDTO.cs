using System.Runtime.Serialization;

namespace Infrastructure.Services.HubConnectorService.DTOs
{
    [DataContract]
    public class CreateHubDTO
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}