﻿using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.DTOs
{
    [DataContract]
    public class DeploySiteDTO
    {
        [DataMember(Name = "repo")]
        public DeployRepoDTO Repo { get; set; }
    }
}