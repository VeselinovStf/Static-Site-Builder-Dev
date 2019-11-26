﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAPIRepoClientService<T>
    {
        Task<string> CreateHubAsync(string name, string accesTokken);

        Task<bool> PushDataToHub(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents);

        Task<bool> AddVariables(string hubId, string accesToken, string hostingId, string hostingAccesToken);
    }
}