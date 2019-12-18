using Infrastructure.ClientProjects.DTOs;
using System.Collections.Generic;
using System.Linq;
using Web.ModelFatories.ProjectsModelFactory.Abstraction;
using Web.ViewModels.Project;


namespace Web.ModelFatories.ProjectsModelFactory
{
    public class ProjectsModelFactory : IProjectsModelFactory
    {
        public ProjectsListViewModel Create(IEnumerable<ClientProjectDTO> serviceCall, string clientId)
        {
            return new ProjectsListViewModel()
            {
                ClientId = clientId,
               
                Projects = new List<SiteProjectViewModel>(serviceCall.Select(p => new SiteProjectViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedOn = p.CreatedOn,
                    ItemsCount = p.ItemsCount,
                    ModifiedOn = p.ModifiedOn,
                    ProjectType = p.ProjectType
                })),
            };
        }
    }
}