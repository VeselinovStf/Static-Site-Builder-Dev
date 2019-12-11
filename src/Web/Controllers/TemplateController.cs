using ApplicationCore.Interfaces;
using Infrastructure.Templates.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.TemplateModelFactory.Abstraction;
using Web.ViewModels.Template;

namespace Web.Controllers
{   
    public class TemplateController : Controller
    {
        private readonly ITemplateService<SiteTemplateDTO> templateService;
        private readonly ITemplateModelFactory modelFactory;
        private readonly IAppLogger<TemplateController> logger;

        public TemplateController(
            ITemplateService<SiteTemplateDTO> templateService,
            ITemplateModelFactory modelFactory,
            IAppLogger<TemplateController> logger)
        {
            this.templateService = templateService ?? throw new System.ArgumentNullException(nameof(templateService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> SelectTemplate(string buildInType, string clientId)
        {
            //TODO: VALIDATION ON INPUT PARAMETTERS
            try
            {
                var buildInSiteTemplatesServiceCall = await this.templateService.GetAllAsync(buildInType, clientId);

                this.logger.LogInformation($"{nameof(TemplateController)} : {nameof(SelectTemplate)} : Sucess - Getting Site Templates");

                var model = this.modelFactory.Create(buildInSiteTemplatesServiceCall.ToList(), buildInType, clientId);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(TemplateController)} : {nameof(SelectTemplate)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Select Site Template. Contact support" });
            }

            //Call db to get all availible build in template
        }

       
    }
}