using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SitesTemplates
{
    public class SiteTemplate : DescriptiveEntity, IAggregateRoot, ISelingEntity
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

        public bool IsFree { get; set; }

        private decimal UnitPrice { get; set; }
        public decimal Price
        {
            get
            {
                if (this.IsFree)
                {
                    return 0m;
                }
                else
                {
                    return this.UnitPrice;
                }
            }
            set
            {
                this.UnitPrice = value;
            }
        }

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