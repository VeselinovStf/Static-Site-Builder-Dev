using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SitesTemplates
{
    public class SiteTemplate : DescriptiveEntity, IAggregateRoot
    {
        private List<SiteTemplateElement> _siteTemplateElements = new List<SiteTemplateElement>();

        public IReadOnlyCollection<SiteTemplateElement> SiteTemplateElements
        {
            get
            {
                return new List<SiteTemplateElement>(_siteTemplateElements.AsReadOnly());
            }
        }

        public string SiteTypeId { get; set; }

        public SiteType.SiteType SiteType { get; set; }

        public void AddElement(string filePath, string fileContent)
        {
            this._siteTemplateElements.Add(new SiteTemplateElement()
            {
                FileContent = fileContent,
                FilePath = filePath
            });
        }
    }
}