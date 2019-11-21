using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities.SiteType
{
    public class SiteType : DescriptiveEntity
    {
        public SiteTypesEnum Type { get; set; }
    }
}