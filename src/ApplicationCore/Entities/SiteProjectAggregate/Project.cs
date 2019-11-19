using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SiteProjectAggregate
{
    public class Project : BaseEntity, IAggregateRoot
    {
        private readonly List<StoreTypeSite> _storeSiteType = new List<StoreTypeSite>();

        private readonly List<BlogTypeSite> _blogSiteType = new List<BlogTypeSite>();

        public string ClientId { get; set; }

        public IReadOnlyCollection<StoreTypeSite> StoreSites
        {
            get
            {
                return new List<StoreTypeSite>(_storeSiteType.AsReadOnly());
            }
        }

        public IReadOnlyCollection<BlogTypeSite> BlogSites
        {
            get
            {
                return new List<BlogTypeSite>(_blogSiteType.AsReadOnly());
            }
        }

        public void AddStoreTypeSite(string projectName, string description, string newProjectLocation, string templateLocation,
            string clientId)
        {
            _storeSiteType.Add(new StoreTypeSite()
            {
                Name = projectName,
                Description = description,
                NewProjectLocation = newProjectLocation,
                TemplateLocation = templateLocation,
                ClientId = clientId
            });
        }
    }
}