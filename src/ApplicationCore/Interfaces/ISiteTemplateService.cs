using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteTemplateService
    {
        Task AddUsebleWidgets(string siteTypeId, string templateName);
    }
}
