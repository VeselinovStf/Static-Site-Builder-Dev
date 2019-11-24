using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
                return new List<StoreTypeSite>(_storeSiteTypes.AsReadOnly().Where(b => !b.IsDeleted));
            }
        }

        public IReadOnlyCollection<BlogTypeSite> BlogSiteTypes
        {
            get
            {
                return new List<BlogTypeSite>(_blogSiteTypes.AsReadOnly().Where(b => !b.IsDeleted));
            }
        }

        public void AddStoreTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            var newStoreId = Guid.NewGuid().ToString();
            _storeSiteTypes.Add(new StoreTypeSite()
            {
                Id = newStoreId,
                Name = name,
                Description = description,
                TemplateLocation = Environment.CurrentDirectory + "\\BuildInTemplates\\StoreTemplates",
                ClientId = clientId,
                LaunchingConfig = new LaunchConfig()
                {
                    SiteTypeId = newStoreId,
                    CardApiKey = cardApiKey,
                    CardServiceGate = cardServiceGate,
                    HostingServiceGate = hostingServiceGate,
                    RepositoryName = name.Replace(" ", ""),
                    RepositoryId = repository,
                    IsLaunched = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                }
            });
        }

        public void AddBlogTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            var newBlogId = Guid.NewGuid().ToString();
            _blogSiteTypes.Add(new BlogTypeSite()
            {
                Id = newBlogId,
                Name = name,
                Description = description,
                TemplateLocation = Environment.CurrentDirectory + "\\BuildInTemplates\\BlogTemplates\\Test",
                ClientId = clientId,
                LaunchingConfig = new LaunchConfig()
                {
                    SiteTypeId = newBlogId,
                    CardApiKey = cardApiKey,
                    CardServiceGate = cardServiceGate,
                    HostingServiceGate = hostingServiceGate,
                    RepositoryName = name.Replace(" ", ""),
                    RepositoryId = repository,
                    IsLaunched = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                }
            });
        }
    }
}