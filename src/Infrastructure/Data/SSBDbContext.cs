using ApplicationCore.Entities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate;
using ApplicationCore.Entities.MessageAggregate;
using ApplicationCore.Entities.PostAggregate;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SSBDbContext : IdentityDbContext<ApplicationUser>
    {
        //Application Site Building
        public DbSet<Project> Projects { get; set; }

        public DbSet<StoreTypeSite> StoreTypeSites { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFrontMatter> ProductsFrontMatters { get; set; }
        public DbSet<BlogTypeSite> BlogTypeSites { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostFrontMatter> BlogPostFrontMatters { get; set; }
        public DbSet<ClientWidjet> ClientWidjets { get; set; }
        public DbSet<WidjetElement> WidjetElements { get; set; }
        public DbSet<LaunchConfig> LaunchConfigs { get; set; }

        //Application Messaging System
        public DbSet<Message> Messages { get; set; }

        public DbSet<MailBox> MailBoxes { get; set; }

        //Application Blog System
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public SSBDbContext(DbContextOptions<SSBDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletionRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletionRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyDeletionRules()
        {
            var entitiesForDeletion = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

            foreach (var entry in entitiesForDeletion)
            {
                var entity = (IDeletable)entry.Entity;
                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModifiable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IModifiable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}