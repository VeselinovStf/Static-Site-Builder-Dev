using Infrastructure.Blog.DTOs;
using System.Collections.Generic;
using System.Linq;
using Web.ModelFatories.BlogModelFactory.Abstraction;
using Web.ViewModels.Blog;

namespace Web.ModelFatories.BlogModelFactory
{
    public class BlogModelFactory : IBlogModelFactory
    {
        public IEnumerable<PublicPostViewModel> Create(IEnumerable<PublicPostDTO> inputModel)
        {
            return inputModel.Count() > 0 ? new List<PublicPostViewModel>(inputModel.Select(p => new PublicPostViewModel()
            {
                Header = p.Header,
                Image = p.Image,
                PubDate = p.PubDate,
                AuthorName = p.AuthorName,
                Content = p.Content,
                PostId = p.PostId
            })) : new List<PublicPostViewModel>();
        }

        public IEnumerable<AdministratedPostViewModel> Create(IEnumerable<AdministratedPostDTO> inputModel)
        {
            return inputModel.Count() > 0 ? new List<AdministratedPostViewModel>(inputModel.Select(p => new AdministratedPostViewModel()
            {
                Header = p.Header,
                Image = p.Image,
                PubDate = p.PubDate,
                AuthorName = p.AuthorName,
                Content = p.Content,
                PostId = p.PostId,
                Comments = p.Content.Count() > 0 ? new List<AdministratedCommentsViewModel>(p.Comments.Select(c => new AdministratedCommentsViewModel()
                {
                    AuthorName = c.AuthorName,
                    AuthorId = c.AuthorId,
                    Content = c.Content,
                    PubDate = c.PubDate
                })) : new List<AdministratedCommentsViewModel>()
            })) : new List<AdministratedPostViewModel>();
        }
    }
}