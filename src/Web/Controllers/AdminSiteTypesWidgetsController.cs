using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypeWidgets.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.AdminSiteTypeWidgetModelFactory.Abstraction;
using Web.ViewModels.AdminSiteTypesWidgets;

namespace Web.Controllers
{
	[Authorize(Roles = "Administrator")]
    public class AdminSiteTypesWidgetsController : Controller
    {
		private readonly IAdminSiteTypeUsebleWidgetsService<UsebleSiteTypeWidgetListDTO> adminSiteTypeUsebleWidgetsService;
		private readonly IAdminSiteTypeWidgetModelFactory modelFactory;
		private readonly IAppLogger<AdminSiteTypesWidgetsController> logger;

		public AdminSiteTypesWidgetsController(
			IAdminSiteTypeUsebleWidgetsService<UsebleSiteTypeWidgetListDTO> adminSiteTypeUsebleWidgetsService,
			IAdminSiteTypeWidgetModelFactory modelFactory,
			IAppLogger<AdminSiteTypesWidgetsController> logger)
		{
			this.adminSiteTypeUsebleWidgetsService = adminSiteTypeUsebleWidgetsService ?? throw new ArgumentNullException(nameof(adminSiteTypeUsebleWidgetsService));
			this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		[HttpGet]
        public async Task<IActionResult> AddUsebleWidget(string siteTypeId)
        {
			try
			{
				var serviceCall = await this.adminSiteTypeUsebleWidgetsService.GetSiteTypeAsync(siteTypeId);

				this.logger.LogInformation($"{nameof(AdminSiteTypesWidgetsController)} : {nameof(AddUsebleWidget)} : Geting administrated site type template useble widgets done.");

				var model = this.modelFactory.Create(serviceCall);

				return View(model);
			}
			catch (Exception ex)
			{
				this.logger.LogWarning($"{nameof(AdminSiteTypesWidgetsController)} : {nameof(AddUsebleWidget)} : Can't get administrated site type template useble widgets : {ex.Message}");

				return RedirectToAction("Error", "Home", new { message = "Can't get administrated site type template useble widgets.Contact support!" });
			}
        }

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> AddUsebleWidget([Bind("SiteTypeId")]CreateUsebleWidgetViewModel model)
		//{
		//	try
		//	{

		//	}
		//	catch (Exception ex)
		//	{

		//		throw;
		//	}
		//}
	}
}
