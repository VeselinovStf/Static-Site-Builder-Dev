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

        public string RepositoryName { get; set; }

        public string RepositoryId { get; set; }

        public string HostingId { get; set; }

        public string SiteTypeId { get; set; }
    }
}