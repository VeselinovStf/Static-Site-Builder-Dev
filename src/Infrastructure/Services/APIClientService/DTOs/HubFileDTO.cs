using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.DTOs
{
    [DataContract]
    public class HubFileDTO
    {
        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "file_path")]
        public string FilePath { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "encoding")]
        public string Encoding { get; set; }
    }
}