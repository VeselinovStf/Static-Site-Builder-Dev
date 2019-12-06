using Infrastructure.Site.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.SiteModelFactory.Abstraction;
using Web.ViewModels.Site;

namespace Web.ModelFatories.SiteModelFactory
{
    public class SiteModelFactory : ISiteModelFactory
    {
        public SiteRenderingViewModel Create(SiteRenderingDTO serviceModel)
        {
            return new SiteRenderingViewModel()
            {
                Widgets = new List<MenuWidgetViewModel>(serviceModel.Widget.Select(w => new MenuWidgetViewModel()
                {
                    Name = w.Name
                }))
            };
        }
    }
}
