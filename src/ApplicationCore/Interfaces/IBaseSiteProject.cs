using ApplicationCore.Entities;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IBaseSiteProject
    {
        string TemplateLocation { get; set; }

        string ClientId { get; set; }

        string LaunchingConfigId { get; set; }
        LaunchConfig LaunchingConfig { get; set; }

        string ProjectId { get; set; }

        //Build in widjets
        ICollection<Widjet> TemplateUsableWidjets { get; set; }
    }
}