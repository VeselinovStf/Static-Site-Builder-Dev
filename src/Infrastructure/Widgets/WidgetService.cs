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
   public class WidgetService : IWidgetService<ClientWidgetListDTO>
    {
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppWidgetService appWidgetService;
        private readonly IAccountService<ApplicationUser> accountService;

        public WidgetService(
            IAppClientWidgetService appClientWidgetService,
            IAppWidgetService appWidgetService,
            IAccountService<ApplicationUser> accountService)
        {
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<ClientWidgetListDTO> GetAllAdminAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
                  clientId, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

            var user = await this.accountService.FindByIdAsync(clientId);

            Validator.ObjectIsNull(
               user, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(user)} : Can't find user with this id.");

            var adminRoles = await this.accountService.GetRolesAsync(user);

            Validator.ObjectIsNull(
               adminRoles, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(adminRoles)} : Can't find admin roles");

            if (!adminRoles.Contains("Administrator"))
            {
                throw new WidgetServiceGetAllAdminException($"{nameof(WidgetServiceGetAllAdminException)} : Exception : ATTENTION USER WITH ID {clientId} IS NOT ADMIN");
            }
           
            var adminWidgets = await this.appClientWidgetService.GetAllAsync(clientId);

            var availibleSystemWidgets = await this.appWidgetService.GetAllWidgetsAsync();

            Validator.ObjectIsNull(
               adminWidgets, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any admin widgets with this id.");

            var resultModel = new ClientWidgetListDTO()
            {
                ClientId = clientId,
                ClientWidgets = new List<WidgetDTO>(adminWidgets.ClientWidjets.Select(w => new WidgetDTO()
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
                AvailibleWidgets = availibleSystemWidgets.Count() < 1 ? new List<WidgetDTO>() : new List<WidgetDTO>(availibleSystemWidgets.Select(w => new WidgetDTO()
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

                var availibleWidgets = widgetsCall.Except(clientWidgets.ClientWidjets).ToList();

                Validator.ObjectIsNull(
                  availibleWidgets, $"{nameof(WidgetService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : Can't find any availible widgets for this user.");
               
                var resultModel = new ClientWidgetListDTO()
                {
                    ClientId = clientId,
                    ClientWidgets = new List<WidgetDTO>(clientWidgets.ClientWidjets.Select(w => new WidgetDTO()
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
