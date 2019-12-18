using ApplicationCore.Interfaces;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.WidgetsModelFactory.Abstraction;

namespace Web.Views.ViewComponents.AdminSiteNavigationViewComponents
{
    public class AdminSideNavigationViewComponent : ViewComponent
    {
        private readonly IManageWidgetService<ClientSiteWidgetsDTO> manageWidgetService;
        private readonly IWidgetModelFactory modelFactory;
        private readonly IAppLogger<AdminSideNavigationViewComponent> logger;

        public AdminSideNavigationViewComponent(
            IManageWidgetService<ClientSiteWidgetsDTO> manageWidgetService,
            IWidgetModelFactory modelFactory,
            IAppLogger<AdminSideNavigationViewComponent> logger)
        {
            this.manageWidgetService = manageWidgetService ?? throw new System.ArgumentNullException(nameof(manageWidgetService));
            this.modelFactory = modelFactory;
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
        public async Task<IViewComponentResult> InvokeAsync(string clientId, string templateName, string siteTypeId)
        {
            try
            {
                var serviceCall = await this.manageWidgetService.GetAllAsync(clientId, templateName);

                this.logger.LogInformation($"{nameof(AdminSideNavigationViewComponent)} : {nameof(InvokeAsync)} : Sucess - Getting Client Site Widgets");

                var model = this.modelFactory.Create(serviceCall,templateName,siteTypeId,clientId);

                return View("AdminSideNavigation", model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminSideNavigationViewComponent)} : {nameof(InvokeAsync)} : Exception - {ex.Message}");

                return View();
            }

           
        }
    }
}