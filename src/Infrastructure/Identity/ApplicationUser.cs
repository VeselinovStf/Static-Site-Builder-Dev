using ApplicationCore.Entities.MessageAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IDeletable, IModifiable
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public MailBox MailBox { get; set; }
    }
}