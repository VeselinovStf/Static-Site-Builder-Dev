using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ModelFatories.ProjectsModelFactory.Abstraction;

namespace Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectsModelFactory modelFactory;
        private readonly IAppLogger<ProjectsController> logger;

        public ProjectsController(
            IProjectsModelFactory modelFactory,
            IAppLogger<ProjectsController> logger)
        {
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index(string clientId, bool walktry = false)
        {
            return View();
        }
    }
}