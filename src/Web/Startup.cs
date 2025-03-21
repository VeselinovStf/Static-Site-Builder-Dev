﻿using ApplicationCore.Entities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Entities.Wallet;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.AdminSiteTypes;
using Infrastructure.AdminSiteTypes.DTOs;
using Infrastructure.AdminSiteTypeUsebleWidgets;
using Infrastructure.AdminSiteTypeUsebleWidgets.DTOs;
using Infrastructure.AdminSiteTypeWidgets;
using Infrastructure.AdminSiteTypeWidgets.DTOs;
using Infrastructure.Blog;
using Infrastructure.Blog.DTOs;
using Infrastructure.BuildInOptions;
using Infrastructure.ClientProjects;
using Infrastructure.ClientProjects.DTOs;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.LaunchSite;
using Infrastructure.Logging;
using Infrastructure.Messages;
using Infrastructure.Messages.DTOs;
using Infrastructure.Services.APIClientService;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.EmailSenderService;
using Infrastructure.Services.FileReader;
using Infrastructure.Services.FileTransferrer;
using Infrastructure.Services.FileTransferrer.DTOs;
using Infrastructure.Services.HostingHubConnectorService;
using Infrastructure.Services.HostingHubConnectorService.DTOs;
using Infrastructure.Services.RepoHubConnectorService;
using Infrastructure.Site;
using Infrastructure.Site.DTOs;
using Infrastructure.SiteTypes;
using Infrastructure.SiteTypes.DTOs;
using Infrastructure.Storage;
using Infrastructure.Templates;
using Infrastructure.Templates.DTOs;
using Infrastructure.Tutorial;
using Infrastructure.Wallet;
using Infrastructure.Wallet.DTOs;
using Infrastructure.Widgets;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Web.ModelFatories.AccountManageModelFactory;
using Web.ModelFatories.AccountManageModelFactory.Abstraction;
using Web.ModelFatories.AdminModelFactory;
using Web.ModelFatories.AdminModelFactory.Abstraction;
using Web.ModelFatories.AdminSiteTypesModelFactory;
using Web.ModelFatories.AdminSiteTypesModelFactory.Abstraction;
using Web.ModelFatories.AdminSiteTypeWidgetModelFactory;
using Web.ModelFatories.AdminSiteTypeWidgetModelFactory.Abstraction;
using Web.ModelFatories.AdminWidgets;
using Web.ModelFatories.AdminWidgets.Abstraction;
using Web.ModelFatories.BlogModelFactory;
using Web.ModelFatories.BlogModelFactory.Abstraction;
using Web.ModelFatories.ClientModelFactory;
using Web.ModelFatories.ClientModelFactory.Abstraction;
using Web.ModelFatories.ClientSettingsModelFactory;
using Web.ModelFatories.ClientSettingsModelFactory.Abstraction;
using Web.ModelFatories.ClientWalletModelFactory;
using Web.ModelFatories.ClientWalletModelFactory.Abstraction;
using Web.ModelFatories.MessagesModelFactory;
using Web.ModelFatories.MessagesModelFactory.Abstraction;
using Web.ModelFatories.ProjectsModelFactory;
using Web.ModelFatories.ProjectsModelFactory.Abstraction;
using Web.ModelFatories.SiteModelFactory;
using Web.ModelFatories.SiteModelFactory.Abstraction;
using Web.ModelFatories.SiteTypeModelFactory;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;
using Web.ModelFatories.TemplateModelFactory;
using Web.ModelFatories.TemplateModelFactory.Abstraction;
using Web.ModelFatories.WidgetsModelFactory;
using Web.ModelFatories.WidgetsModelFactory.Abstraction;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppCookiePolicy(services);
            ConfigureAppDbContext(services);

            //App inner services
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            //App Identity
            services.AddScoped<IAppUserManager<ApplicationUser>, UserManagerAdapter>();
            services.AddScoped<IAppSignInManager<ApplicationUser>, SignInManagerAdapter>();
            services.AddScoped<IAccountService<ApplicationUser>, AccountService>();

            //ClientSettings
            services.AddScoped<IClientSettingsModelFactory, ClientSettingsModelFactory>();

            //AccountManage
            services.AddScoped<IAccountManageModelFactory, AccountManageModelFactory>();

            //Messages
            services.AddScoped<IMessagesModelFactory, MessagesModelFactory>();
            services.AddScoped<IMessageService<MessageDTO>, MailBoxService>();
            services.AddScoped<IMailBoxService<MailBoxDTO>, MailBoxService>();

            //BlogPost
            services.AddScoped<IAppBlogPostService, AppBlogPostService>();
            services.AddScoped<IPublicBlogPostService<PublicPostDTO>, PublicBlogPostService>();
            services.AddScoped<IAdministratedBlogPostService<AdministratedPostDTO>, AdministratedBlogPostService>();
            services.AddScoped<IClientBlogPostService<ClientPostDTO>, ClientBlogPostService>();
            services.AddScoped<IBlogModelFactory, BlogModelFactory>();

            //Projects
            services.AddScoped<IClientProjectService<ClientProjectDTO>, ClientProjectService>();
            services.AddScoped<IProjectsModelFactory, ProjectsModelFactory>();

            //SiteTypes
            services.AddScoped<ISiteTypesService<SiteTypeDTO>, SiteTypesService>();
            services.AddScoped<ISiteTypeModelFactory, SiteTypeModelFactory>();
            services.AddScoped<IAppSiteTypesService<SiteType>, AppSiteTypesService>();
            services.AddScoped<IAppProjectsService<Project>, AppProjectsService>();
            services.AddScoped<ISiteTypeEditorService<SiteTypeEditorDTO>, SiteTypeEditorService>();
            services.AddScoped<IAppLaunchConfigService<LaunchConfig>, AppLaunchConfigService>();
            services.AddScoped<IAppStoreTypeSiteService<StoreTypeSite>, AppStoreTypeSiteService>();
            services.AddScoped<IAppBlogTypeSiteService<BlogTypeSite>, AppBlogTypeSiteService>();
            services.AddScoped<SiteTypesFactory, BlogTypeSiteFactory>();
            services.AddScoped<SiteTypesFactory, StoreTypeSiteFactory>();

            //Widgets
            services.AddScoped<IAppClientWidgetService, AppClientWidgetService>();
            services.AddScoped<IAppWidgetService, AppWidgetService>();
            services.AddScoped<IWidgetService<ClientWidgetListDTO>, ClientWidgetService>();
            services.AddScoped<IWidgetModelFactory, WidgetModelFactory>();
            services.AddScoped<IManageWidgetService<ClientSiteWidgetsDTO>, ManageWidgetsService>();

            //LaunchSite
            services.AddScoped<ILaunchSiteService, LaunchSiteService>();

            //Templates
            services.AddScoped<ITemplateService<SiteTemplateDTO>, TemplateService>();
            services.AddScoped<ITemplateModelFactory, TemplateModelFactory>();

            //Site
            services.AddScoped<ISiteModelFactory, SiteModelFactory>();
            services.AddScoped<ISiteService<SiteRenderingDTO>, SiteService>();


            //Client
            services.AddScoped<IClientModelFactory, ClientModelFactory>();

            //Admin
            services.AddScoped<IAdminModelFactory, AdminModelFactory>();

            services.AddScoped<IAppMailBoxService, AppMailBoxService>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //Admin Site Types
            services.AddScoped<IAdminSiteTypeService<AdminSiteTypeDTO>, AdminSiteTypeService>();
            services.AddScoped<IAdminSiteTypesModelFactory, AdminSiteTypesModelFactory>();
            services.AddScoped<IAppAdminSiteTypesService<SiteType>, AppAdminSiteTypesService>();

            //Admin Site Type Useble widgets
            services.AddScoped<IAdminSiteTypeUsebleWidgetsService<AdminSiteTypeUsebleWidgetsDTO>, AdminSiteTypeUsebleWidgetsService>();
            services.AddScoped<IAppAdminSiteTypesUsebleWidgetsService<SiteType>, AppAdminSiteTypesUsebleWidgetsService>();
            services.AddScoped<IAdminSiteTypeWidgetModelFactory, AdminSiteTypeWidgetModelFactory>();
            services.AddScoped<IAdminSiteTypeUsebleWidgetsService<UsebleSiteTypeWidgetListDTO>, AdminSiteTypeWidgetsService>();
            services.AddScoped<IAdminSiteTypeWidgetService, AdminSiteTypeWidgetService>();

            //Admin Site Template 
            services.AddScoped<ISiteTemplateService, SiteTemplateService>();

            //Admin Widgets
            services.AddScoped<IWidgetService<AdminClientWidgetListDTO>, AdminWidgetsService>();
            services.AddScoped<IAdminWidgetsModelFactory, AdminWidgetsModelFactory>();

            //Wallet
            services.AddScoped<IAppWalletService<Wallet>, AppWalletService>();
            services.AddScoped<IWalletService<WalletDTO>, WalletService>();
            services.AddScoped<IClientWalletModelFactory, ClientWalletModelFactory>();

            //Tokens
            services.AddScoped<ITokenService, WalletService>();
            services.AddScoped<IDiamondService, WalletService>();
            services.AddScoped<IAppProjectCalculatorService, AppProjectCalculatorService>();
            services.AddScoped<IAppWidgetCalculatorService, AppWidgetCalculatorService>();

            //Tutorial
            services.AddScoped<ITutorialService, TutorialService>();

            //SiteWidgets
            services.AddScoped<IManageSiteWidgetService, ManageSiteWidgetService>();


            //Infrastructure Services
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IRepoHubConnector<RepoPullTemplateDTO>, RepoHubConnector>();

            services.AddScoped<IRepoHubKeyMaker, RepoHubKeyMaker>();
            services.AddScoped<IHostingHubConnector, HostingHubConnector>();
            services.AddScoped<IHubKeyMaker<HostingCreatePrepDTO>, HostingHubKeyMaker>();
            services.AddScoped<IFileReader, FileReader>();
            services.AddScoped<ISiteStorageCreatorService, SiteStorageCreatorService>();

            services.AddTransient<IFileTransferrer<ConvertedFileElement>>(r => new FileTransferrer(
                r.GetRequiredService<IFileReader>(),
                new List<string> { ".img", ".jpg", ".png", ".otf", ".eot", ".ttf", ".woff", ".woff2" }
                ));

            services.AddScoped<IAPIRepoClientService<RepoPullTemplateDTO>, GitLabAPIClientService>();
            services.AddScoped<IRepoUserKey, GitLabAPIClientService>();
            services.AddScoped<IAPIHostClientService<NetlifyHubClient>, NetlifyApiClientService>();
            services.AddScoped<IHostDeployToken<DeployKeyDTO>, NetlifyApiClientService>();
            services.AddScoped<IAppSiteTemplatesService<SiteTemplate>, AppSiteTemplatesService>();
            services.AddScoped<IAppTemplateElementsService<SiteTemplateElement>, AppTemplateElementsService>();

            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.Configure<AuthRepoHubConnectorOptions>(Configuration);
            services.Configure<AuthHostingConnectorOptions>(Configuration);
            services.Configure<TemplatesRepoOptions>(Configuration);

            services.AddHttpClient<GitLabHubClient>(c =>
                c.BaseAddress = new Uri("https://gitlab.com/api/v4/")
                );

            services.AddHttpClient<NetlifyHubClient>(c =>
               c.BaseAddress = new Uri("https://api.netlify.com/api/v1/")
               );

            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession(options =>
            {
                options.Cookie.Name = ".SiteProject.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });
        }

        private void ConfigureAppDbContext(IServiceCollection services)
        {
            services.AddDbContext<SSBDbContext>
               (options => options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<SSBDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3)
                );
        }

        private void ConfigureAppCookiePolicy(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}