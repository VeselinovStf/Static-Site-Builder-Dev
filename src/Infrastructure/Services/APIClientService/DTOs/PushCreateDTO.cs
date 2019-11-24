using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.DTOs
{
    [DataContract]
    public class PushCreateDTO
    {
        [DataMember(Name = "branch")]
        public string Branch { get; set; }

        [DataMember(Name = "commit_message")]
        public string CommitMessage { get; set; }

        [DataMember(Name = "actions")]
        public List<HubFileDTO> Actions { get; set; }
    }
}