using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.HostingHubConnectorService;
using Infrastructure.Templates.DTOs;
using Infrastructure.Templates.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Templates
{
    public class TemplateService : ITemplateService<SiteTemplateDTO>
    {
        private readonly IAppSiteTemplatesService<SiteTemplate> appTemplateService;
        private readonly IOptions<AuthHostingConnectorOptions> hostingOptions;

        public TemplateService(
            IAppSiteTemplatesService<SiteTemplate> appTemplateService,
            IOptions<AuthHostingConnectorOptions> hostingOptions)
        {
            this.appTemplateService = appTemplateService ?? throw new ArgumentNullException(nameof(appTemplateService));
            this.hostingOptions = hostingOptions ?? throw new ArgumentNullException(nameof(hostingOptions));
        }
    

        public async Task<IList<SiteTemplateDTO>> GetAllAsync(string buildInType, string buildInTypeId,string clientId)
        {
            try
            {
                Validator.StringIsNullOrEmpty(
                    buildInType, $"{nameof(TemplateService)} : {nameof(GetAllAsync)} : {nameof(buildInType)} : is null/empty");

                Validator.StringIsNullOrEmpty(
                     clientId, $"{nameof(TemplateService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

                Validator.StringIsNullOrEmpty(
                     buildInTypeId, $"{nameof(TemplateService)} : {nameof(GetAllAsync)} : {nameof(buildInTypeId)} : is null/empty");

                var elementsCall = await this.appTemplateService.GetAllTemplatesByTypeAsync(buildInType);

                Validator.ObjectIsNull(
                    elementsCall, $"{nameof(TemplateService)} : {nameof(GetAllAsync)} : {nameof(elementsCall)} : Can't find site templates with this build in type");

                var returnModel = new List<SiteTemplateDTO>(elementsCall.Select(e => new SiteTemplateDTO()
                {
                    ClientId = clientId,
                    Name = e.Name,
                    SiteType = buildInType,

                    Description = e.Description,
                    Price = e.Price
                }));

                return returnModel;
            }
            catch (Exception ex)
            {
                throw new TemplateServiceGetAllException($"{nameof(TemplateServiceGetAllException)} : Exception : Can't get all site templates : {ex.Message}");
            }
        }
    }
}