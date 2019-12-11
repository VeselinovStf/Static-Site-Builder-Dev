using ApplicationCore.Interfaces;
using Infrastructure.Blog.DTOs;
using Infrastructure.SiteTypes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.AdminModelFactory.Abstraction;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdministratedBlogPostService<AdministratedPostDTO> administratedBlogPostService;
       
        private readonly IAdminModelFactory adminModelFactory;
        
        private readonly IAppLogger<AdminController> logger;

        public AdminController(
            IAdministratedBlogPostService<AdministratedPostDTO> administratedBlogPostService,
           
            IAdminModelFactory admninModelFactory,
            
            IAppLogger<AdminController> logger
            )
        {
            this.administratedBlogPostService = administratedBlogPostService ?? throw new ArgumentNullException(nameof(administratedBlogPostService));
           
            this.adminModelFactory = admninModelFactory ?? throw new ArgumentNullException(nameof(admninModelFactory));
         
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> AdminArea(string clientId)
        {
            try
            {
                var serviceCall = await this.administratedBlogPostService.GetAllAdminPostsAsync(clientId);

                this.logger.LogInformation($"{nameof(AdminController)} : {nameof(AdminArea)} : Geting administrated blog posts done.");

                var model = this.adminModelFactory.Create(serviceCall, clientId);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminController)} : {nameof(AdminArea)} : Can't get admin posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        
    }
}