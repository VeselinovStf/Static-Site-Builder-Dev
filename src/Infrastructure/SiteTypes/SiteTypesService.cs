using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.SiteTypes.DTOs;
using Infrastructure.SiteTypes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SiteTypes
{
    public class SiteTypesService : ISiteTypesService<SiteTypeDTO>
    {
        private readonly IAppSiteTypesService<SiteType> appSiteTypeService;

        public SiteTypesService(
            IAppSiteTypesService<SiteType> appSiteTypeService)
        {
            this.appSiteTypeService = appSiteTypeService ?? throw new System.ArgumentNullException(nameof(appSiteTypeService));
        }

        public async Task<IEnumerable<SiteTypeDTO>> GetAllTypesAsync()
        {
            try
            {
                var siteTypes = await this.appSiteTypeService.GetAllAsync();

                Validator.ObjectIsNull(
                 siteTypes, $"{nameof(SiteTypesService)} : {nameof(GetAllTypesAsync)} : {nameof(siteTypes)} : Can't find build in site types!");

                var serviceModel = new List<SiteTypeDTO>(siteTypes.Select(t => new SiteTypeDTO()
                {
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.Type.ToString()
                }));

                return serviceModel;
            }
            catch (Exception ex)
            {
                throw new SiteTypesServiceGetAllTypesException($"{nameof(SiteTypesServiceGetAllTypesException)} : Can't get build in types! : {ex.Message}");
            }
        }
    }
}