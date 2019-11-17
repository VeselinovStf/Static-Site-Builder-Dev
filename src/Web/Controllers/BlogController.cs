using ApplicationCore.Interfaces;
using Infrastructure.Blog.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.BlogModelFactory.Abstraction;
using Web.ViewModels.Blog;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPublicBlogPostService<PublicPostDTO> publicBlogPostService;
        private readonly IAdministratedBlogPostService<AdministratedPostDTO> administratedBlogPostService;
        private readonly IBlogModelFactory modelFactory;
        private readonly IAppLogger<BlogController> logger;

        public BlogController(
            IPublicBlogPostService<PublicPostDTO> publicBlogPostService,
            IAdministratedBlogPostService<AdministratedPostDTO> administratedBlogPostService,
            IBlogModelFactory modelFactory,
            IAppLogger<BlogController> logger)
        {
            this.publicBlogPostService = publicBlogPostService ?? throw new System.ArgumentNullException(nameof(publicBlogPostService));
            this.administratedBlogPostService = administratedBlogPostService ?? throw new ArgumentNullException(nameof(administratedBlogPostService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var serviceCall = await this.publicBlogPostService.GetAllPublicPosts();

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(Index)} : Geting public blog posts done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(Index)} : Can't get public posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> AdminBlog(string clientId)
        {
            try
            {
                var serviceCall = await this.administratedBlogPostService.GetAllAdminPosts(clientId);

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(Index)} : Geting public blog posts done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(AdminBlog)} : Can't get administrated posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentUser = User.Identity.Name;

            var model = new CreatePostViewModel()
            {
                AuthorName = currentUser
            };

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Header", "Image", "Content", "AuthorName")]CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceCallClientId = await this.administratedBlogPostService.Create(model.Header, model.Image, model.Content, model.AuthorName);

                    this.logger.LogInformation($"{nameof(BlogController)} : {nameof(Create)} : Blog post created.");

                    return RedirectToAction("AdminBlog", "Blog", new { clientId = serviceCallClientId });
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(BlogController)} : {nameof(Create)} : Can't create posts : {ex.Message}");

                    return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> EditPost(string postId, string authorName)
        {
            try
            {
                var serviceCall = await this.administratedBlogPostService.GetSinglePost(postId, authorName);

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(EditPost)} : Getting single blog post done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(EditPost)} : Can't create posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost([Bind("Header", "Image", "Content", "AuthorName", "PostId")]AdministratedPostViewModel model)
        {
            try
            {
                var serviceCall = await this.administratedBlogPostService.EditPost(model.PostId, model.AuthorName, model.Header, model.Image, model.Content);

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(EditPost)} : Editing single blog post done.");

                var returnModel = this.modelFactory.Create(serviceCall);

                return View(returnModel);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(EditPost)} : Can't edit posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> DeletePost(string postId, string authorName)
        {
            try
            {
                var serviceCall = await this.administratedBlogPostService.GetSinglePost(postId, authorName);

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(EditPost)} : Getting single blog post done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(EditPost)} : Can't create posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost([Bind("AuthorName", "PostId")]AdministratedPostViewModel model)
        {
            try
            {
                var serviceCallClientId = await this.administratedBlogPostService.DeletePost(model.PostId, model.AuthorName);

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(DeletePost)} : Deleting single blog post done.");

                return RedirectToAction("AdminBlog", "Blog", new { clientId = serviceCallClientId });
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(DeletePost)} : Can't delete posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Post(string postId)
        {
            try
            {
                var serviceCall = await this.publicBlogPostService.GetSinglePublicPost(postId);

                this.logger.LogInformation($"{nameof(BlogController)} : {nameof(Post)} : Getting single blog post done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(BlogController)} : {nameof(EditPost)} : Can't get posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { AdminBlog = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }
    }
}