using Infrastructure.Blog.DTOs;
using System.Collections.Generic;
using System.Linq;
using Web.ModelFatories.ClientModelFactory.Abstraction;
using Web.ViewModels.HomeArea;

namespace Web.ModelFatories.ClientModelFactory
{
    public class ClientModelFactory : IClientModelFactory
    {
        public HomeAreaViewModel Create(IEnumerable<ClientPostDTO> serviceCall, string clientId, bool inTutorial)
        {
            return new HomeAreaViewModel()
            {
                ClientId = clientId,
                Tutorial = inTutorial,
                HomePosts = new List<HomeAreaPostsViewModel>(serviceCall.Select(p => new HomeAreaPostsViewModel()
                {
                    AuthorName = p.AuthorName,
                    CommentsCount = p.Comments.Count(),
                    Content = p.Content,
                    Header = p.Header,
                    Image = p.Image,
                    PostId = p.PostId,
                    PubDate = p.PubDate
                }))
            };
        }
    }
}