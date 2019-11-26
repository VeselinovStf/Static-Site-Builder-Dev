using Infrastructure.Templates.DTOs;
using System.Collections.Generic;
using Web.ViewModels.Template;

namespace Web.ModelFatories.TemplateModelFactory.Abstraction
{
    public interface ITemplateModelFactory
    {
        SelectTemplateViewModel Create(List<SiteTemplateDTO> serviceModel, string buildInTemplate, string clientId);
    }
}