using ApplicationCore.Entities;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IBaseSiteProject
    {
        string TemplateName { get; set; }

        string ClientId { get; set; }

        string LaunchingConfigId { get; set; }
        LaunchConfig LaunchingConfig { get; set; }

        string ProjectId { get; set; }

        //Build in widjets
        ICollection<Widget> TemplateUsableWidjets { get; set; }
        ICollection<Widget> SiteUsedWidgets { get; set; }
    }
}