using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppProjectCalculatorService
    {
        Task<bool> TakeDiamondsAsync(string clientId, string buildInType, string templateName, string siteTypeId);
        Task<bool> CheckDiamondsAsync(string clientId, string buildInType, string templateName, string siteTypeId);
    }
}
