using ApplicationCore.Interfaces;
using Infrastructure.Blog.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.BlogModelFactory.Abstraction;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostService<PublicPostDTO> publicBlogPostService;
        private readonly IBlogModelFactory modelFactory;
        private readonly IAppLogger<BlogController> logger;

        public BlogController(
            IBlogPostService<PublicPostDTO> publicBlogPostService,
            IBlogModelFactory modelFactory,
            IAppLogger<BlogController> logger)
        {
            this.publicBlogPostService = publicBlogPostService ?? throw new System.ArgumentNullException(nameof(publicBlogPostService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var serviceCall = await this.publicBlogPostService.GetAllPublicPosts();

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}