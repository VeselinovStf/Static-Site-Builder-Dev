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
    public class AdminWidgetsService : IWidgetService<AdminClientWidgetListDTO>
    {
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppWidgetService appWidgetService;
        private readonly IAccountService<ApplicationUser> accountService;

        public AdminWidgetsService(
            IAppClientWidgetService appClientWidgetService,
            IAppWidgetService appWidgetService,
            IAccountService<ApplicationUser> accountService)
        {
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<AdminClientWidgetListDTO> GetAllAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
                  clientId, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

            var user = await this.accountService.FindByIdAsync(clientId);

            Validator.ObjectIsNull(
               user, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(user)} : Can't find user with this id.");

            var adminRoles = await this.accountService.GetRolesAsync(user);

            Validator.ObjectIsNull(
               adminRoles, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(adminRoles)} : Can't find admin roles");

            if (!adminRoles.Contains("Administrator"))
            {
                throw new WidgetServiceGetAllAdminException($"{nameof(WidgetServiceGetAllAdminException)} : Exception : ATTENTION USER WITH ID {clientId} IS NOT ADMIN");
            }

            var adminWidgets = await this.appClientWidgetService.GetAllAsync(clientId);

            var availibleSystemWidgets = await this.appWidgetService.GetAllWidgetsAsync();

            Validator.ObjectIsNull(
               adminWidgets, $"{nameof(ClientWidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any admin widgets with this id.");

            var resultModel = new AdminClientWidgetListDTO()
            {
                ClientId = clientId,
                ClientWidgets = new List<WidgetDTO>(adminWidgets.ClientWidgets.Select(w => new WidgetDTO()
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
               
            };

            return resultModel;
        }
    }
}
