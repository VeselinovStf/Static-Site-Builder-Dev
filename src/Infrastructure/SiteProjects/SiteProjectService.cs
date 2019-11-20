using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Identity;
using Infrastructure.SiteProjects.DTOs;
using Infrastructure.SiteProjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SiteProjects
{
    public class SiteProjectService : ISiteProjectService<SiteProjectDTO>
    {
        //private readonly IAppSiteProjectsService<Project> appSiteProjectService;
        private readonly IAppUserManager<ApplicationUser> userManager;

        public SiteProjectService(
            //IAppSiteProjectsService<Project> appSiteProjectService,
            IAppUserManager<ApplicationUser> userManager)
        {
            //this.appSiteProjectService = appSiteProjectService ?? throw new ArgumentNullException(nameof(appSiteProjectService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IEnumerable<SiteProjectDTO>> GetAllAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteProjectService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
                var client = await this.userManager.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                    client, $"{nameof(SiteProjectService)} : {nameof(GetAllAsync)} : {nameof(client)} : Can't find client with this id");

                var project = client.Project;

                var siteTypes = new List<SiteProjectDTO>(project.StoreSiteTypes.Select(p => new SiteProjectDTO()
                {
                    Name = p.Name,
                    Description = p.Description,
                    CreatedOn = p.CreatedOn,
                    ModifiedOn = p.ModifiedOn,
                    ItemsCount = p.Products.Count(),
                    ProjectType = "Store Type"
                }));

                siteTypes.AddRange(new List<SiteProjectDTO>(project.BlogSiteTypes.Select(p => new SiteProjectDTO()
                {
                    Name = p.Name,
                    Description = p.Description,
                    CreatedOn = p.CreatedOn,
                    ModifiedOn = p.ModifiedOn,
                    ItemsCount = p.BlogPosts.Count(),
                    ProjectType = "Blog Type"
                })));

                return siteTypes.OrderBy(t => t.ModifiedOn);
            }
            catch (Exception ex)
            {
                throw new SiteProjectServiceGetAllException($"{nameof(SiteProjectServiceGetAllException)} : Can't Get all clients Site projects : {ex.Message}");
            }
        }
    }
}