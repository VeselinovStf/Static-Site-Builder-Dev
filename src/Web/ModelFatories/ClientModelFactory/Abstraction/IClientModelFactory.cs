using Infrastructure.Blog.DTOs;
using System.Collections.Generic;
using Web.ViewModels.HomeArea;

namespace Web.ModelFatories.ClientModelFactory.Abstraction
{
    public interface IClientModelFactory
    {
        HomeAreaViewModel Create(IEnumerable<ClientPostDTO> serviceCall, string clientId, bool inTutorial);
    }
}