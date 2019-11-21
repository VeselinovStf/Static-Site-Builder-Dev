using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SiteProjectAggregate
{
    public class Project : BaseEntity, IAggregateRoot
    {
        private readonly List<StoreTypeSite> _storeSiteTypes = new List<StoreTypeSite>();

        private readonly List<BlogTypeSite> _blogSiteTypes = new List<BlogTypeSite>();

        public ICollection<WidjetElement> AvailibleWidjets { get; set; }

        public ICollection<WidjetElement> UsedWidjets { get; set; }

        public string ClientId { get; set; }

        public IReadOnlyCollection<StoreTypeSite> StoreSiteTypes
        {
            get
            {
                return new List<StoreTypeSite>(_storeSiteTypes.AsReadOnly());
            }
        }

        public IReadOnlyCollection<BlogTypeSite> BlogSiteTypes
        {
            get
            {
                return new List<BlogTypeSite>(_blogSiteTypes.AsReadOnly());
            }
        }

        public void AddStoreTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            _storeSiteTypes.Add(new StoreTypeSite()
            {
                Name = name,
                Description = description,
                NewProjectLocation = newProjectLocation,
                TemplateLocation = templateLocation,
                ClientId = clientId,
            });
        }

        public void AddBlogTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            _blogSiteTypes.Add(new BlogTypeSite()
            {
                Name = name,
                Description = description,
                NewProjectLocation = newProjectLocation,
                TemplateLocation = templateLocation,
                ClientId = clientId,
            });
        }
    }
}