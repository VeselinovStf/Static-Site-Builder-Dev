﻿using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppClientWidgetService : IAppClientWidgetService
    {
        private readonly IAsyncRepository<ClientWidjet> clientWidgetRepository;

        public AppClientWidgetService(
            IAsyncRepository<ClientWidjet> clientWidgetRepository)
            
        {
            this.clientWidgetRepository = clientWidgetRepository ?? throw new ArgumentNullException(nameof(clientWidgetRepository));
        }

        public async Task<ClientWidjet> GetAllAsync(string clientId)
        {
            var specification = new ClientWidgetsWithWidgetsSpecification(clientId);

            return this.clientWidgetRepository.GetSingleBySpec(specification);
        }
    }
}
