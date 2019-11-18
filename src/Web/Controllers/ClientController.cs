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
        private readonly IClientModelFactory modelFactory;
        private readonly IAppLogger<ClientController> logger;

        public ClientController(
             IClientBlogPostService<ClientPostDTO> clientBlogPostService,
            IClientModelFactory modelFactory,
            IAppLogger<ClientController> logger
            )
        {
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.clientBlogPostService = clientBlogPostService ?? throw new ArgumentNullException(nameof(clientBlogPostService));
        }

        [HttpGet]
        public async Task<IActionResult> ClientArea(string clientId)
        {
            try
            {
                var serviceCall = await this.clientBlogPostService.GetAllClientPostsWithCommentsAsync(clientId);

                this.logger.LogInformation($"{nameof(ClientController)} : {nameof(ClientArea)} : Geting client blog posts done.");

                var model = this.modelFactory.Create(serviceCall, clientId);

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