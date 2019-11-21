using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.ClientProjects.DTOs;
using Infrastructure.ClientProjects.Exceptions;
using Infrastructure.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ClientProjects
{
    public class ClientProjectService : IClientProjectService<ClientProjectDTO>
    {
        private readonly IAppProjectsService<Project> appProjectService;

        public ClientProjectService(
            IAppProjectsService<Project> appProjectService)
        {
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
        }

        public async Task<IEnumerable<ClientProjectDTO>> GetAllAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(ClientProjectService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
                var project = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    project, $"{nameof(ClientProjectService)} : {nameof(GetAllAsync)} : {nameof(project)} : Can't find client projects with this id");

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