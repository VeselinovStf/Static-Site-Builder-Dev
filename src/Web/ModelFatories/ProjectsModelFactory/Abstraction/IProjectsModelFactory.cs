using Infrastructure.SiteProjects.DTOs;
using System.Collections.Generic;
using Web.ViewModels.Project;

namespace Web.ModelFatories.ProjectsModelFactory.Abstraction
{
    public interface IProjectsModelFactory
    {
        ProjectsListViewModel Create(IEnumerable<SiteProjectDTO> serviceCall, string clientId, bool walkTry);
    }
}