using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.BaseEntities
{
    public class BaseSiteProject : DescriptiveEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "New Project Location")]
        public string NewProjectLocation { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Template Location")]
        public string TemplateLocation { get; set; }

        public string ClientId { get; set; }

        public LaunchConfig LaunchingConfig { get; set; }

        public ICollection<WidjetElement> AvailibleWidjets { get; set; }

        public ICollection<WidjetElement> UsedWidjets { get; set; }
    }
}