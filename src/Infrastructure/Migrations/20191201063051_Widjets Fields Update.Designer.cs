﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SSBDbContext))]
    [Migration("20191201063051_Widjets Fields Update")]
    partial class WidjetsFieldsUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Entities.BlogSiteTypeEntities.BlogTypeSite", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LaunchingConfigId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProjectId");

                    b.Property<string>("TemplateName");

                    b.HasKey("Id");

                    b.HasIndex("LaunchingConfigId");

                    b.HasIndex("ProjectId");

                    b.ToTable("BlogTypeSites");
                });

            modelBuilder.Entity("ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate.BlogPost", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Header");

                    b.Property<string>("Image");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProjectId");

                    b.Property<DateTime>("PubDate");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate.BlogPostFrontMatter", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogPostId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Include")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Layout")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PermaLink")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId")
                        .IsUnique()
                        .HasFilter("[BlogPostId] IS NOT NULL");

                    b.ToTable("BlogPostFrontMatters");
                });

            modelBuilder.Entity("ApplicationCore.Entities.LaunchConfig", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CardApiKey");

                    b.Property<string>("CardServiceGate");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("HostingId");

                    b.Property<string>("HostingServiceGate");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsLaunched");

                    b.Property<bool>("IsPushed");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("RepositoryId");

                    b.Property<string>("RepositoryName");

                    b.Property<string>("SiteTypeId");

                    b.HasKey("Id");

                    b.ToTable("LaunchConfigs");
                });

            modelBuilder.Entity("ApplicationCore.Entities.MessageAggregate.MailBox", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.ToTable("MailBoxes");
                });

            modelBuilder.Entity("ApplicationCore.Entities.MessageAggregate.Message", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnName("Sender")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDraft");

                    b.Property<bool>("IsNew");

                    b.Property<bool>("IsSent");

                    b.Property<bool>("IsTrash");

                    b.Property<string>("MailBoxId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime>("SendDate")
                        .HasColumnName("Send Date Of Message");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnName("Subject")
                        .HasMaxLength(100);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("Content")
                        .HasMaxLength(2000);

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnName("To")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("MailBoxId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ApplicationCore.Entities.PostAggregate.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("AuthorId");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PostId");

                    b.Property<DateTime>("PubDate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ApplicationCore.Entities.PostAggregate.Post", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("AuthorId");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Header");

                    b.Property<string>("Image");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime>("PubDate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SiteProjectAggregate.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SiteType.SiteType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("SiteTypes");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SitesTemplates.SiteTemplate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SiteTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SiteTypeId");

                    b.ToTable("SiteTemplates");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SitesTemplates.SiteTemplateElement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("FileContent")
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<string>("FilePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("SiteTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("SiteTemplateId");

                    b.ToTable("SiteTemplateElements");
                });

            modelBuilder.Entity("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<string>("Brand");

                    b.Property<string>("Category");

                    b.Property<string>("Collection");

                    b.Property<string>("Color");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<bool>("DealOfTheDayProduct");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Details");

                    b.Property<string>("DetailsLink");

                    b.Property<decimal>("DiscountProcent")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateTime>("DiscountTimer");

                    b.Property<string>("Gender");

                    b.Property<bool>("Hot");

                    b.Property<string>("Image");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LatestNewCollectionProduct");

                    b.Property<bool>("LatestProduct");

                    b.Property<string>("Link");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("PickedForYou");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<bool>("ProductOfTheDay");

                    b.Property<string>("ProjectId");

                    b.Property<bool>("Publish");

                    b.Property<double>("Rating");

                    b.Property<string>("Size");

                    b.Property<bool>("State");

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.ProductFrontMatter", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Include")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Layout")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PermaLink")
                        .HasMaxLength(100);

                    b.Property<string>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL");

                    b.ToTable("ProductsFrontMatters");
                });

            modelBuilder.Entity("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.StoreTypeSite", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LaunchingConfigId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProjectId");

                    b.Property<string>("TemplateName");

                    b.HasKey("Id");

                    b.HasIndex("LaunchingConfigId");

                    b.HasIndex("ProjectId");

                    b.ToTable("StoreTypeSites");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WidjetsEntityAggregate.ClientWidjet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ClientId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ClientWidjets");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WidjetsEntityAggregate.Widjet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogTypeSiteId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("Dependency");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Functionality");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFree");

                    b.Property<bool>("IsOn");

                    b.Property<string>("Key");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("SiteTypeSpecification");

                    b.Property<string>("StoreTypeSiteId");

                    b.Property<int>("SystemName");

                    b.Property<string>("UsebleSiteTypeId");

                    b.Property<int>("Version");

                    b.Property<double>("Votes");

                    b.Property<string>("WidjetElementId");

                    b.HasKey("Id");

                    b.HasIndex("BlogTypeSiteId");

                    b.HasIndex("StoreTypeSiteId");

                    b.HasIndex("UsebleSiteTypeId");

                    b.HasIndex("WidjetElementId");

                    b.ToTable("Widjets");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WidjetsEntityAggregate.WidjetElement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientWidjetId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("ClientWidjetId");

                    b.ToTable("WidjetElements");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MailBoxId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProjectId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("MailBoxId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProjectId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ApplicationCore.Entities.BlogSiteTypeEntities.BlogTypeSite", b =>
                {
                    b.HasOne("ApplicationCore.Entities.LaunchConfig", "LaunchingConfig")
                        .WithMany()
                        .HasForeignKey("LaunchingConfigId");

                    b.HasOne("ApplicationCore.Entities.SiteProjectAggregate.Project")
                        .WithMany("BlogSiteTypes")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate.BlogPost", b =>
                {
                    b.HasOne("ApplicationCore.Entities.BlogSiteTypeEntities.BlogTypeSite", "Project")
                        .WithMany("BlogPosts")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate.BlogPostFrontMatter", b =>
                {
                    b.HasOne("ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate.BlogPost", "BlogPost")
                        .WithOne("FrontMatter")
                        .HasForeignKey("ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate.BlogPostFrontMatter", "BlogPostId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.MessageAggregate.Message", b =>
                {
                    b.HasOne("ApplicationCore.Entities.MessageAggregate.MailBox", "MailBox")
                        .WithMany("Messages")
                        .HasForeignKey("MailBoxId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.PostAggregate.Comment", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("ApplicationCore.Entities.PostAggregate.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.PostAggregate.Post", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany("Posts")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SitesTemplates.SiteTemplate", b =>
                {
                    b.HasOne("ApplicationCore.Entities.SiteType.SiteType", "SiteType")
                        .WithMany("SiteTemplates")
                        .HasForeignKey("SiteTypeId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.SitesTemplates.SiteTemplateElement", b =>
                {
                    b.HasOne("ApplicationCore.Entities.SitesTemplates.SiteTemplate")
                        .WithMany("SiteTemplateElements")
                        .HasForeignKey("SiteTemplateId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.Product", b =>
                {
                    b.HasOne("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.StoreTypeSite", "Project")
                        .WithMany("Products")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.ProductFrontMatter", b =>
                {
                    b.HasOne("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.Product", "Product")
                        .WithOne("FrontMatter")
                        .HasForeignKey("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.ProductFrontMatter", "ProductId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.StoreTypeSite", b =>
                {
                    b.HasOne("ApplicationCore.Entities.LaunchConfig", "LaunchingConfig")
                        .WithMany()
                        .HasForeignKey("LaunchingConfigId");

                    b.HasOne("ApplicationCore.Entities.SiteProjectAggregate.Project")
                        .WithMany("StoreSiteTypes")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WidjetsEntityAggregate.ClientWidjet", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany("ClientWidjets")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WidjetsEntityAggregate.Widjet", b =>
                {
                    b.HasOne("ApplicationCore.Entities.BlogSiteTypeEntities.BlogTypeSite")
                        .WithMany("TemplateUsableWidjets")
                        .HasForeignKey("BlogTypeSiteId");

                    b.HasOne("ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate.StoreTypeSite")
                        .WithMany("TemplateUsableWidjets")
                        .HasForeignKey("StoreTypeSiteId");

                    b.HasOne("ApplicationCore.Entities.SiteType.SiteType", "UsebleSiteType")
                        .WithMany("UsebleWidjets")
                        .HasForeignKey("UsebleSiteTypeId");

                    b.HasOne("ApplicationCore.Entities.WidjetsEntityAggregate.WidjetElement")
                        .WithMany("Widjets")
                        .HasForeignKey("WidjetElementId");
                });

            modelBuilder.Entity("ApplicationCore.Entities.WidjetsEntityAggregate.WidjetElement", b =>
                {
                    b.HasOne("ApplicationCore.Entities.WidjetsEntityAggregate.ClientWidjet", "ClientWidjet")
                        .WithMany("ClientWidjets")
                        .HasForeignKey("ClientWidjetId");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.HasOne("ApplicationCore.Entities.MessageAggregate.MailBox", "MailBox")
                        .WithMany()
                        .HasForeignKey("MailBoxId");

                    b.HasOne("ApplicationCore.Entities.SiteProjectAggregate.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
