using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.BaseEntities
{
    public class BaseWidget : DescriptiveEntity, IBaseSiteProject
    {
        public string TemplateName { get ; set ; }
        public string ClientId { get; set; }
        public string LaunchingConfigId { get; set; }
        public LaunchConfig LaunchingConfig { get; set; }
        public string ProjectId { get; set; }
        public ICollection<SiteWidget> SiteUsedWidgets { get; set; }
    }
}
