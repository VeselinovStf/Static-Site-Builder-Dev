using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Templates
{
    public class SiteTemplateService : ISiteTemplateService
    {
        public SiteTemplateService()
        {
        }

        public async Task AddUsebleWidgets(string siteTypeId, string templateName)
        {
           //call api and get elements
           //convert if is neaded
           //add them to Db

        }
    }
}
