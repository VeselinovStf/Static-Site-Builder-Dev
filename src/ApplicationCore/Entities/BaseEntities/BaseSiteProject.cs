using ApplicationCore.Entities.WidjetsEntity;
using System.Collections.Generic;

namespace ApplicationCore.Entities.BaseEntities
{
    public class BaseSiteProject : DescriptiveEntity
    {
        public string NewProjectLocation { get; set; }

        public string TemplateLocation { get; set; }

        public string ClientId { get; set; }

        public LaunchConfig LaunchingConfig { get; set; }

        public ICollection<WidjetElement> AvailibleWidjets { get; set; }
    }
}