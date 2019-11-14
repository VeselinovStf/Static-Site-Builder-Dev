using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Messages
{
    public class MessageViewModel
    {
        public string Id { get; set; }
        public string ClientOwnerId { get; set; }
        public string From { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string To { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Subject { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Text { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsDraft { get; set; }

        public bool IsTrash { get; set; }

        public bool IsNew { get; set; }
    }
}