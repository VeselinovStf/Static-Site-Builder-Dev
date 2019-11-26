using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services.FileTransferrer.DTOs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                        .Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var fileTransporter = services.GetRequiredService<IFileTransferrer<ConvertedFileElement>>();

                try
                {
                    var ssbDbContext = services.GetRequiredService<SSBDbContext>();
                    SSBDbContextSeed.SeedAsync(ssbDbContext, loggerFactory, fileTransporter).Wait();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}