using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppSiteTemplatesService : IAppSiteTemplatesService<SiteTemplate>
    {
        private readonly IAsyncRepository<SiteTemplate> siteTemplatesRepository;
        private readonly IAsyncRepository<Widget> widgetsRepository;

        public AppSiteTemplatesService(
            IAsyncRepository<SiteTemplate> siteTemplatesRepository,
            IAsyncRepository<Widget> widgetsRepository)
        {
            this.siteTemplatesRepository = siteTemplatesRepository ?? throw new ArgumentNullException(nameof(siteTemplatesRepository));
            this.widgetsRepository = widgetsRepository ?? throw new ArgumentNullException(nameof(widgetsRepository));
        }

       

        public async Task<SiteTemplate> CreateTemplateAsync(string siteTypeId, string templateName, string description,decimal price)
        {
            var newTemplate = new SiteTemplate()
            {
                Name = templateName,
                Description = description,
                SiteTypeId = siteTypeId,
                Price = price
            };

           var result = await this.siteTemplatesRepository.AddAsync(newTemplate);

            return result;
        }

        //public async Task AddVariablesAsync(string buildInSiteType, string templateName, string siteId, string accessToken)
        //{
        //    var specification = new SiteTemplateByBuildNameAndTemplateNameSpecification(buildInSiteType, templateName);

        //    var template = this.siteTemplatesRepository.GetSingleBySpec(specification);

        //    var element = template.SiteTemplateElements.FirstOrDefault(t => t.FilePath == "gitlab-ci.yml");

        //    element.FileContent.Replace("NETLIFY_SITE_ID", siteId);
        //    element.FileContent.Replace("NETLIFY_AUTH_TOKEN", accessToken);

        //    await this.siteTemplatesRepository.UpdateAsync(template);
        //}

        public async Task<IEnumerable<SiteTemplate>> GetAllTemplatesByTypeAsync(string buildInType)
        {
            var specification = new SiteTemplatesByBuildInTypeNameSpecification(buildInType);

            return await this.siteTemplatesRepository.ListAsync(specification);
        }

        public async Task<SiteTemplate> GetTemplateAsync(string templateName)
        {
            var specification = new SiteTemplateByNameWithWidgetsSpecification(templateName);

            return this.siteTemplatesRepository.GetSingleBySpec(specification);
        }

        public async Task<SiteTemplate> GetTemplateWithElementsAsync(string templateName)
        {
            var specification = new SiteTemplateWithElementsSpecification(templateName);

            return this.siteTemplatesRepository.GetSingleBySpec(specification);
        }

       
    }
}