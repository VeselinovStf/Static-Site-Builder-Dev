using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Blog
{
    public class CreatePostViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Header")]
        public string Header { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Post Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string AuthorName { get; set; }
    }
}