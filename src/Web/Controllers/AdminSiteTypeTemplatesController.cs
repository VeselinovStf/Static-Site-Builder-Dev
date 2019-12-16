using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.AdminSiteTypeTemplates;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminSiteTypeTemplatesController : Controller
    {
		private readonly ISiteTemplateService siteTemplateService;
		private readonly IAppLogger<AdminSiteTypesController> logger;

		public AdminSiteTypeTemplatesController(
			ISiteTemplateService siteTemplateService,
			IAppLogger<AdminSiteTypesController> logger)
		{
			this.siteTemplateService = siteTemplateService ?? throw new ArgumentNullException(nameof(siteTemplateService));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		[HttpGet]
		public  IActionResult AddUsebleTemplate(string siteTypeId)
		{
			if (string.IsNullOrWhiteSpace(siteTypeId))
			{
				this.logger.LogWarning($"{nameof(AdminSiteTypeTemplatesController)} : {nameof(AddUsebleTemplate)} : Can't add administrated template without site type id : {(nameof(siteTypeId))}");

				return RedirectToAction("Error", "Home", new { message = "Can't add template!" });
			}

			var model = new AdminCreateSiteTypeTemplateViewModel()
			{
				SiteTypeId = siteTypeId
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddUsebleTemplate([Bind("SiteTypeId", "TemplateName")]AdminCreateSiteTypeTemplateViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await this.siteTemplateService.AddUsebleWidgets(model.SiteTypeId, model.TemplateName);


					this.logger.LogInformation($"{nameof(AdminSiteTypeTemplatesController)} : {nameof(AddUsebleTemplate)} : Adding administrated site type template.");

					return RedirectToAction("Type", "AdminSiteTypes", new { siteTypeId = model.SiteTypeId });

				}
				catch (Exception ex)
				{
					this.logger.LogWarning($"{nameof(AdminSiteTypeTemplatesController)} : {nameof(AddUsebleTemplate)} : Can't add administrated site type template : {ex.Message}");

					return RedirectToAction("Error", "Home", new { message = "Can't add administrated site type template.Contact support!" });
				}
			}

			return View(model.SiteTypeId);
		}
	}
}
