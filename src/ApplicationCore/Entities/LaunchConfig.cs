using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities
{
    public class LaunchConfig : BaseEntity
    {
        public bool IsLaunched { get; set; }
        public bool IsPushed { get; set; }

        public string CardApiKey { get; set; }

        public string CardServiceGate { get; set; }

        public string HostingServiceGate { get; set; }

        public string Repository { get; set; }

        public string SiteTypeId { get; set; }
    }
}