using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteTemplateService
    {
        Task AddTemplateAsync(string siteTypeId, string templateName,string description, decimal price);
        Task UpdateTemplateStructureAsync(string siteTypeId, string templateId);
    }
}
