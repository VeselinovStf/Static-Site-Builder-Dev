using Infrastructure.Site.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.SiteModelFactory.Abstraction;
using Web.ViewModels.Site;
using Web.ViewModels.SiteRendering;

namespace Web.ModelFatories.SiteModelFactory
{
    public class SiteModelFactory : ISiteModelFactory
    {
        public SiteRenderingViewModel Create(SiteRenderingDTO serviceModel,string siteTypeId)
        {
            return new SiteRenderingViewModel()
            {
                ClientId = serviceModel.ClientId,
                PresentationLink = serviceModel.PresentationLink,
                TemplateName = serviceModel.TemplateName,
                SiteTypeId = siteTypeId
            };
        }
    }
}
