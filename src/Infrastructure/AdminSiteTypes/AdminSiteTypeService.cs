using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypes.DTOs;
using Infrastructure.AdminSiteTypes.Exceptions;
using Infrastructure.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AdminSiteTypes
{
    public class AdminSiteTypeService : IAdminSiteTypeService<AdminSiteTypeDTO>
    {
        private readonly IAppAdminSiteTypesService<SiteType> appSiteTypeService;

        public AdminSiteTypeService(IAppAdminSiteTypesService<SiteType> appSiteTypeService)
        {
            this.appSiteTypeService = appSiteTypeService ?? throw new ArgumentNullException(nameof(appSiteTypeService));
        }

        public async Task<IEnumerable<AdminSiteTypeDTO>> GetAllTypesAsync()
        {
            try
            {
                var siteTypes = await this.appSiteTypeService.GetAllAsync();

                Validator.ObjectIsNull(
                 siteTypes, $"{nameof(AdminSiteTypeService)} : {nameof(GetAllTypesAsync)} : {nameof(siteTypes)} : Can't find build in site types!");

                var serviceModel = new List<AdminSiteTypeDTO>(siteTypes.Select(t => new AdminSiteTypeDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.Type.ToString()
                }));

                return serviceModel;
            }
            catch (Exception ex)
            {
                throw new AdminSiteTypeServiceGetAllTypesException($"{nameof(AdminSiteTypeServiceGetAllTypesException)} : Can't get build in types! : {ex.Message}");
            }
        }

        public IList<string> GetBuildInSiteTypes()
        {
            try
            {
                var buildInSiteTypes = this.appSiteTypeService.GetSiteTypes();

                Validator.ObjectIsNull(
                    buildInSiteTypes, $"{nameof(AdminSiteTypeService)} : {nameof(GetBuildInSiteTypes)} : {nameof(buildInSiteTypes)} : Can't get build in site types!");

                return new List<string>(buildInSiteTypes.Select(t => t.ToString()));

            }
            catch (Exception ex)
            {

                throw new AdminSiteTypeServiceGetBuildInSiteTypesException($"{nameof(AdminSiteTypeServiceGetBuildInSiteTypesException)} : Can't get build in types! : {ex.Message}");

            }
        }

        public async Task<AdminSiteTypeDTO> AddSiteTypeAsync(string name, string description, string siteType, decimal price)
        {
            Validator.StringIsNullOrEmpty(
                name, $"{nameof(AdminSiteTypeService)} : {nameof(AddSiteTypeAsync)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             description, $"{nameof(AdminSiteTypeService)} : {nameof(AddSiteTypeAsync)} : {nameof(description)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             siteType, $"{nameof(AdminSiteTypeService)} : {nameof(AddSiteTypeAsync)} : {nameof(siteType)} : is null/empty");

            try
            {
                var type = (SiteTypesEnum)Enum.Parse(typeof(SiteTypesEnum), siteType);

                var newType = await this.appSiteTypeService.CreateSiteTypeAsync(name, description, type, price);

                return new AdminSiteTypeDTO()
                {
                    Id = newType.Id,
                    Name = newType.Name,
                    Description = newType.Description,
                    BuildInName = newType.Type.ToString(),
                };
            }
            catch (Exception ex)
            {

                throw new AdminSiteTypeServiceAddSiteTypeException($"{nameof(AdminSiteTypeServiceAddSiteTypeException)} : Can't add new site type! : {ex.Message}");

            }
        }

        
    }
}
