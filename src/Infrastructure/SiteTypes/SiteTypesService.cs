using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.SiteTypes.DTOs;
using Infrastructure.SiteTypes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SiteTypes
{
    public class SiteTypesService : ISiteTypesService<SiteTypeDTO>
    {
        private readonly IAppSiteTypesService<SiteType> appSiteTypeService;
        private readonly IAppProjectsService<Project> appProjectService;
        private readonly Dictionary<SiteTypesEnum, SiteTypesFactory> _factories;

        public SiteTypesService(
            IAppSiteTypesService<SiteType> appSiteTypeService,
            IAppProjectsService<Project> appProjectService)
        {
            this.appSiteTypeService = appSiteTypeService ?? throw new System.ArgumentNullException(nameof(appSiteTypeService));
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            _factories = new Dictionary<SiteTypesEnum, SiteTypesFactory>
                        {
                            { SiteTypesEnum.BlogType, new BlogTypeSiteFactory(appProjectService) },
                            { SiteTypesEnum.StoreType, new StoreTypeSiteFactory(appProjectService) }
                        };
        }

        private async Task ExecuteCreation(SiteTypesEnum action, string clientProjectId,
            string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository) => await _factories[action].Create(clientProjectId,
             name, description, clientId,
             buildInType, newProjectLocation, templateLocation,
             cardApiKey, cardServiceGate, hostingServiceGate,
             repository);

        public async Task<bool> ConfirmTypeAsync(string buildInType)
        {
            try
            {
                var siteTypes = await this.appSiteTypeService.GetAllAsync();

                Validator.ObjectIsNull(
                 siteTypes, $"{nameof(SiteTypesService)} : {nameof(GetAllTypesAsync)} : {nameof(siteTypes)} : Can't find build in site types!");

                if (siteTypes.Any(t => t.Type.ToString() == buildInType))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceConfirmTypeException($"{nameof(SiteTypesServiceConfirmTypeException)} : Can't get build in types! USER TRY UNLEAGLE ACTION : {ex.Message}");
            }
        }

        public async Task CreateAsync(
            string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            Validator.StringIsNullOrEmpty(
                 name, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(name)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                description, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(description)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(clientId)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                buildInType, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(buildInType)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                newProjectLocation, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(newProjectLocation)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                templateLocation, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(templateLocation)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                cardApiKey, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(cardApiKey)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                cardServiceGate, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(cardServiceGate)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                hostingServiceGate, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(hostingServiceGate)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                repository, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(repository)} : is null/empty");

            try
            {
                var type = (SiteTypesEnum)Enum.Parse(typeof(SiteTypesEnum), buildInType);

                var clientProject = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProject, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(clientProject)} : Object is null");

                var clientProjectId = clientProject.Id;

                Validator.StringIsNullOrEmpty(
                    clientProjectId, $"{nameof(SiteTypesService)} : {nameof(CreateAsync)} : {nameof(clientProjectId)} : is null/empty");

                await this.ExecuteCreation(type, clientProjectId,
                           name, description, clientId,
                           buildInType, newProjectLocation, templateLocation,
                           cardApiKey, cardServiceGate, hostingServiceGate,
                           repository);
            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceCreateException($"{nameof(SiteTypesServiceCreateException)} : Can't create site type! : {ex.Message}");
            }
        }

        public async Task<IEnumerable<SiteTypeDTO>> GetAllTypesAsync()
        {
            try
            {
                var siteTypes = await this.appSiteTypeService.GetAllAsync();

                Validator.ObjectIsNull(
                 siteTypes, $"{nameof(SiteTypesService)} : {nameof(GetAllTypesAsync)} : {nameof(siteTypes)} : Can't find build in site types!");

                var serviceModel = new List<SiteTypeDTO>(siteTypes.Select(t => new SiteTypeDTO()
                {
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.Type.ToString()
                }));

                return serviceModel;
            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceGetAllTypesException($"{nameof(SiteTypesServiceGetAllTypesException)} : Can't get build in types! : {ex.Message}");
            }
        }
    }
}