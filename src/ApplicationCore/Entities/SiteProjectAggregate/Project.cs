using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SiteProjectAggregate
{
    public class Project : BaseEntity, IAggregateRoot
    {
        private readonly List<StoreTypeSite> _storeSiteTypes = new List<StoreTypeSite>();

        private readonly List<BlogTypeSite> _blogSiteTypes = new List<BlogTypeSite>();

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

        public void AddStoreTypeSite(string projectName, string description, string newProjectLocation, string templateLocation,
            string clientId)
        {
            _storeSiteTypes.Add(new StoreTypeSite()
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