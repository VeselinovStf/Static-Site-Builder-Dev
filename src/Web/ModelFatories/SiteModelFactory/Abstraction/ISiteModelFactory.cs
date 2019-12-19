using Infrastructure.Site.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Site;
using Web.ViewModels.SiteRendering;

namespace Web.ModelFatories.SiteModelFactory.Abstraction
{
    public interface ISiteModelFactory
    {
        SiteRenderingViewModel Create(SiteRenderingDTO serviceModel, string siteTypeId);
    }
}
