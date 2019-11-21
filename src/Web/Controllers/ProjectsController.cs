using ApplicationCore.Interfaces;
using Infrastructure.ClientProjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.ProjectsModelFactory.Abstraction;

namespace Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IClientProjectService<ClientProjectDTO> projectService;
        private readonly IProjectsModelFactory modelFactory;
        private readonly IAppLogger<ProjectsController> logger;

        public ProjectsController(
            IClientProjectService<ClientProjectDTO> projectService,
            IProjectsModelFactory modelFactory,
            IAppLogger<ProjectsController> logger)
        {
            this.projectService = projectService ?? throw new System.ArgumentNullException(nameof(projectService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index(string clientId, bool walktry = false)
        {
            try
            {
                var serviceCall = await this.projectService.GetAllAsync(clientId);

                this.logger.LogInformation($"{nameof(ProjectsController)} : {nameof(Index)} : Sucess - Getting Projects");

                var model = this.modelFactory.Create(serviceCall, clientId, walktry);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(ProjectsController)} : {nameof(Index)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display projects. Contact support" });
            }
        }
    }
}