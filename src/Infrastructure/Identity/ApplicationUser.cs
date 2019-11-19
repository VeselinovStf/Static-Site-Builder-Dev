using ApplicationCore.Entities.MessageAggregate;
using ApplicationCore.Entities.PostAggregate;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IDeletable, IModifiable
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public MailBox MailBox { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<ClientWidjet> ClientWidjets { get; set; }
    }
}