using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.SiteTypes
{
    public class StoreTypeSiteFactory : SiteTypesFactory
    {
        private readonly IAppProjectsService<Project> appService;

        public StoreTypeSiteFactory(
            IAppProjectsService<Project> appService)
        {
            this.appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        public override async Task Create(string clientProjectId, string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            await this.appService.AddStoreTypeSite(clientProjectId, name, description, clientId,
             buildInType, newProjectLocation, templateLocation,
             cardApiKey, cardServiceGate, hostingServiceGate,
             repository);
        }
    }
}