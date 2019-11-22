namespace Web.ViewModels.SiteType
{
    public class SiteTypeEditViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string ClientId { get; set; }

        public string BuildInType { get; set; }

        public string NewProjectLocation { get; set; }

        public string TemplateLocation { get; set; }

        public string CardApiKey { get; set; }

        public string CardServiceGate { get; set; }

        public string HostingServiceGate { get; set; }

        public string Repository { get; set; }

        public bool IsLaunched { get; set; }
    }
}