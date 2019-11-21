using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.BlogSiteTypeEntities
{
    public class BlogTypeSite : DescriptiveEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "New Project Location")]
        public string NewProjectLocation { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Template Location")]
        public string TemplateLocation { get; set; }

        public string ClientId { get; set; }

        public LaunchConfig LaunchingConfig { get; set; }

        private readonly List<BlogPost> _blogPosts = new List<BlogPost>();

        public IReadOnlyCollection<BlogPost> BlogPosts
        {
            get
            {
                return new List<BlogPost>(_blogPosts.AsReadOnly());
            }
        }

        //Build in site type config
        public SiteTypes SiteTypeSpecification
        {
            get
            {
                return SiteTypes.BlogType;
            }
        }

        //Build in widjets
        public ICollection<Widjet> TemplateUsableWidjets { get; set; }

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