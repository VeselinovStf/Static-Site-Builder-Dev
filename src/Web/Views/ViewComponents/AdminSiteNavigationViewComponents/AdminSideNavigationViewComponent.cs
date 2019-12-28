using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.WidgetsModelFactory.Abstraction;

namespace Web.Views.ViewComponents.AdminSiteNavigationViewComponents
{
    public class AdminSideNavigationViewComponent : ViewComponent
    {
        private readonly IManageWidgetService<ClientSiteWidgetsDTO> manageWidgetService;
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IWidgetModelFactory modelFactory;
        private readonly IAppLogger<AdminSideNavigationViewComponent> logger;

        public AdminSideNavigationViewComponent(
            IManageWidgetService<ClientSiteWidgetsDTO> manageWidgetService,
            IAccountService<ApplicationUser> accountService,
            IWidgetModelFactory modelFactory,
            IAppLogger<AdminSideNavigationViewComponent> logger)
        {
            this.manageWidgetService = manageWidgetService ?? throw new System.ArgumentNullException(nameof(manageWidgetService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = await this.accountService.RetrieveUserAsync(UserClaimsPrincipal);

                var templateName = HttpContext.Session.GetString("_SiteTemplateName");

                var serviceCall = await this.manageWidgetService.GetAllAsync(client.Id, templateName);

                this.logger.LogInformation($"{nameof(AdminSideNavigationViewComponent)} : {nameof(InvokeAsync)} : Sucess - Getting Client Site Widgets");

                var model = this.modelFactory.Create(serviceCall);

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