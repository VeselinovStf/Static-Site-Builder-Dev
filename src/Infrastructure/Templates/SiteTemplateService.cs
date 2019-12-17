using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Templates.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Templates
{
    public class SiteTemplateService : ISiteTemplateService
    {
        private readonly IAppSiteTypesService<SiteType> appSiteTypesService;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService;
       
        private readonly ISiteStorageCreatorService siteStorageCreatorService;

        public SiteTemplateService(
            IAppSiteTypesService<SiteType> appSiteTypesService,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService,
            
            ISiteStorageCreatorService siteStorageCreatorService
            )
        {
            this.appSiteTypesService = appSiteTypesService ?? throw new ArgumentNullException(nameof(appSiteTypesService));
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
          
            this.siteStorageCreatorService = siteStorageCreatorService ?? throw new ArgumentNullException(nameof(siteStorageCreatorService));
        }

        public async Task AddTemplateAsync(string siteTypeId, string templateName, string description, decimal price)
        {
            Validator.StringIsNullOrEmpty(
                   siteTypeId, $"{nameof(SiteTemplateService)} : {nameof(AddTemplateAsync)} : {nameof(siteTypeId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                 templateName, $"{nameof(SiteTemplateService)} : {nameof(AddTemplateAsync)} : {nameof(templateName)} : is null/empty");
            
            Validator.StringIsNullOrEmpty(
                 description, $"{nameof(SiteTemplateService)} : {nameof(AddTemplateAsync)} : {nameof(description)} : is null/empty");


            try
            {
                var siteType = await this.appSiteTypesService.GetSiteTypeAsync(siteTypeId);

                Validator.ObjectIsNull(
                    siteType, $"{nameof(SiteTemplateService)} : {nameof(AddTemplateAsync)} : {nameof(siteType)} : Can't find site type with this id");

                var createdTemplate = await this.appSiteTemplateService.CreateTemplateAsync(siteTypeId, templateName,description, price);

                Validator.ObjectIsNull(
                   createdTemplate, $"{nameof(SiteTemplateService)} : {nameof(AddTemplateAsync)} : {nameof(createdTemplate)} : Can't create template");

              //  await this.appTemplateElementService.AddTemplateElements(createdTemplate.Id);
            }
            catch (Exception ex)
            {

                throw new SiteTemplateServiceAddTemplateAsyncException($"{nameof(SiteTemplateServiceAddTemplateAsyncException)} : Exception : Can't add site template : {ex.Message}");

            }
            

        }

        public async Task UpdateTemplateStructureAsync(string siteTypeId, string templateId, string templateName)
        {

            await this.siteStorageCreatorService.UpdateTemplate(templateId,templateName);
          
            //throw new NotImplementedException();
        }
    }
}
