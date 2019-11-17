using Infrastructure.Blog.DTOs;
using System.Collections.Generic;
using Web.ViewModels.Blog;

namespace Web.ModelFatories.BlogModelFactory.Abstraction
{
    public interface IBlogModelFactory
    {
        IEnumerable<PublicPostViewModel> Create(IEnumerable<PublicPostDTO> inputModel);

        IEnumerable<AdministratedPostViewModel> Create(IEnumerable<AdministratedPostDTO> inputModel);

        AdministratedPostViewModel Create(AdministratedPostDTO inputModel);

        PublicPostViewModel Create(PublicPostDTO inputModel);
    }
}