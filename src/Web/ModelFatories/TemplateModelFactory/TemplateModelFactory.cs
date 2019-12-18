using Infrastructure.Templates.DTOs;
using System.Collections.Generic;
using System.Linq;
using Web.ModelFatories.TemplateModelFactory.Abstraction;
using Web.ViewModels.Template;

namespace Web.ModelFatories.TemplateModelFactory
{
    public class TemplateModelFactory : ITemplateModelFactory
    {
        public SelectTemplateViewModel Create(List<SiteTemplateDTO> serviceModel, string buildInName,string buildInTypeId, string clientId)
        {
            return new SelectTemplateViewModel()
            {
                BuildInSiteTypeName = buildInName,
                ClientId = clientId,
               SiteTypeId = buildInTypeId,
                SiteTypeTemplate = new List<SiteTemplateViewModel>(serviceModel.Select(e => new SiteTemplateViewModel()
                {
                    Name = e.Name,
                    Description = e.Description,
                    Price = e.Price
                }))

            };
        }
    }
}