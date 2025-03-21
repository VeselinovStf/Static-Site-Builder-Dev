﻿using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using System;

namespace ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate
{
    public class BlogPost : DescriptiveEntity
    {
        public string Header { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public DateTime PubDate { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string ProjectId { get; set; }

        public BlogTypeSite Project { get; set; }

        public BlogPostFrontMatter FrontMatter { get; set; }
    }
}