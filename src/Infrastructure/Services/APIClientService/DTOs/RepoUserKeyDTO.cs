using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.DTOs
{
    [DataContract]
    public class RepoUserKeyDTO
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "Title")]
        public string Title { get; set; }
    }
}