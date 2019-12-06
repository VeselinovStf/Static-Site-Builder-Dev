using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Site.DTOs;
using Infrastructure.Site.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Site
{
    public class SiteService : ISiteService<SiteRenderingDTO>
    {
        public SiteService()
        {

        }

        public async Task<SiteRenderingDTO> RenderSiteAsync(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              siteTypeId, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(siteTypeId)} : is null/empty");

            try
            {


                var serviceModel = new SiteRenderingDTO();

                return serviceModel;
            }
            catch (Exception ex)
            {

                throw new SiteServiceRenderSiteException($"{nameof(SiteServiceRenderSiteException)} : Can't render user site! : {ex.Message}");
            }
        }
    }
}
