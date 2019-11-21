using Infrastructure.ClientProjects.DTOs;
using System.Collections.Generic;
using Web.ViewModels.Project;

namespace Web.ModelFatories.ProjectsModelFactory.Abstraction
{
    public interface IProjectsModelFactory
    {
        ProjectsListViewModel Create(IEnumerable<ClientProjectDTO> serviceCall, string clientId, bool walkTry);
    }
}