﻿using Infrastructure.Blog.DTOs;
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

        public AdministratedPostViewModel Create(AdministratedPostDTO inputModel)
        {
            return new AdministratedPostViewModel()
            {
                AuthorName = inputModel.AuthorName,
                Comments = inputModel.Comments.Select(c => new AdministratedCommentsViewModel()
                {
                    AuthorId = c.AuthorId,
                    PubDate = c.PubDate,
                    AuthorName = c.AuthorName,
                    Content = c.Content
                }),
                Content = inputModel.Content,
                Header = inputModel.Header,
                Image = inputModel.Image,
                PostId = inputModel.PostId,
                PubDate = inputModel.PubDate
            };
        }

        public PublicPostViewModel Create(PublicPostDTO inputModel)
        {
            return new PublicPostViewModel()
            {
                AuthorName = inputModel.AuthorName,
                Content = inputModel.Content,
                Header = inputModel.Header,
                Image = inputModel.Image,
                PostId = inputModel.PostId,
                PubDate = inputModel.PubDate
            };
        }

        public ClientPostViewModel Create(ClientPostDTO serviceCall)
        {
            return new ClientPostViewModel()
            {
                AuthorName = serviceCall.AuthorName,
                Comments = serviceCall.Comments.Select(c => new ClientCommentViewModel()
                {
                    AuthorId = c.AuthorId,
                    PubDate = c.PubDate,
                    AuthorName = c.AuthorName,
                    Content = c.Content
                }),
                Content = serviceCall.Content,
                Header = serviceCall.Header,
                Image = serviceCall.Image,
                PostId = serviceCall.PostId,
                PubDate = serviceCall.PubDate
            };
        }
    }
}