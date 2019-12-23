using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Site;
using Web.ViewModels.SiteRendering;
using Web.ViewModels.Widgets.ProductWidget;

namespace Web.Controllers
{
    [Authorize]
    public class ProductWidgetController : Controller
    {
        private readonly IAppLogger<ProductWidgetController> logger;

        public ProductWidgetController(
            IAppLogger<ProductWidgetController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Product(string widgetId, string clientId, string templateName, string siteTypeId)
        {
            var model = new ProductsDisplayListViewModel()
            {
                Products = new List<ProductsSingleSimpleViewModel>(),
                ProjectId = siteTypeId,
                SiteRendering = new SiteRenderingViewModel()
                {
                    ClientId = clientId,
                    SiteTypeId = siteTypeId,
                    PresentationLink = "nope",
                    TemplateName = templateName
                }
            };
       
            return View(model);
        }

        //NOTE: When client neat project -> search in StoreTypeSite table for id
        public IActionResult AddProduct(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                logger.LogWarning("Products - Add Product - Get - Id is null/empty");

                return NotFound();
            }

            var model = new ProductsDisplayDetailsViewModel()
            {
                ProjectId = projectId
            };

            return View(model);
        }

    }
}
