using ApplicationCore.Interfaces;
using Infrastructure.ClientProjects.DTOs;
using Infrastructure.ClientProjects.Exceptions;
using Infrastructure.Guard;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ClientProjects
{
    public class ClientProjectService : IClientProjectService<ClientProjectDTO>
    {
        //private readonly IAppSiteProjectsService<Project> appSiteProjectService;
        private readonly IAppUserManager<ApplicationUser> userManager;

        public ClientProjectService(
            //IAppSiteProjectsService<Project> appSiteProjectService,
            IAppUserManager<ApplicationUser> userManager)
        {
            //this.appSiteProjectService = appSiteProjectService ?? throw new ArgumentNullException(nameof(appSiteProjectService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IEnumerable<ClientProjectDTO>> GetAllAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(ClientProjectService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
                var client = await this.userManager.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                    client, $"{nameof(ClientProjectService)} : {nameof(GetAllAsync)} : {nameof(client)} : Can't find client with this id");

                var project = client.Project;

                var siteTypes = new List<ClientProjectDTO>(project.StoreSiteTypes.Select(p => new ClientProjectDTO()
                {
                    Name = p.Name,
                    Description = p.Description,
                    CreatedOn = p.CreatedOn,
                    ModifiedOn = p.ModifiedOn,
                    ItemsCount = p.Products.Count(),
                    ProjectType = "Store Type"
                }));

                siteTypes.AddRange(new List<ClientProjectDTO>(project.BlogSiteTypes.Select(p => new ClientProjectDTO()
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
                throw new ClientProjectServiceGetAllException($"{nameof(ClientProjectServiceGetAllException)} : Can't Get all clients Site projects : {ex.Message}");
            }
        }
    }
}