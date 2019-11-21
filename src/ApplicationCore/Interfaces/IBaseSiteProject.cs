using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBaseSiteProject
    {
        string NewProjectLocation { get; set; }

        string TemplateLocation { get; set; }

        string ClientId { get; set; }

        LaunchConfig LaunchingConfig { get; set; }
    }
}