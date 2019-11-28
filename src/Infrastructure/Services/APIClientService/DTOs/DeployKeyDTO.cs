using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.DTOs
{
    [DataContract]
    public class DeployKeyDTO
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "public_key")]
        public string PublicKey { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }
    }
}