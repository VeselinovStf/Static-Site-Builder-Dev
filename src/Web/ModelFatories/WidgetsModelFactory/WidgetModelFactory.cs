using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Widgets.DTOs;
using Web.ModelFatories.WidgetsModelFactory.Abstraction;
using Web.ViewModels.Site;
using Web.ViewModels.Widget;

namespace Web.ModelFatories.WidgetsModelFactory
{
    public class WidgetModelFactory : IWidgetModelFactory
    {
        public WidgetsListViewModel Create(ClientWidgetListDTO inputModel)
        {
            return new WidgetsListViewModel()
            {
                ClientId = inputModel.ClientId,
                
                ClientWidgets = new List<WidgetViewModel>(inputModel.ClientWidgets.Select(w => new WidgetViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Dependency = w.Dependency.ToString(),
                    Functionality = w.Functionality,
                    IsFree = w.IsFree,
                    IsOn = w.IsOn,
                    Price = w.Price,
                    SiteTypeSpecification = w.SiteTypeSpecification.ToString(),

                    Version = w.Version,
                    Votes = w.Votes
                })),
                AvailibleWidgets = inputModel.AvailibleWidgets.Count() < 1 ? 
                    new List<WidgetViewModel>() : 
                        new List<WidgetViewModel>(inputModel.AvailibleWidgets.Select(w => new WidgetViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Dependency = w.Dependency.ToString(),
                    Functionality = w.Functionality,
                    IsFree = w.IsFree,
                    IsOn = w.IsOn,
                    Price = w.Price,
                    SiteTypeSpecification = w.SiteTypeSpecification.ToString(),

                    Version = w.Version,
                    Votes = w.Votes
                }))
            };
        }

        public SiteViewModel Create(ClientSiteWidgetsDTO serviceCall, string templateName, string siteTypeId,string clientId)
        {
            return new SiteViewModel()
            {
                ClientId = clientId,
               TemplateName = templateName,
               SiteTypeId = siteTypeId,
                Widgets = new List<SiteWidgetViewModel>(serviceCall.Widgets.Select(w => new SiteWidgetViewModel()
                {
                    DisplayName = w.DisplayName,
                    IsOn = w.IsOn,
                    WidgetId = w.WidgetId
                }))
            };
        }

        
    }
}
