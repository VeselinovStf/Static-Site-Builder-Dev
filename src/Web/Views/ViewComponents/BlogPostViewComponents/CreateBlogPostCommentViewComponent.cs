using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.ViewComponentModels;

namespace Web.Views.ViewComponents.BlogPostViewComponents
{
    public class CreateBlogPostCommentViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IAppLogger<CreateBlogPostCommentViewComponent> logger;

        public CreateBlogPostCommentViewComponent(
            IHttpContextAccessor httpContext,
            IAccountService<ApplicationUser> accountService,
            IAppLogger<CreateBlogPostCommentViewComponent> logger)
        {
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IViewComponentResult> InvokeAsync(string postId)
        {
            try
            {
                var user = this.httpContext.HttpContext.User;

                if (this.accountService.IsSignedIn(user))
                {
                    var client = await this.accountService.RetrieveUserAsync(user);

                    var model = new CreateBlogPostCommentViewModel()
                    {
                        AuthorId = client.Id,
                        AuthorName = client.UserName,
                        PostId = postId
                    };

                    return View("CreateBlogPostComment", model);
                }
                else
                {
                    return View("~/Views/Account/Login");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(CreateBlogPostCommentViewComponent)} : {nameof(InvokeAsync)} : Exception : {ex.Message}");

                var model = new ErrorViewModel()
                {
                    Message = "Some error accure.. Please contact site support for help."
                };

                return View("DefaultError", model);
            }
        }
    }
}