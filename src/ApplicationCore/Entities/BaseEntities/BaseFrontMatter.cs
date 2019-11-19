using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.BaseEntities
{
    public class BaseFrontMatter : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Front matter layout")]
        public string Layout { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Front Matter permalink")]
        public string PermaLink { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Front Matter Include")]
        public string Include { get; set; }
    }
}