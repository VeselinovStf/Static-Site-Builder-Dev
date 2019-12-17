using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IManageWidgetService<T>
    {
        Task<T> GetAllAsync(string clientId, string templateName);

        IList<string> GetBuildInWidgetTypes();
        Task CreateWidgetAsync(
             string name, string description, string functionality, string implementation,
            decimal price, int version, bool isOn, bool isFree, string widgetType,
            string usebleWidgetType, string dependency
            );

        Task<bool> AddWidget(string widgetId, string clientId);
    }
}
