using Infrastructure.Blog.DTOs;
using System.Collections.Generic;
using Web.ViewModels.HomeArea;

namespace Web.ModelFatories.AdminModelFactory.Abstraction
{
    public interface IAdminModelFactory
    {
        HomeAreaViewModel Create(IEnumerable<AdministratedPostDTO> serviceCall, string clientId);
    }
}