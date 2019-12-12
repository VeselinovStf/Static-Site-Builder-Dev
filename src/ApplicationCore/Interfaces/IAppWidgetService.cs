using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppWidgetService
    {
        Task<IEnumerable<Widget>> GetAllWidgetsAsync();
      
        IList<SiteWidgetEnum> GetBuildInWidgetTypes();
        Task CreateWidgetAsync(string name, string description, string functionality, string implementation,
            decimal price, int version, bool isOn, bool isFree, string widgetType, SiteWidgetEnum usebleWidgetType, string dependency,
            SiteTypesEnum siteType);
    }
}
