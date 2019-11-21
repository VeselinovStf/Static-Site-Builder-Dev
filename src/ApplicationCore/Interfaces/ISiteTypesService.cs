﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteTypesService<T>
    {
        Task<IEnumerable<T>> GetAllTypesAsync();

        Task<bool> ConfirmType(string buildInType);

        Task Create(
            string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository);
    }
}