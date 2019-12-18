using ApplicationCore.Interfaces;
using Infrastructure.Blog.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.ClientModelFactory.Abstraction;

namespace Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IClientBlogPostService<ClientPostDTO> clientBlogPostService;
        private readonly ITutorialService tutorialService;
        private readonly IClientModelFactory modelFactory;
        private readonly IAppLogger<ClientController> logger;

        public ClientController(
             IClientBlogPostService<ClientPostDTO> clientBlogPostService,
             ITutorialService tutorialService,
            IClientModelFactory modelFactory,
            IAppLogger<ClientController> logger
            )
        {
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.clientBlogPostService = clientBlogPostService ?? throw new ArgumentNullException(nameof(clientBlogPostService));
            this.tutorialService = tutorialService ?? throw new ArgumentNullException(nameof(tutorialService));
        }

        [HttpGet]
        public async Task<IActionResult> ClientArea(string clientId)
        {
            try
            {
                var blogPostServiceCall = await this.clientBlogPostService.GetAllClientPostsWithCommentsAsync(clientId);

                this.logger.LogInformation($"{nameof(ClientController)} : {nameof(ClientArea)} : Geting client blog posts done.");

                var inTutorial = await this.tutorialService.IsClientInTutorial(clientId);

                this.logger.LogInformation($"{nameof(ClientController)} : {nameof(ClientArea)} : Geting client tutorial status done.");

                var model = this.modelFactory.Create(blogPostServiceCall, clientId,inTutorial);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(ClientController)} : {nameof(ClientArea)} : Can't get client posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }
    }
}