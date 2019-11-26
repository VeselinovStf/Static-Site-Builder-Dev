using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.SitesTemplates
{
    public class SiteTemplateElement : BaseEntity, IAggregateRoot
    {
        public string FilePath { get; set; }

        public string FileContent { get; set; }

        public string SiteTemplateId { get; set; }
    }
}