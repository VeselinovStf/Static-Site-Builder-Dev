using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Widgets.DTOs;
using Infrastructure.Widgets.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Widgets
{
   public class WidgetService : IWidgetService<ClientWidgetListDTO>
    {
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppWidgetService appWidgetService;

        public WidgetService(
            IAppClientWidgetService appClientWidgetService,
            IAppWidgetService appWidgetService)
        {
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
        }

        public async Task<ClientWidgetListDTO> GetAllAsync(string clientId)
        {
            try
            {
                Validator.StringIsNullOrEmpty(
                  clientId, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

                var widgetsCall = await this.appWidgetService.GetAllWidgetsAsync();

                Validator.ObjectIsNull(
                    widgetsCall, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any widgets.");

                var clientWidgets = await this.appClientWidgetService.GetAllAsync(clientId);

                Validator.ObjectIsNull(
                   clientWidgets, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any client widgets with this id.");

                var availibleWidgets = widgetsCall.Where(w => clientWidgets.ClientWidjets.Any(x => x.Id != w.Id));

                var resultModel = new ClientWidgetListDTO()
                {
                    ClientId = clientId,
                    ClientWidgets = new List<WidgetDTO>(clientWidgets.ClientWidjets.Select(w => new WidgetDTO()
                    {
                        Name = w.Name,
                        Description = w.Description,
                        Dependency = w.Dependency.ToString(),
                        Functionality = w.Functionality,
                        IsFree = w.IsFree,
                        IsOn = w.IsOn,
                        Price = w.Price,
                        SiteTypeSpecification = w.SiteTypeSpecification.ToString(),
                        UsebleSiteType = w.UsebleSiteType.ToString(),
                        Version = w.Version,
                        Votes = w.Votes
                    })),
                    AvailibleWidgets = new List<WidgetDTO>(availibleWidgets.Select(w => new WidgetDTO() 
                    {
                        Name = w.Name,
                        Description = w.Description,
                        Dependency = w.Dependency.ToString(),
                        Functionality = w.Functionality,
                        IsFree = w.IsFree,
                        IsOn = w.IsOn,
                        Price = w.Price,
                        SiteTypeSpecification = w.SiteTypeSpecification.ToString(),
                        UsebleSiteType = w.UsebleSiteType.ToString(),
                        Version = w.Version,
                        Votes = w.Votes
                    }))                    
                };

                return resultModel;
                
            }
            catch (Exception ex)
            {
                throw new WidgetServiceGetAllAsyncException($"{nameof(WidgetServiceGetAllAsyncException)} : Exception : Can't get client widgets : {ex.Message}");
            }
        }
    }
}
