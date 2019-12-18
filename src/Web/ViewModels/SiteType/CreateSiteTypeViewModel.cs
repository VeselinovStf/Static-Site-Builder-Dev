using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.SiteType
{
    public class CreateSiteTypeViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string ClientId { get; set; }

        public string BuildInType { get; set; }

        public string TemplateName { get; set; }

        [Display(Name = "Card API Key")]
        public string CardApiKey { get; set; }

        [Display(Name = "Card Service Gate")]
        public string CardServiceGate { get; set; }

        [Display(Name = "Hosting Service Gate")]
        public string HostingServiceGate { get; set; }

        [Display(Name = "Repository Name")]
        public string Repository { get; set; }

        public string SiteTypeId { get; set; }
    }
}