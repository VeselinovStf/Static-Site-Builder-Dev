using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Identity;
using Infrastructure.Widgets.DTOs;
using Infrastructure.Widgets.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Widgets
{
   public class ClientWidgetService : IWidgetService<ClientWidgetListDTO>
    {
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppWidgetService appWidgetService;
        private readonly IAccountService<ApplicationUser> accountService;

        public ClientWidgetService(
            IAppClientWidgetService appClientWidgetService,
            IAppWidgetService appWidgetService,
            IAccountService<ApplicationUser> accountService)
        {
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task AddWidget(string widgetId, string clientId)
        {
            Validator.StringIsNullOrEmpty(
                  widgetId, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(widgetId)} : is null/empty");

            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ClientWidgetListDTO> GetAllAsync(string clientId)
        {
            try
            {
                Validator.StringIsNullOrEmpty(
                  clientId, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

                var widgetsCall = await this.appWidgetService.GetAllWidgetsAsync();

                Validator.ObjectIsNull(
                    widgetsCall, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any widgets.");

                var clientWidgets = await this.appClientWidgetService.GetAllAsync(clientId);

                Validator.ObjectIsNull(
                   clientWidgets, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any client widgets with this id.");

                var availibleWidgets = widgetsCall.Except(clientWidgets.ClientWidgets.Select(w => w.Widget));

                Validator.ObjectIsNull(
                  availibleWidgets, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any availible widgets for this user.");

                var resultModel = new ClientWidgetListDTO()
                {
                    ClientId = clientId,
                    ClientWidgets = new List<WidgetDTO>(clientWidgets.ClientWidgets.Select(w => new WidgetDTO()
                    {
                        Id = w.Widget.Id,
                        Name = w.Widget.Name,
                        Description = w.Widget.Description,
                        Dependency = w.Widget.Dependency.ToString(),
                        Functionality = w.Widget.Functionality,
                        IsFree = w.Widget.IsFree,
                        IsOn = w.Widget.IsOn,
                        Price = w.Widget.Price,
                        SiteTypeSpecification = w.Widget.SiteTypeSpecification.ToString(),

                        Version = w.Widget.Version,
                        Votes = w.Widget.Votes
                    })),
                    AvailibleWidgets = availibleWidgets.Count() < 1 ? new List<WidgetDTO>() : new List<WidgetDTO>(availibleWidgets.Select(w => new WidgetDTO()
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

                return resultModel;

            }
            catch (Exception ex)
            {
                throw new WidgetServiceGetAllAsyncException($"{nameof(WidgetServiceGetAllAsyncException)} : Exception : Can't get client widgets : {ex.Message}");
            }
        }
    }
}
