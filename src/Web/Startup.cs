using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Logging;
using Infrastructure.Messages;
using Infrastructure.Messages.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Web.ModelFatories.AccountManageModelFactory;
using Web.ModelFatories.AccountManageModelFactory.Abstraction;
using Web.ModelFatories.ClientSettingsModelFactory;
using Web.ModelFatories.ClientSettingsModelFactory.Abstraction;
using Web.ModelFatories.MessagesModelFactory;
using Web.ModelFatories.MessagesModelFactory.Abstraction;

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
            services.AddScoped<IMailBoxService<MailBoxDTO>, MailBoxService>();
            services.AddScoped<IMailMessageService<MessageDTO>, MailBoxService>();
            services.AddScoped<IAppMailBoxService, AppMailBoxService>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //Extend Service
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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