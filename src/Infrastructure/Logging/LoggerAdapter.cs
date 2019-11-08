using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<T>();
        }
        public void LogInformation(string message, params object[] args)
        {
            this.logger.LogInformation($"----SSB Logger----: {message} : {args}");
        }

        public void LogWarning(string message, params object[] args)
        {
            this.logger.LogWarning($"----SSB Logger----: {message} : {args}");
        }
    }
}
