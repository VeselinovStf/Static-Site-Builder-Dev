﻿using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities.BlogSiteTypeEntities
{
    public class BlogTypeSite : BaseWidget,IAggregateRoot
    {
       

        private readonly List<BlogPost> _blogPosts = new List<BlogPost>();

        public IReadOnlyCollection<BlogPost> BlogPosts
        {
            get
            {
                return new List<BlogPost>(_blogPosts.AsReadOnly());
            }
        }

        //Build in site type config
        public SiteTypesEnum SiteTypeSpecification
        {
            get
            {
                return SiteTypesEnum.BlogType;
            }
        }

        //Build in widjets
       

    

        public void AddBlogPost(string name, string description,
            string header, string image, string content,
            DateTime pubDate, string authorName)
        {
            _blogPosts.Add(new BlogPost()
            {
                Name = name,
                Description = description,
                AuthorName = authorName,
                Content = content,
                Header = header,
                Image = image,
                PubDate = pubDate,
            });
        }
    }
}