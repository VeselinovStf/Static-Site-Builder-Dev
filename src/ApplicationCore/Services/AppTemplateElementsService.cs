using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppTemplateElementsService : IAppTemplateElementsService<SiteTemplateElement>
    {
        private readonly IAsyncRepository<SiteTemplate> siteTemplatesRepository;

        public AppTemplateElementsService(
            IAsyncRepository<SiteTemplate> siteTemplatesRepository)
        {
            this.siteTemplatesRepository = siteTemplatesRepository ?? throw new ArgumentNullException(nameof(siteTemplatesRepository));
        }
        public async Task AddTemplateElementsAsync(string templateId, IList<SiteTemplateElement> elements)
        {
            var siteTemplate = await this.siteTemplatesRepository.GetByIdAsync(templateId);

            elements
               .ToList()
               .ForEach(
                   d => siteTemplate
                   .AddElement(
                       d.FilePath, d.FileContent
                       )
                );

            await this.siteTemplatesRepository.UpdateAsync(siteTemplate);
        }
    }
}
